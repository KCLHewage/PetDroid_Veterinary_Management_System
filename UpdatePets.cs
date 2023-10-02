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
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Pet_Clinic_Project
{
    public partial class UpdatePets : KryptonForm
    {
        
        private int owner_id;
        private string gender;
        public UpdatePets(int owner_id)
        {
            InitializeComponent();
            this.owner_id = owner_id;

        }            

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader da;

        private void UpdatePets_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
            
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT Pet_Id FROM Pet WHERE Pet.Owner_Id='" + owner_id + "'", con);
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
                KryptonMessageBox.Show("Error failed to set Pet ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_id.SelectedIndex == -1)
                {
                    lbl_error.Text = "Pet ID must be Selected.";
                }
                else if (cmb_type.SelectedIndex == -1)
                {
                    lbl_error.Text = "Pet Type must be Selected.";
                }
                else if (txt_breed.Text.Length == 0)
                {
                    lbl_error.Text = "Breed cannot be blank.";
                }
                else if (txt_name.Text.Length == 0)
                {
                    lbl_error.Text = "Name cannot be blank.";
                }
                else if (txt_name.Text.Any(char.IsDigit))
                {
                    lbl_error.Text = "Name cannot have digits";
                    txt_name.Focus();
                }
                else if ((dob_picker.Value).ToString().Length == 0)
                {
                    lbl_error.Text = "DOB cannot be blank.";
                }
                else if (!Regex.IsMatch(txt_bloodtype.Text, @"^(A|B|AB|O)[+-]?$"))
                {
                    lbl_error.Text = "Please Enter Following: A+, A-, B+, B-, O+, O-, AB+ or AB- ";
                    txt_bloodtype.Focus();
                }
                else 
                {
                    if (radiobtn_male.Checked == true)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }
                    con.Open();
                    cmd = new SqlCommand("UPDATE Pet SET  Pet_Type= '" + this.cmb_type.GetItemText(this.cmb_type.SelectedItem) + "', Pet_Breed = '" + txt_breed.Text + "', Pet_Name = '" + txt_name.Text + "', Pet_DOB=  '" + dob_picker.Value + "', Pet_Gender= '" + gender + "', Pet_Bloodtype= '" + txt_bloodtype.Text + "' WHERE Pet_Id = '" + Convert.ToInt32(this.cmb_id.GetItemText(this.cmb_id.SelectedItem)) + "'", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        KryptonMessageBox.Show("Data Updated Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show("Data Update Successfully");
                        Hide();
                    }
                    else
                    {
                        KryptonMessageBox.Show("Data Could Not Be Updated, Please Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show("Data Cannot Update");
                    }
                    con.Close();
                    cmd.Dispose();
                }
            }
            catch (SqlException)
            {
                KryptonMessageBox.Show("Database Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Database Errors");
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Invalid Request,Please Check Your Data Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Errors");
            }

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            cmb_type.SelectedIndex = -1;
            txt_breed.Clear();
            txt_name.Clear();
            radiobtn_male.Checked = true;
            txt_bloodtype.Clear();
        }
    }
}
