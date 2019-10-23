using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TcpClient tcpWhois;
        NetworkStream nsWhois;
        BufferedStream bfWhois;
        StreamWriter strmSend;
        StreamReader strmRecive;

        private void button1_Click(object sender, EventArgs e)
        {
            tcpWhois = new TcpClient("whois.internic.net", 43);
            nsWhois = tcpWhois.GetStream(); 
            bfWhois = new BufferedStream(nsWhois);
            strmSend = new StreamWriter(bfWhois);
            strmSend.WriteLine(HostName.Text);
            strmSend.Flush(); 
            Info.Clear(); 
            try
            {
                strmRecive = new StreamReader(bfWhois);
                string response;
                while ((response = strmRecive.ReadLine()) != null)
                {
                    Info.Text += response + "\r\n";
                }
            }
            catch
            {
                MessageBox.Show("WHOis Server Error :x", "Error");
            }
            finally
            {
                try
                {
                    tcpWhois.Close();
                }
                catch
                {
                }

            }
        }
    }
}
