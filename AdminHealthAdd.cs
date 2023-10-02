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
    public partial class AdminHealthAdd : KryptonForm
    {

        public AdminHealthAdd()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader da;

        private void AdminHealthAdd_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");

            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT Pet_Id FROM Pet", con);
                da = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Pet_Id", typeof(string));
                dt.Load(da);
                cmb_id.ValueMember = "Pet_Id";
                cmb_id.DataSource = dt;
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error failed to set Pet ID");
            }
            
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT Vet_Id FROM Vet", con);
                da = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Vet_Id", typeof(string));
                dt.Load(da);
                cmb_vetid.ValueMember = "Vet_Id";
                cmb_vetid.DataSource = dt;
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error failed to set Vet ID");
            }

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("INSERT INTO Health VALUES ('" + Convert.ToString(cmb_catogery.GetItemText(cmb_catogery.SelectedItem)) + "', '" + txt_treatement.Text + "', '" + txt_dosage.Text + "', '" + dob_lastvisit.Value + "', '" + dob_nextvisit.Value + "', '" + Convert.ToInt32(cmb_id.GetItemText(cmb_id.SelectedItem)) + "', '" +  Convert.ToInt32(cmb_vetid.GetItemText(cmb_vetid.SelectedItem)) + "') ", con);

            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Pet Registered Successfully");
            }
            else
            {
                MessageBox.Show("Pet Registration Unsuccessful, please checkk again");
            }
            con.Close();
            cmd.Dispose();
        }
    }
}
