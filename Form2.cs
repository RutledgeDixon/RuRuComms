using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuRu_Comms
{
    public partial class Form2 : Form
    {
        public string Name { get; private set; } = string.Empty;
        public Form2()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(nameTextBox.Text != string.Empty)
            {
                Name = nameTextBox.Text.Trim();
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
