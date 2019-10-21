// Test app to show examples of using the ping command.
// 
// Joseph True
// jtrueprojects@gmail.com
// http://www.josephtrue.com/
// Project Repo: https://github.com/joseph-true/visual-network-explorer
// Oct, 2019
//
// NOTE: Some web servers are configured to not accept ICMP commands and will not work with the ping command.
//
// Ping class
// https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation.ping?view=netframework-4.8

// IPStatus Enum
// https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation.ipstatus?view=netframework-4.8


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Populate the combo-box list with some servers to try
            cboServerName.Items.Add("www.google.com");
            cboServerName.Items.Add("www.yahoo.com");
            cboServerName.Items.Add("www.amazon.com");
            cboServerName.Items.Add("mit.edu");
            cboServerName.Items.Add("stanford.edu");
            cboServerName.Items.Add("192.168.1.1");
            // As a test, add some entries that won't work
            cboServerName.Items.Add("---------------------");
            cboServerName.Items.Add("not_a_valid_web_server.com");
            cboServerName.Items.Add("192.168.1.0");
            
            // Display first option in list
            cboServerName.SelectedIndex = 0;
        }

        private void btnPingServer_Click(object sender, EventArgs e)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 2000;   // 2 secs

            // Get name of the server to ping
            string host = cboServerName.Text;

            try
            {
                // Run the actual ping command
                PingReply reply = pingSender.Send(host, timeout);

                if (reply.Status == IPStatus.Success)
                {
                    // It worked!  Display results.
                    string pingResults;
                    pingResults = "Status: " + reply.Status.ToString();
                    pingResults = pingResults + "\r\nAddress: " + reply.Address.ToString();
                    pingResults = pingResults + "\r\nRoundTrip time: " + reply.RoundtripTime;
                    pingResults = pingResults + "\r\nTime to live: " + reply.Options.Ttl;
                    pingResults = pingResults + "\r\nDon't fragment: " + reply.Options.DontFragment;
                    pingResults = pingResults + "\r\nBuffer size: " + reply.Buffer.Length;
                    textBox1.Text = pingResults;
                }
                else
                {
                    // Something else happened. Display status.
                    String pingMessage = "Ping command did not work:";
                    pingMessage = pingMessage + "\r\n" + reply.Status.ToString();
                    textBox1.Text = pingMessage;
                }
            }
            catch (Exception ping_error)
            {
                // Some other error happened while trying to run ping.
                String errMessage = "";
                errMessage = ping_error.Message;
                errMessage = errMessage + "\r\n" + ping_error.InnerException.ToString();
                textBox1.Text = errMessage;

                pingSender.Dispose();
                return;
            }
        }
    }
}
