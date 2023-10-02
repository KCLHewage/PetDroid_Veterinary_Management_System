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
    public partial class Main : KryptonForm
    {
        private string username;
        private int owner_id;
        private string fullname;
        private string tp;
        private string address;
        private string email;
        private int petcount;
        private int healthcount;
        

        public Main(string username)
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

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            openChildForm(new Overview(username, owner_id, fullname, tp, address, email, petcount,healthcount));
        }

        private void btn_mypets_Click(object sender, EventArgs e)
        {
            openChildForm(new MyPets(owner_id));
        }

        private void btn_pethealth_Click(object sender, EventArgs e)
        {
            openChildForm(new Health(owner_id));
        }

        private void btn_qrfinder_Click(object sender, EventArgs e)
        {
            openChildForm(new QrFinder(fullname, owner_id, tp, address));
        }

        private void btn_aboutus_Click(object sender, EventArgs e)
        {
            openChildForm(new AboutUs());
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Hide();
            Owner_Login login = new Owner_Login();
            login.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Sql Connection
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
            cmd = new SqlCommand("SELECT Owner_Id, Owner_Name, Owner_TP, Owner_Address, Owner_Email FROM Owner WHERE Owner_Username='" + username + "'", con);
            con.Open();
            da = cmd.ExecuteReader();
            while (da.Read())
            {
                owner_id = da.GetInt32(0);
                fullname = da.GetString(1);
                tp = (da.GetInt32(2)).ToString();
                address = da.GetString(3);
                email = da.GetString(4);
            }
            da.Close();
            cmd.Dispose();

            
            cmd = new SqlCommand("SELECT COUNT(*) FROM Pet WHERE Pet.Owner_Id='"+ owner_id + "'", con);
            petcount = (Int32)cmd.ExecuteScalar();
            cmd.Dispose();

            cmd = new SqlCommand("SELECT COUNT(Health_Id) FROM Health,Pet WHERE Pet.Pet_Id=Health.Pet_Id AND Pet.Owner_Id='" + owner_id + "'", con);
            healthcount = (Int32)cmd.ExecuteScalar();
            cmd.Dispose();
        }
    }
}
