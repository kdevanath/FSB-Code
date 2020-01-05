using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AlgMgrWindow
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            
            if (args.Length > 1)
            {
                this.comboBox1.Text = args[1];
                Console.WriteLine(args[1]);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbox = (CheckBox)sender;
            if (!cbox.Checked)
                label3.Text = "TEST";
            else
                label3.Text = "PROD";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            if (cbox.Text.Equals("LIME"))
            {
                textBox1.Text = "10.22.73.130";
            }else if(cbox.Text.Equals("ITG"))
            {
                textBox1.Text = "10.27.113.10";
            }
        }

        private String _shareHost;
        public String ShareHost
        {
            get { return _shareHost; }
            set { _shareHost = value; }
        }
        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        private bool _testMode;
        public bool Test
        {
            get { return _testMode; }
            set { _testMode = value; }
        }

        private String _broker;
        public String Broker
        {
            get { return _broker; }
            set { _broker = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                Test = false;
            else
                Test = true;

            Broker = comboBox1.Text;

            Port = Convert.ToInt32(numericUpDown1.Value);

            ShareHost = textBox1.Text;

            Console.WriteLine(Test + " " + Broker + " " + Port + " " + ShareHost);
            Close();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Console.WriteLine("Closing " + e.CloseReason.ToString());
        }
    }
}