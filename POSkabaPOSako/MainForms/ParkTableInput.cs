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
    public partial class ParkTableInput : BaseController
    {
        MainForm _mf = null;
        public ParkTableInput(MainForm mf)
        {
            _mf = mf;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mf.ParkItems(textBox1.Text,this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
