using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestNiMax
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Ivi.Visa.Interop.ResourceManager listNi;
        public OSC osc = new OSC();

        private void Form1_Load(object sender, EventArgs e)
        {
            listNi = new Ivi.Visa.Interop.ResourceManager();
            try
            {
                string[] allNi = listNi.FindRsrc("?*");
                foreach (string list_ in allNi)
                {
                    comboBox1.Items.Add(list_);
                }
            }catch { }
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            osc.name = comboBox1.Text;
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            osc.Connect();
            osc.Send(textBox1.Text);
            osc.DisConnect();
        }
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            osc.Connect();
            textBox2.Text = osc.Query(textBox3.Text);
            osc.DisConnect();
        }



        public class OSC
        {
            private Connect psu { get; set; }
            public string name { get; set; }
            

            public OSC()
            {
                psu = new Connect();
            }
            public void Connect()
            {
                psu.ConnectInstr(name);
            }
            public void DisConnect()
            {
                psu.DisConnectInstr();
            }
            public void Send(string cmd)
            {
                psu.SendSLICCmd(cmd);
            }
            public string Query(string cmd)
            {
                return psu.QueryString(cmd);
            }
        }
    }
}
