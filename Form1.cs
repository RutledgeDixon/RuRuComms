using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;

//RuRu Comms works with a simple TCP server, which takes a message from a client and sends it to all other clients

/*      NOTES:
 *      This program is designed for end users who do not have knowledge of networking
 *      Feeling wheel exists in data form as Feeling class with root node rootFeeling
 *      Messages on neat style tab are staggered by one line on each side, but a longer message will mess this up
 *      
 *      Codes:
 *          Basic: "BxF"
 *          ID: "BxF_ID_"
 *          Simple mesg: "BxFxx"
 *          Feeling: "BxF_FEEL_"
 *          Server: "BxF_SERVER_"
 */

/*    TODO:
      add animation to feeling wheel? like mouse-over
*/

namespace RuRu_Comms
{
    public partial class Form1 : Form
    {
        private const int DiscoveryPort = 5000;
        private string IPAddress = "Error";
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private CancellationTokenSource _receiveCancellation;
        private const string IP_PLACEHOLDER = "Magic #...";
        private int newNotifications = 0; //number of new notifications on the neat style tab
        private Font wheelFont = new Font("Arial", 12, FontStyle.Bold);
        private bool nerdVisible = false;
        private string clientId = string.Empty;

        //feelings for feelings wheel
        class Feeling
        {
            public string Name { get; set; }
            public Feeling Parent { get; set; }
            public List<Feeling> Children { get; set; } = new List<Feeling>();
            public Feeling(){}
            public Feeling getParent()
            {
                return this.Parent;
            }
        }

        private Feeling rootFeeling = new Feeling {Name = "Feeling"};
        private Feeling currentFeeling = null;


        public Form1()
        {
            InitializeComponent();
            // Attach event handlers for user activity on the messages tab
            //  this is for the notifications functionality
            AttachUserActivityHandlers(tabPage2); 

            //IPText placeholder code
            SetPlaceholder(IPText, IP_PLACEHOLDER);
            IPText.GotFocus += (s, e) => RemovePlaceholder(IPText, IP_PLACEHOLDER);
            IPText.LostFocus += (s, e) => SetPlaceholder(IPText, IP_PLACEHOLDER);

            //Initializing Feeling Tree
            string jsonFilePath = "feelings.js"; // Path to your JSON file
            BuildFeelingsTreeFromJson(jsonFilePath);

            //testing feeling data
            //printFeelingsTree();

            //initialize feelingWheel
            feelingWheelPanel.Invalidate();

            //set tab to neat style (messages) tab
            tabControl1.SelectTab(0);
            tabPage2.Text = "Messages";
            tabPage1.BackColor = Color.DarkSeaGreen;
            tabPage2.BackColor = Color.DarkSeaGreen;
            tabPage3.BackColor = Color.DarkSeaGreen;

            //nerd tab starts hidden
            tabControl1.TabPages.Remove(tabPage1);

            
        }

