using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSkabaPOSako.MainForms
{
    public partial class PopupBox : Form
    {
        bool _isError = false;
        string _message = "";
        public PopupBox(bool isError, string message)
        {
            _isError = isError;
            _message = message;
            InitializeComponent();
        }

        private void PopupBox_Load(object sender, EventArgs e)
        {
            if (_isError)
                pictureBox1.ImageLocation = @"media\error.png";
            else
                pictureBox1.ImageLocation = @"media\check.png";
            label1.Text = _message;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
   
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
