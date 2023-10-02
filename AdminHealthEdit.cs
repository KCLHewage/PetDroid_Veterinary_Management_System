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
    public partial class AdminHealthEdit : KryptonForm
    {
        public AdminHealthEdit()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader da;

        private void AdminHealthEdit_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");

            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT Health_Id FROM Health", con);
                da = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Health_Id", typeof(string));
                dt.Load(da);
                cmb_id.ValueMember = "Health_Id";
                cmb_id.DataSource = dt;
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error failed to set Owner ID");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE Health SET Health_Category = '" + Convert.ToString(cmb_catogery.GetItemText(cmb_catogery.SelectedItem)) + "',Treatement_Name = '" + txt_treatement.Text + "',Treatement_Dosage = '" + txt_dosage.Text + "',Last_Visit = '" + dob_lastvisit.Value + "',Next_Visit = '" + dob_nextvisit.Value + "' WHERE Health_Id = '" + Convert.ToInt32(cmb_id.GetItemText(cmb_id.SelectedItem)) + "'", con);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Data Update Successfully");
                    Hide();
                }
                else
                {
                    MessageBox.Show("Data Cannot Update");
                }
                con.Close();
                cmd.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Database Error");
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
