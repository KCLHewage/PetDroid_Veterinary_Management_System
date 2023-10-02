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
    public partial class Admin_Main : KryptonForm
    {
        private string username;
        private int owner_id;
        private string fullname;
        private string tp;
        private string address;
        private string email;
        public Admin_Main(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader da;

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_body.Controls.Add(childForm);
            panel_body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Admin_Main_Load(object sender, EventArgs e)
        {
            //Sql Connection
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
           Hide();
            Admin_Login admin = new Admin_Login();
            admin.ShowDialog();
        }

        private void btn_ownerview_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminOwnerView());
        }

        private void btn_owneredit_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminOwnerEdit());
        }

        private void btn_petview_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminPetView());
        }

        private void btn_healthview_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminHealthView());
        }

        private void btn_healthadd_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminHealthAdd());
        }

        private void btn_healthedit_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminHealthEdit());
        }
    }
}
