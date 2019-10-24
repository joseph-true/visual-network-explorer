// Sample program to test IP geo-location lookup using MaxMind GeoIP.
// Finds a physical geo-location for the specified IP address.
//
// Joseph True
// jtrueprojects@gmail.com
// http://www.josephtrue.com/
// Project Repo: https://github.com/joseph-true/visual-network-explorer
// Oct, 2019
//
// This product includes GeoLite2 data created by MaxMind, available from
// https://www.maxmind.com


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MaxMind.Db;
using MaxMind.GeoIP2;


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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // This creates the DatabaseReader object, which should be reused across lookups.
            try
            {
                var reader = new DatabaseReader("GeoLite2-City.mmdb");

                // Get geo location info for specific IP address
                var geoLoc = reader.City(textBox2.Text.ToString());

                // Assemble geo location info
                string geoIPText;
                geoIPText = "IsoCode: " + geoLoc.Country.IsoCode; // EX: 'US'
                geoIPText = geoIPText + "\r\nCountry Name: " + geoLoc.Country.Name;   // EX: 'United States'
                geoIPText = geoIPText + "\r\nMostSpecificSubdivision.Name: " + geoLoc.MostSpecificSubdivision.Name; // EX: 'Minnesota'
                geoIPText = geoIPText + "\r\nMostSpecificSubdivision.IsoCode: " + geoLoc.MostSpecificSubdivision.IsoCode; // EX: 'MN'
                geoIPText = geoIPText + "\r\nCity.Name: " + geoLoc.City.Name; // EX: 'Minneapolis'
                geoIPText = geoIPText + "\r\nPostal.Code: " + geoLoc.Postal.Code; // EX: '55455'
                geoIPText = geoIPText + "\r\nLatitude: " + geoLoc.Location.Latitude; // EX: 44.9733
                geoIPText = geoIPText + "\r\nLongitude: " + geoLoc.Location.Longitude; // EX: -93.2323

                // Show results from the lookup
                textBox1.Text = geoIPText;
            }
            catch (Exception er)
            {
                textBox1.Text = "Exception caught: " + er;
            }
        }
    }
}
