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
    public partial class Health : KryptonForm
    {
        private string username;
        private int owner_id;
        private string fullname;
        private string tp;
        private string address;
        private string email;
        public Health(int owner_id)
        {
            InitializeComponent();
            this.owner_id = owner_id;
        }
        SqlConnection con;
        SqlDataAdapter da;

        private void Health_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
        }

        private void btn_filter_Click(object sender, EventArgs e)
        {
            try
            {

                if (radiobtn_name.Checked == true && txt_petname.Text.Length != 0)
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT Pet.Pet_Name, Pet.Pet_Type,Health.Health_Category,Health.Treatement_Name,Health.Treatement_Dosage,Health.Last_Visit,Health.Next_Visit FROM Pet, Health WHERE Pet.Pet_Id = Health.Pet_Id AND Pet.Pet_Name ='" + txt_petname.Text + "'AND Pet.Owner_Id='"+owner_id+"'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_health.DataSource = dt;
                    con.Close();
                }
                else if (radiobtn_type.Checked == true)
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT Pet.Pet_Type, Pet.Pet_Name, Health.Health_Category,Health.Treatement_Name,Health.Treatement_Dosage,Health.Last_Visit,Health.Next_Visit FROM Pet, Health WHERE Pet.Pet_Id = Health.Pet_Id AND Pet.Pet_Type ='" + this.cmb_type.GetItemText(this.cmb_type.SelectedItem) + "'AND Pet.Owner_Id='"+owner_id+"'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_health.DataSource = dt;
                    con.Close();
                }
                else
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT Pet.Pet_Type, Pet.Pet_Name, Health.Health_Category,Health.Treatement_Name,Health.Treatement_Dosage,Health.Last_Visit,Health.Next_Visit FROM Pet, Health WHERE Pet.Pet_Id = Health.Pet_Id AND Pet.Owner_Id='" + owner_id + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_health.DataSource = dt;
                    con.Close();
                }

            }

            catch (SqlException)
            {
                MetroFramework.MetroMessageBox.Show(this, "Database Errors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}