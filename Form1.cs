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
//NOTE: This program is designed for end users who do not have knowledge of networking
//Code for translating messages is "BxF", which in decimal is 11x15 or November 15th
//Code for feelings is "BxF_FEEL_"

//NOTE: Feeling wheel exists in data form as Feeling class with root node rootFeeling
//      Need to add functionality to feeling wheel tab, a wheel of feelings, select one
//      and its children will populate the wheel, until you get to the end of the wheel

//TODO:
//      update colors on feeling wheel - done
//      add animation to feeling wheel? like mouse-over
//      add reset button to feeling wheel
//      fix text on feeling wheel - done
//      add end to color wheel function

//      for server: add a variable that keeps current feeling for each client, sends it upon
//                  connection to other clients, updated by feeling wheel

namespace RuRu_Comms
{
    public partial class Form1 : Form
    {
        private const int DiscoveryPort = 5000;
        private string IPAddress = "Error";
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private const string IP_PLACEHOLDER = "Magic Number...";
        private Font wheelFont = new Font("Arial", 12, FontStyle.Bold);

        //feelings for feelings wheel
        class Feeling
        {
            public string Name { get; set; }
            public Feeling Parent { get; set; }
            public List<Feeling> Children { get; set; } = new List<Feeling>();
            public Feeling(){}
        }

        private Feeling rootFeeling = new Feeling {Name = "Feeling"};
        private Feeling currentFeeling = null;


        public Form1()
        {
            InitializeComponent();

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
        }

        // Connect to the server
        private async void ConnectToServer(string serverIp, int port)
        {
            AppendLog($"Connecting to server {serverIp} on port {port}");
            try
            {
                _tcpClient = new TcpClient();
                var connectTask = _tcpClient.ConnectAsync(serverIp, port);
                // Adding an explicit delay (because default TCP delay is too long)
                if (await Task.WhenAny(connectTask, Task.Delay(3000)) == connectTask)
                {
                    _networkStream = _tcpClient.GetStream();
                    AppendLog("Connected to server!");

                    // Start a thread to listen for messages from the server
                    Thread receiveThread = new Thread(ReceiveMessages);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
                else
                {
                    AppendLog("Connection timed out.");
                }
            }
            catch (Exception ex)
            {
                AppendLog($"Error connecting to server: {ex.Message}");
            }
        }

        // Send a message to the server
        private void SendMessage(string message)
        {
            if (_networkStream != null)
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                _networkStream.Write(data, 0, data.Length);
            }
        }

        // Receive messages from the server
        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                try
                {
                    int bytesRead = _networkStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    AppendLog($"Received: {message}");
                    //display the message on the pretty tab
                    printPretty(message);
                }
                catch
                {
                    AppendLog("Disconnected from server.");
                    break;
                }
            }
        }

        private void printPretty(string message)
        {
            //idk what to do here yet - how much is print and how much is graphical?
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            IPAddress = IPText.Text.Trim();
            //I should add a try-catch here to check if the IP address is valid
            if (!IPAddress.Equals("Error"))
            {
                ConnectToServer(IPAddress, DiscoveryPort);
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
                    AppendLog($"Sent: {message}");
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
        private Color GetColorForIndex(int index)
        {
            var colors = new[] {"#BF553E", "#3E77BF", "#3EBF5D", "#E7E432", "#BF8A3E", "#983EBF" };
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
                // Send the feeling to the server
                sendFeeling(currentFeeling.Name);
                //update tab name with the current feeling
                tabPage3.Text = currentFeeling.Name.Substring(0, 1).ToUpper() + currentFeeling.Name.Substring(1, currentFeeling.Name.Length-1);
                //reset panel and redraw
                currentFeeling = null;
                feelingWheelPanel.Invalidate(); // Redraw the panel
            }
        }

        private void sendFeeling(string feeling)
        {
            string message = "BxF_FEEL_" + feeling;
            SendMessage(message);
            AppendLog($"Sent feeling: {message}");
        }

        private void IPLabel_Click(object sender, EventArgs e)
        {

        }
        private void IPText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

    }
}
