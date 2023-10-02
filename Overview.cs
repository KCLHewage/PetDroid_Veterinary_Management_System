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

namespace Pet_Clinic_Project
{
    public partial class Overview : KryptonForm
    {
        private string username;
        private int owner_id;
        private string fullname;
        private string tp;
        private string address;
        private string email;
        private int petcount;
        private int healthcount;
        public Overview(string username, int owner_id, string fullname, string tp, string address, string email,int petcount, int healthcount)
        {
            InitializeComponent();
            this.username = username;
            this.owner_id = owner_id;
            this.fullname = fullname; ;
            this.tp = tp;
            this.address = address;
            this.email = email;
            this.petcount = petcount;
            this.healthcount = healthcount;

        }

        private void Overview_Load(object sender, EventArgs e)
        {
            lbl_username.Text = username;
            lbl_fullname.Text = fullname;
            lbl_tp.Text = tp;
            lbl_address.Text = address;
            lbl_email.Text = email;
            lbl_totalpets.Text = petcount.ToString();
            lbl_healthcount.Text = healthcount.ToString();


            //Video Embed
            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "<iframe id='video' src= 'https://www.youtube.com/embed/{0}' width='400' height='250px' frameborder='0' allowfullscreen></iframe>";
            html += "</body></html>";
            this.web_browser_video.DocumentText = string.Format(html, "https://www.youtube.com/watch?v=Yzv0gXqoCkc".Split('=')[1]);
        }
    }
}
