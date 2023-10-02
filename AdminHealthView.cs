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
    public partial class AdminHealthView : KryptonForm
    {
        public AdminHealthView()
        {
            InitializeComponent();

        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;

        private void AdminHealthView_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
               /* if (radiobtn_pet.Checked == true && !(txt_petid.Text.Any(char.IsDigit)))
                {
                    KryptonMessageBox.Show("Enter Pet Id Number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (radiobtn_owner.Checked == true && (txt_ownerid.Text.Any(char.IsDigit)))
                {
                    KryptonMessageBox.Show("Enter Owner Name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               */
                if (radiobtn_pet.Checked == true && txt_petid.Text.Length != 0 && (txt_petid.Text.Any(char.IsDigit)))
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Health WHERE Pet_Id ='" + Convert.ToInt32(txt_petid.Text) + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_health.DataSource = dt;
                    con.Close();
                }
                else if (radiobtn_owner.Checked == true && txt_ownerid.Text.Length != 0 && (txt_ownerid.Text.Any(char.IsDigit)))
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT Pet_Name,Health_Id,Health_Category,Treatement_Name,Treatement_Dosage,Last_Visit,Next_Visit FROM Health,Pet WHERE Health.Pet_Id = Pet.Pet_Id AND Pet.Owner_Id='" + txt_ownerid.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_health.DataSource = dt;
                    con.Close();
                }
                else
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Health", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_health.DataSource = dt;
                    con.Close();
                }
            }
            catch (SqlException)
            {
                KryptonMessageBox.Show("Database Error, Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Invalid Request, Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
