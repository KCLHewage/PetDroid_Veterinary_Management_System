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
    public partial class AddPets : KryptonForm
    {
        private int owner_id;

        private string gender;
       

        public AddPets(int owner_id)
        {
            InitializeComponent();
            this.owner_id = owner_id; 
            
        }
        SqlConnection con;
        SqlCommand cmd;
        

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

        private void btn_add_Click(object sender, EventArgs e)
        {
            try 
            {
                if (cmb_type.SelectedIndex == -1)
                {
                    lbl_error.Text = "Catergory must be Selected.";
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
                    cmd = new SqlCommand("INSERT INTO Pet VALUES ('" + cmb_type.SelectedItem + "', '" + txt_breed.Text + "', '" + txt_name.Text + "', '" + dob_picker.Value + "', '" + gender + "',  '" + txt_bloodtype.Text + "',  '" + owner_id + "') ", con);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        KryptonMessageBox.Show("Pet Registered Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show("Pet Registered Successfully");
                        Hide();
                    }
                    else
                    {
                        KryptonMessageBox.Show("Pet Registration Unsuccessful, Please Check Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show("Pet Registration Unsuccessful, please checkk again");
                    }
                    con.Close();
                    cmd.Dispose();
                }   
            }
            catch (SqlException)
            {
                KryptonMessageBox.Show("Database Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                //MessageBox.Show("Database Errors");
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Please Check Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Please check again");
            }


        }

        private void AddPets_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
            
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
