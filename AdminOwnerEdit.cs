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
    public partial class AdminOwnerEdit : KryptonForm
    {
        public AdminOwnerEdit()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader da;

        private void AdminOwnerEdit_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");

            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT Owner_Id FROM Owner", con);
                da = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Owner_Id", typeof(string));
                dt.Load(da);
                cmb_id.ValueMember = "Owner_Id";
                cmb_id.DataSource = dt;
                con.Close();
                cmd.Dispose();
            }
            catch (SqlException)
            {
                KryptonMessageBox.Show("Database Error, Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Failed to set Owner ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_password.Text.Length == 0)
                {
                    lbl_error.Text = "Password cannot be blank.";
                    txt_password.Focus();
                }
                else if (txt_password.Text == "Enter your Password")
                {
                    lbl_error.Text = "Enter a valid Password.";
                    txt_password.Focus();
                }
                else if (!Regex.IsMatch(txt_password.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
                {
                    lbl_error.Text = "Password must have numbers, uppercase and lowercase letters (min 8 char)";
                    txt_password.Focus();
                }
                else if (txt_name.Text.Length == 0)
                {
                    lbl_error.Text = "Name cannot be blank.";
                    txt_name.Focus();
                }
                else if (txt_name.Text == "Enter your Full Name")
                {
                    lbl_error.Text = "Enter your Name";
                    txt_name.Focus();
                }
                else if (txt_name.Text.Any(char.IsDigit))
                {
                    lbl_error.Text = "Name cannot have digits";
                    txt_name.Focus();
                }
                else if (!Regex.IsMatch(txt_name.Text, @"^[a-zA-Z]{3,}(?: [a-zA-Z]+){0,2}$"))
                {
                    lbl_error.Text = "Name cannot have symbols. Must include at least 3 letters";
                    txt_name.Focus();
                }
                else if (txt_nic.Text.Length == 0)
                {
                    lbl_error.Text = "NIC Number cannot be blank.";
                    txt_nic.Focus();
                }
                else if (txt_nic.Text == "Enter your NIC Number")
                {
                    lbl_error.Text = "Enter a valid NIC Number";
                    txt_nic.Focus();
                }
                else if (!Regex.IsMatch(txt_nic.Text, @"^([0-9]{9}[x|X|v|V]|[0-9]{12})$"))
                {
                    lbl_error.Text = "NIC Number format is incorrect";
                    txt_nic.Focus();
                }
                else if (txt_tp.Text.Length == 0)
                {
                    lbl_error.Text = "Telephone cannot be blank.";
                    txt_tp.Focus();
                }
                else if (txt_tp.Text == "Enter your Telephone Number")
                {
                    lbl_error.Text = "Enter a valid Telephone Number";
                    txt_tp.Focus();
                }
                else if (!Regex.IsMatch(txt_tp.Text, @"^(?:7|0|(?:\+94))[0-9]{8,9}$"))
                {
                    lbl_error.Text = "Telephone Number must be 10 digits";
                    txt_tp.Focus();
                }
                else if (txt_address.Text.Length == 0)
                {
                    lbl_error.Text = "Address cannot be blank.";
                    txt_address.Focus();
                }
                else if (txt_address.Text == "Enter you Address")
                {
                    lbl_error.Text = "Enter your Address";
                    txt_address.Focus();
                }
                else if (txt_address.Text.Length >= 50)
                {
                    lbl_error.Text = "Address is too long, please write in a shorter format.";
                    txt_address.Focus();
                }
                else if (txt_email.Text.Length == 0)
                {
                    lbl_error.Text = "Email cannot be blank.";
                    txt_email.Focus();
                }
                else if (txt_email.Text == "Enter your Email Address")
                {
                    lbl_error.Text = "Enter a valid Email Address";
                    txt_email.Focus();
                }
                else if (!Regex.IsMatch(txt_email.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                {
                    lbl_error.Text = "Incorrect email format. Please enter as follows: name@mail.com";
                    txt_email.Focus();
                }
                else if ((dob_picker.Value).ToString().Length == 0)
                {
                    lbl_error.Text = "DOB cannot be blank.";
                }
                else
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE Owner SET Owner_Password = '" + txt_password.Text + "',Owner_Name = '" + txt_name.Text + "',Owner_NIC = '" + txt_nic.Text + "',Owner_TP = '" + txt_tp.Text + "',Owner_Address = '" + txt_address.Text + "',Owner_Email = '" + txt_email.Text + "',Owner_DOB=  '" + dob_picker.Value + "' WHERE Owner_Id = '" + Convert.ToInt32(cmb_id.GetItemText(cmb_id.SelectedItem)) + "'", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        KryptonMessageBox.Show("Data Updated Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Hide();
                    }
                    else
                    {
                        KryptonMessageBox.Show("Data Could Not be Updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                    cmd.Dispose();
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