        // Connect to the server
        private async void ConnectToServer(string serverIp, int port)
        {
            // Cancel previous receive thread if running
            if (_receiveCancellation != null)
            {
                _receiveCancellation.Cancel();
                _receiveCancellation = null;
            }

            //first disconnect if already connected
            if (_tcpClient != null && _tcpClient.Connected)
            {
                try
                {
                    _networkStream?.Close();
                    _tcpClient.Close();
                }
                catch (Exception ex)
                {
                    AppendLog($"Error during disconnect: {ex.Message}");
                }
                finally
                {
                    _networkStream = null;
                    _tcpClient = null;
                }
                AppendLog("Disconnected from server.");
                // Optional: await Task.Delay(100); // Give the OS a moment to release the socket
            }

            //now try to connect to the server
            AppendLog($"Connecting to server {serverIp} on port {port}");
            try
            {
                _tcpClient = new TcpClient();
                var connectTask = _tcpClient.ConnectAsync(serverIp, port);

                //change button to connecting...
                btnConnectToServer.Text = "Connecting...";
                btnConnectToServer.BackColor = Color.OrangeRed;

                // Adding an explicit delay (because default TCP delay is too long)
                if (await Task.WhenAny(connectTask, Task.Delay(3000)) == connectTask)
                {
                    _networkStream = _tcpClient.GetStream();
                    AppendLog("Connected to server!");
                    string sendId = "BxF_ID_" + clientId;
                    SendMessage(sendId);

                    //change button to connected
                    btnConnectToServer.Text = "Connected!";
                    btnConnectToServer.BackColor = Color.DarkSeaGreen;

                    // Start a thread to listen for messages from the server
                    _receiveCancellation = new CancellationTokenSource();
                    Thread receiveThread = new Thread(() => ReceiveMessages(_receiveCancellation.Token));
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
                else
                {
                    AppendLog("Connection timed out.");

                    //change button back to connect
                    btnConnectToServer.Text = "Connect!";
                    btnConnectToServer.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                AppendLog($"Error connecting to server: {ex.Message}");

                //change button back to connect
                btnConnectToServer.Text = "Connect!";
                btnConnectToServer.BackColor = Color.White;
            }
        }

        // Send a message to the server
        private void SendMessage(string message)
        {
            if (_tcpClient != null && _tcpClient.Connected && _networkStream != null)
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                _networkStream.Write(data, 0, data.Length);

                printPretty(message, 1);
            }
            else
            {
                AppendLog("Error sending message: not connected to server");
            }
            
        }

        // Receive messages from the server
        private void ReceiveMessages(CancellationToken token)
        {
            byte[] buffer = new byte[1024];

            while (!token.IsCancellationRequested)
            {
                try
                {
                    int bytesRead = _networkStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string parcel = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    //AppendLog($"Received: {parcel}");

                    int newMesgIndex;
                    //this is for when the server sends buffered messages as one big message bc of TCP
                    while ((newMesgIndex = parcel.IndexOf('\n')) >= 0)
                    {
                        //extract the message
                        string message = parcel.Substring(0, newMesgIndex).TrimEnd('\r');
                        parcel = parcel.Substring(newMesgIndex + 1);

                        //process the message
                        AppendLog($"Received: {message}");
                        Invoke(new Action(() =>
                        {
                            printPretty(message, 0);
                        }));
                    }
                    //print the last message if it exists
                    if (parcel.Length > 0)
                    {
                        string message = parcel.TrimEnd('\r');
                        Invoke(new Action(() =>
                        {
                            printPretty(message, 0);
                        }));
                    }

                }
                catch (Exception ex)
                {
                    if (!token.IsCancellationRequested)
                    {
                        AppendLog($"Error receiving message: {ex.Message}");
                    }
                    break;
                }
            }

            //change connection button back to connect
            Invoke(new Action(() =>
            {
                btnConnectToServer.Text = "Connect!";
                btnConnectToServer.BackColor = Color.White;
            }));
        }

        //this message displays the message correctly in the neat style tab
        //  if it is a normal message, it prints it normally
        //  if it is a coded message, it deciphers it and does what it commands
        //  sendOrReceive is 0 for received messages and 1 for sent messages
        private void printPretty(string message, int sendOrReceive)
        {
            //idk what to do here yet - how much is print and how much is graphical?
            if (string.IsNullOrWhiteSpace(message))
            {
                AppendLog("Error: Received an empty or null message.");
                return;
            }

            if (message.StartsWith("BxF"))
            {
                if (message.StartsWith("BxF_FEEL_"))
                {
                    string feelingName = message.Substring("BxF_FEEL_".Length);
                    Feeling f = FindFeelingByName(rootFeeling, feelingName);

                    if (f != null)
                    {
                        updateFeelingOnNeatStyle(f, sendOrReceive);
                        if (sendOrReceive == 0)
                        {
                            AppendLog($"Received feeling '{feelingName}'");
                        }
                        else
                        {
                            AppendLog($"Sent feeling '{feelingName}'");
                        }
                    }
                    else
                    {
                        AppendLog($"Feeling '{feelingName}' not found in the tree. Updating manually");
                        updateFeelingOnNeatStyle_Manual_(feelingName, sendOrReceive);
                    }
                }
                else if (message.StartsWith("BxF_ID_"))
                {
                    AppendLog($"Sent client ID: {message.Substring("BxF_ID_".Length)}");
                }
                else if (message.StartsWith("BxF_SERVER_"))
                {
                    string msg = message.Substring("BxF_SERVER_New connection: ".Length);

                    if (message.StartsWith("BxF_SERVER_New Connection: "))
                    {
                        
                        AppendLog($"New client connected: {msg}");
                        printReceivedText($"{msg} just connected!", sendOrReceive);
                    }
                    else if (message.StartsWith("BxF_SERVER_Client disconnected: "))
                    {
                        AppendLog($"Client disconnected: {msg}");
                        printReceivedText($"{msg} just disconnected :'(", sendOrReceive);
                    }
                    else
                    {
                        AppendLog($"Received server message: {message.Substring("BxF_SERVER_".Length)}");
                    }
                }
                else
                {
                    //Right now this is where BxFxx and BxF_SERVER_ messages go, they aren't used
                    AppendLog($"Coded message not handled yet: {message}");
                }
            }
            else
            {
                //print the message
                printReceivedText(message, sendOrReceive);
                if (sendOrReceive == 0)
                {
                    AppendLog($"Received: {message}");
                }
                else
                {
                    AppendLog($"Sent: {message}");
                }

                //add a notification to the neat style tab
                if (sendOrReceive == 0)
                {
                    newNotifications++;
                }
                udpateNotificationLabel();
            }

            

        }


        //simply print a line of string into the right text box
        private void printReceivedText(string message, int box)
        {
            if(box == 0)
            {
                displayMesg0.Text += message + Environment.NewLine;
                // auto scroll-down the box
                displayMesg0.SelectionStart = displayMesg0.Text.Length;
                displayMesg0.ScrollToCaret();
            }
            else if(box == 1)
            {
                displayMesg1.Text += message + Environment.NewLine;
                //auto scroll-down the box
                displayMesg1.SelectionStart = displayMesg1.Text.Length;
                displayMesg1.ScrollToCaret();
            }
            else
            {
                //this is unnecessary because the error is dealt with up the chain but I still want it
                AppendLog($"Error: box number not valid for printing text: {box}");
            }
        }

        private Feeling FindFeelingByName(Feeling root, string name)
        {
            if (root.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return root;
            }

            foreach (var child in root.Children)
            {
                var result = FindFeelingByName(child, name);
                if (result != null)
                {
                    return result;
                }
            }

            return null; // Return null if no matching feeling is found
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            IPAddress = IPText.Text.Trim();

            // easter egg - if the input is "turtles" then show nerd log
            if (IPAddress.Equals("turtles", StringComparison.OrdinalIgnoreCase) && !nerdVisible)
            {
                tabControl1.TabPages.Insert(0, tabPage1);
                nerdVisible = true;
                return;
            }

            //  if the IP address is empty, set it to the placeholder
            if (IPAddress.Equals(IP_PLACEHOLDER))
            {
                IPAddress = "Error";
            }

            //I should add a try-catch here to check if the IP address is valid
            if (!IPAddress.Equals("Error") && !string.IsNullOrWhiteSpace(IPAddress))
            {
                ConnectToServer(IPAddress, DiscoveryPort);
            }
            else
            {
                AppendLog("Please enter a valid IP address.");
            }
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            //string message = "Hello from client!";
            //SendMessage(message);
            using (var inputForm = new MessageWindowForm())
            {
                if (inputForm.ShowDialog(this) == DialogResult.OK)
                {
                    string message = inputForm.Message;
                    SendMessage(message);
                }
            }
        }

        private void AppendLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                // Ensure thread-safe access to the UI
                txtLog.Invoke(new Action(() => AppendLog(message)));
            }
            else
            {
                // Append the message to the log TextBox
                txtLog.AppendText(message + Environment.NewLine);
            }
        }

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholderText;
                textBox.ForeColor = Color.Gray;
            }
        }

        private void RemovePlaceholder(TextBox textBox, string placeholderText)
        {
            if (textBox.Text == placeholderText)
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = Color.Black;
            }
        }

        private void BuildFeelingsTreeFromJson(string jsonFilePath)
        {
            // Read the JSON file
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);

            // Deserialize the JSON into a Dictionary
            var feelingsData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);

            // Parse the JSON into a Feeling tree
            ParseFeelingsTree(feelingsData, rootFeeling);
        }

        // recursively defines the feeling and each of its children
        private void ParseFeelingsTree(Dictionary<string, object> json, Feeling parent)
        {
            foreach (var entry in json)
            {
                // Create a new Feeling object for the current entry
                var child = new Feeling
                {
                    Name = entry.Key,
                    Parent = parent
                };
                parent.Children.Add(child);

                // Check if the value is a JObject and convert it to a dictionary for recursion
                if (entry.Value is Newtonsoft.Json.Linq.JObject nestedObject)
                {
                    var nestedChildren = nestedObject.ToObject<Dictionary<string, object>>();
                    ParseFeelingsTree(nestedChildren, child); // Recursively parse the nested children
                }
                else if (entry.Value is Dictionary<string, object> nestedChildren)
                {
                    ParseFeelingsTree(nestedChildren, child); // Recursively parse the nested children
                }
                else
                {
                    // Log or handle unexpected data types if needed
                    AppendLog($"Unexpected data type for key: {entry.Key}");
                }
            }
        }

        private void printFeelingsTree()
        {
            
            // Example: Print the root feeling and its children
            AppendLog($"Root Feeling: {rootFeeling.Name}");
            foreach (var child in rootFeeling.Children)
            {
                AppendLog($"Child Feeling: {child.Name}");
                foreach (var grandchild in child.Children)
                {
                    AppendLog($"  Grandchild Feeling: {grandchild.Name}");
                    foreach (var greatgrandchild in grandchild.Children)
                    {
                        AppendLog($"    Greatgrandchild Feeling: {greatgrandchild.Name}");
                    }
                }
            }
        }

        private void FeelingWheelPanel_Paint(object sender, PaintEventArgs e)
        {
            //Console.WriteLine("FeelingWheelPanel_Paint called");
            //AppendLog("FeelingWheelPanel_Paint called");

            if (currentFeeling == null) currentFeeling = rootFeeling;

            var graphics = e.Graphics;
            // Save the current state of the Graphics object
            var state = graphics.Save();

            // Rotate the Graphics object around the center of the panel
            var center = new Point(feelingWheelPanel.Width / 2, feelingWheelPanel.Height / 2);
            graphics.TranslateTransform(center.X, center.Y);
            graphics.RotateTransform(-90);
            graphics.TranslateTransform(-center.X, -center.Y);

            var radius = Math.Min(feelingWheelPanel.Width, feelingWheelPanel.Height) / 2 - 10;

            // Get the number of children
            var feelings = currentFeeling.Children;
            int count = feelings.Count;
            if (count == 0) return;

            // Calculate angles
            float anglePerSegment = 360f / count;
            float startAngle = 0;

            // Draw each segment
            for (int i = 0; i < count; i++)
            {
                var brush = new SolidBrush(GetColorForIndex(i));
                //var sweepAngle = anglePerSegment;

                // Draw the segment
                graphics.FillPie(brush, center.X - radius, center.Y - radius, radius * 2, radius * 2, startAngle, anglePerSegment);

                // Draw the text
                var midAngle = startAngle + anglePerSegment / 2;
                var textPoint = GetPointOnCircle(center, radius / 2, midAngle);
                var feelingName = feelings[i].Name;
                var textSize = graphics.MeasureString(feelingName, this.Font);
                DrawRotatedString(graphics, feelingName, wheelFont, Brushes.Black, textPoint, midAngle);
                //startAngle += sweepAngle;
                startAngle += anglePerSegment;
            }
        }

        private void DrawRotatedString(Graphics graphics, string text, Font font, Brush brush, PointF point, float angle)
        {
            var textSize = graphics.MeasureString(text, font);

            // Save the current state of the Graphics object
            var state = graphics.Save();

            // Move the origin to the point where the text will be drawn
            graphics.TranslateTransform(point.X, point.Y);

            // Rotate the Graphics object
            //  if the object would be more upside down than right side up (very specific I know), flip it upside down
            //  NOTE: checking >= 90 and < 270, but adding 90 because the graphics object is rotated -90 to begin with
            if (angle >= 90+90 && angle < 270+90)
            {
                angle += 180;
            }
            graphics.RotateTransform(angle);

            //offset text so it is centered
            float offX = -textSize.Width / 2;
            float offY = -textSize.Height / 2;

            // Draw the string at the new origin
            graphics.DrawString(text, font, brush, offX, offY);

            // Restore the original state of the Graphics object
            graphics.Restore(state);
        }

        // Helper to get a color for each segment
        //  In order: Orange (fear), purple (anger), blue (sadness), red (love), green (joy), yellow (surprise)
        private Color GetColorForIndex(int index)
        {
            var colors = new[] { "#BF8A3E", "#983EBF", "#3E77BF", "#BF553E", "#3EBF5D", "#E7E432" };
            return ColorTranslator.FromHtml(colors[index % colors.Length]);
        }

        // Helper to calculate a point on the circle
        private PointF GetPointOnCircle(Point center, float radius, float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * (float)Math.PI / 180f;
            float x = center.X + radius * (float)Math.Cos(angleInRadians);
            float y = center.Y + radius * (float)Math.Sin(angleInRadians);
            return new PointF(x, y);
        }

        private void FeelingWheelPanel_MouseClick(object sender, MouseEventArgs e)
        {
            var center = new Point(feelingWheelPanel.Width / 2, feelingWheelPanel.Height / 2);
            var radius = Math.Min(feelingWheelPanel.Width, feelingWheelPanel.Height) / 2 - 10;

            // Calculate the angle of the click
            float dx = e.X - center.X;
            float dy = e.Y - center.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance > radius) return; // Click is outside the wheel

            float angle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
            if (angle < 0) angle += 360;

            //adjust angle to account for -90 degree rotation
            angle = (angle + 90) % 360;

            // Determine which segment was clicked
            var feelings = currentFeeling.Children;
            int count = feelings.Count;
            float anglePerSegment = 360f / count;

            int clickedIndex = (int)(angle / anglePerSegment);
            if (clickedIndex >= 0 && clickedIndex < count)
            {
                // Update the current feeling and redraw
                currentFeeling = feelings[clickedIndex];
                feelingWheelPanel.Invalidate(); // Redraw the panel
            }
            if (currentFeeling.Children.Count == 0)
            {
                string capitalFeeling = currentFeeling.Name.Substring(0, 1).ToUpper() + currentFeeling.Name.Substring(1, currentFeeling.Name.Length - 1);
                // Send the feeling to the server
                sendFeeling(currentFeeling.Name);

                //update tab name with the current feeling
                //tabPage3.Text = capitalFeeling;

                //reset panel and redraw
                currentFeeling = null;
                feelingWheelPanel.Invalidate(); // Redraw the panel

                //change to the messages tab
                tabControl1.SelectTab(tabPage2);
            }
        }

        private void sendFeeling(string feeling)
        {
            string message = "BxF_FEEL_" + feeling;
            SendMessage(message);
        }

        // sendOrReceive: 0 is receive, 1 is send
        private void updateFeelingOnNeatStyle(Feeling feeling, int sendOrReceive)
        {
            //update the feeling above the right person on the neat style tab
            string capitalFeeling = feeling.Name.Substring(0, 1).ToUpper() + feeling.Name.Substring(1, feeling.Name.Length - 1);
            if(sendOrReceive == 0)
            {
                //displayMesg0.Text = capitalFeeling;
                displayFeelingButton0.Text = capitalFeeling;
            }
            else if (sendOrReceive == 1)
            {
                //displayMesg1.Text = capitalFeeling;
                displayFeelingButton1.Text = capitalFeeling;
            }
            else
            {
                AppendLog($"Error: sendOrReceive value not valid: {sendOrReceive}");
            }
            Color feelColor = Color.White;

            //determine the ultimate grandparent of the feeling and change color accordingly
            string ancestor = feeling.getParent().getParent().Name;

            if(ancestor.Equals("love")){
                feelColor = ColorTranslator.FromHtml("#EB6969");
            }
            else if (ancestor.Equals("anger"))
            {
                feelColor = ColorTranslator.FromHtml("#DF9B6B");
            }
            else if (ancestor.Equals("sadness"))
            {
                feelColor = ColorTranslator.FromHtml("#6BA2DF");
            }
            else if (ancestor.Equals("fear"))
            {
                feelColor = ColorTranslator.FromHtml("#D16BDF");
            }
            else if (ancestor.Equals("surprise"))
            {
                feelColor = ColorTranslator.FromHtml("#DADA60");
            }
            else if (ancestor.Equals("joy"))
            {
                feelColor = ColorTranslator.FromHtml("#60DCBC");
            }
            else
            {
                feelColor = Color.White;
                AppendLog("Error: feeling ancestor not read correctly");
            }

            //update the background color
            if (sendOrReceive == 0)
            {
                displayMesg0.BackColor = feelColor;
                displayFeelingButton0.BackColor = feelColor;
            }
            else if (sendOrReceive == 1)
            {
                displayMesg1.BackColor = feelColor;
                displayFeelingButton1.BackColor = feelColor;
            }
            else
            {
                AppendLog($"Error: sendOrReceive value not valid: {sendOrReceive}");
            }
        }

        //updates the feeling with a manually entered feeling that is outside of the tree
        private void updateFeelingOnNeatStyle_Manual_(string feeling, int sendOrReceive)
        {
            Color feelColor = ColorTranslator.FromHtml("#179530");

            if (sendOrReceive == 0)
            {
                //displayMesg0.Text = feeling;
                displayFeelingButton0.Text = feeling;
                displayFeelingButton0.BackColor = feelColor;
                displayMesg0.BackColor = feelColor;
            }
            else if (sendOrReceive == 1)
            {
                //displayMesg1.Text = feeling;
                displayFeelingButton1.Text = feeling;
                displayFeelingButton1.BackColor = feelColor;
                displayMesg1.BackColor = feelColor;
            }
        }

        private void IPLabel_Click(object sender, EventArgs e)
        {

        }
        private void IPText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize clientId
            clientId = initializeClientId();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void FeelingWheelPanel_MouseMove(object sender, MouseEventArgs e)
        {

        }


        private void loadIP_Click_1(object sender, EventArgs e)
        {
            //check if the file exists
            if (!System.IO.File.Exists("ip.json"))
            {
                return;
            }
            //read the json file
            string jsonContent = System.IO.File.ReadAllText("ip.json");
            //deserialize it into a string
            string ip = JsonConvert.DeserializeObject<string>(jsonContent);
            //load this ip into the text box
            RemovePlaceholder(IPText, IP_PLACEHOLDER);
            IPText.Text = ip;
            //append to the log
            AppendLog($"Loaded IP address from file: {ip}");
        }

        private void saveIP_Click(object sender, EventArgs e)
        {
            //create a JSON form of the IP address
            string ipJson = JsonConvert.SerializeObject(IPText.Text.Trim(), Formatting.Indented);

            //check if the file is already created
            if (!System.IO.File.Exists("ip.json"))
            {
                //save it to a file
                System.IO.File.WriteAllText("ip.json", ipJson);
                //append to the log
                AppendLog($"Saved IP address to file: {IPAddress}");
            }
            else if (!IPText.Text.Trim().Equals(IP_PLACEHOLDER))
            {
                //read current saved IP
                //  read the json file
                string jsonContent = System.IO.File.ReadAllText("ip.json");
                //  deserialize it into a string
                string ip = JsonConvert.DeserializeObject<string>(jsonContent);
                //If the IP address is the same, do nothing
                if (ip.Equals(IPText.Text.Trim()))
                {
                    return;
                }
                //add a message popup to ask for confirmation
                DialogResult result = MessageBox.Show($"Current number saved: {ip}\nDo you want to replace with {IPText.Text.Trim()}?", "Save new Number?", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //save it to a file
                    System.IO.File.WriteAllText("ip.json", ipJson);
                    //append to the log
                    AppendLog($"Saved IP address to file: {IPText.Text.Trim()}");
                }
                else
                {
                    AppendLog($"IP address kept: {ip}");
                    return;
                }
            }
        }

        private void udpateNotificationLabel()
        {
            //change text on neat style tab to include number of notifications
            if (newNotifications > 0)
            {
                tabPage2.Text = $"Messages ({newNotifications})";
            }
            else
            {
                tabPage2.Text = "Messages";
            }
        }

        private void IPText_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                newNotifications = 0;
                udpateNotificationLabel();
            }
        }

        private void displayFeelingButton1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage3);
        }

        //reset notification count
        private void MessagesTab_UserActivity(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                newNotifications = 0;
                udpateNotificationLabel();
            }
        }

        //this function makes it so that if the user interacts with the messages tab or button in any way,
        //  the notification count resets
        private void AttachUserActivityHandlers(Control parent)
        {
            //if the user messes with anything inside the tab...
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.MouseClick += MessagesTab_UserActivity;
                ctrl.KeyDown += MessagesTab_UserActivity;
                ctrl.GotFocus += MessagesTab_UserActivity;
                // Recursively attach to child controls
                AttachUserActivityHandlers(ctrl);
            }

            //if the user messes with the button...
            btnSendMessage.MouseClick += MessagesTab_UserActivity;
            btnSendMessage.KeyDown += MessagesTab_UserActivity;
            btnSendMessage.GotFocus += MessagesTab_UserActivity;
        }

        // Replace the line causing the error with the following code to use a standard Windows Forms input dialog instead of relying on Microsoft.VisualBasic.Interaction.InputBox.

        private string initializeClientId()
        {
            // Check if a clientId.json file exists
            if (System.IO.File.Exists("clientId.json"))
            {
                // Read the json file
                string jsonContent = System.IO.File.ReadAllText("clientId.json");
                // Deserialize it into a string
                string id = JsonConvert.DeserializeObject<string>(jsonContent);

                //updateWelcomeLabel
                welcomeLabel.Text = "Hi, " + id + "!";

                return id;
            }
            else
            {
                using (var inputForm = new Form2())
                {
                    if (inputForm.ShowDialog(this) == DialogResult.OK)
                    {
                        string id = inputForm.Name;

                        //create the json file with the clientId
                        string clientIdJson = JsonConvert.SerializeObject(id, Formatting.Indented);
                        System.IO.File.WriteAllText("clientId.json", clientIdJson);
                        //return the clientId

                        //updateWelcomeLabel
                        welcomeLabel.Text = "Hi, " + id + "!";

                        return id;
                    }
                }

                
            }

            return "Client"; // Default value if no input is provided
        }

        private void displayFeelingButton0_Click(object sender, EventArgs e)
        {
            using(MessageWindowForm messageForm = new MessageWindowForm())
            {
                messageForm.Message = "Why are you feeling " + displayFeelingButton0.Text.ToLower() + "?";
                

                if (messageForm.ShowDialog(this) == DialogResult.OK)
                {
                    string message = messageForm.Message;
                    SendMessage(message);
                }
            }
        }

        private void IPText_Key_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                btnConnectToServer_Click(sender, e);
            }
        }
    }
}
