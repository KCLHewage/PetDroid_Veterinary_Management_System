using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;
using System.Diagnostics;


namespace Pet_Clinic_Project
{
    public partial class AboutUs : KryptonForm
    {
        private string username;
        private int owner_id;
        private string fullname;
        private string tp;
        private string address;
        private string email;
        public AboutUs()
        {
            InitializeComponent();
            try
            {
                StringBuilder queryaddress = new StringBuilder();
                queryaddress.Append("https://www.google.com/maps/place/National+Institute+of+Business+Management/@6.9062952,79.868638,17z/data=!3m1!4b1!4m5!3m4!1s0x3ae2597908e10713:0x5094ec60f1e2649e!8m2!3d6.9062952!4d79.8708267");
                browser_maps.Navigate(queryaddress.ToString());
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void browser_maps_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void pbox_ig_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.instagram.com/");
        }

        private void pbox_fb_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/");
        
        }

        private void pbox_ytd_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/");
        }
    }
}
