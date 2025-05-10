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

//RuRu Comms works with a simple TCP server, which takes a message from a client and sends it to all other clients

namespace RuRu_Comms
{
    public partial class Form1 : Form
    {
        private const int DiscoveryPort = 5000;
        private string IPAddress = "Error";
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;

        public Form1()
        {
            InitializeComponent();
        }

        // Connect to the server
        private async void ConnectToServer(string serverIp, int port)
        {
            AppendLog($"Connecting to server {serverIp} on port {port}");
            try
            {
                _tcpClient = new TcpClient();
                var connectTask = _tcpClient.ConnectAsync(serverIp, port);
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
                }
                catch
                {
                    AppendLog("Disconnected from server.");
                    break;
                }
            }
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
            string message = "Hello from client!";
            SendMessage(message);
            AppendLog($"Sent: {message}");
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

        private void IPLabel_Click(object sender, EventArgs e)
        {

        }
        private void IPText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
