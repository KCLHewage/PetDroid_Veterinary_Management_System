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
    public partial class AdminPetView : KryptonForm
    {
        public AdminPetView()
        {
            InitializeComponent();

        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;

        private void AdminPetView_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (radiobtn_petid.Checked == true && txt_petid.Text.Length != 0 && (txt_petid.Text.Any(char.IsDigit)))
                {            
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Pet WHERE Pet_Id ='" + Convert.ToInt32(txt_petid.Text) + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_pet.DataSource = dt;
                    con.Close();
                }
                else if (radiobtn_type.Checked == true)
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Pet WHERE Pet_Type = '" + cmb_type.GetItemText(cmb_type.SelectedItem) + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_pet.DataSource = dt;
                    con.Close();
                }
                else if (radiobtn_ownerid.Checked == true && txt_ownerid.Text.Length != 0 && (txt_ownerid.Text.Any(char.IsDigit)))
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT Owner_id,Pet_Type,Pet_Id,Pet_Breed,Pet_Name,Pet_DOB,Pet_Gender,Pet_Bloodtype FROM Pet WHERE Owner_Id = '" + Convert.ToInt32(txt_ownerid.Text) + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_pet.DataSource = dt;
                    con.Close();
                }
                else
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Pet", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_pet.DataSource = dt;
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
