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
    public partial class AdminOwnerView : KryptonForm
    {
        

        public AdminOwnerView()
        {
            InitializeComponent();

        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;

        private void AdminOwnerView_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (radiobtn_ownerid.Checked == true && !(txt_id.Text.Any(char.IsDigit)))
                {
                    KryptonMessageBox.Show("Enter Owner Id Number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (radiobtn_petid.Checked == true && !(txt_petid.Text.Any(char.IsDigit)))
                {
                    KryptonMessageBox.Show("Enter Pet Id Number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/

                if (radiobtn_ownerid.Checked == true && txt_id.Text.Length != 0 && (txt_id.Text.Any(char.IsDigit)))
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Owner WHERE Owner_Id ='" + Convert.ToInt32(txt_id.Text) + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_owner.DataSource = dt;
                    con.Close();
                }
                else if (radiobtn_petid.Checked == true && txt_petid.Text.Length != 0 && (txt_petid.Text.Any(char.IsDigit)))
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT Pet_Id,Owner_Username,Owner_Password, Owner_Name,Owner_NIC,Owner_TP,Owner_Address,Owner_Email,Owner_DOB FROM Owner,Pet WHERE Pet.Owner_Id = Owner.Owner_Id AND Pet.Pet_Id='" + Convert.ToInt32(txt_petid.Text) + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_owner.DataSource = dt;
                    con.Close();
                }
                else
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Owner", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagridview_owner.DataSource = dt;
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
