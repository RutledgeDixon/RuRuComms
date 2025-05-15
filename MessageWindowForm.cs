using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Code for translating messages is "BxF", which in decimal is 11x15 or November 15th
//    BxF is followed by 2 digits, which determine what message to display
//    BxF00 = "I miss you"
//    BxF01 = "I love you"

namespace RuRu_Comms
{
    public partial class MessageWindowForm : Form
    {
        public string Message { get; private set; }
        public MessageWindowForm()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            Message = msgBox.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void missYouButton_Click(object sender, EventArgs e)
        {
            //this is a test for sending coded messages that can be translated by the pretty tab
            msgBox.Text = "BxF00";
            sendButton_Click(sender, e);
        }

        private void loveYouButton_Click(object sender, EventArgs e)
        {
            msgBox.Text = "BxF01";
            sendButton_Click(sender, e);
        }

        private void msgBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                sendButton_Click(sender, e);
            }
            else if (e.KeyValue == (char)Keys.Escape)
            {
                cancelButton_Click(sender, e);
            }
        }

        private void buttonLayout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
