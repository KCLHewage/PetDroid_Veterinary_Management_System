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
    public partial class SignUp : KryptonForm
    {
        public SignUp()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;

        private void SignUp_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_username.Text.Length == 0)
                {
                    lbl_error.Text = "Username cannot be blank.";
                    txt_username.Focus();
                    //Check if unique?
                }
                else if (txt_username.Text == "Enter your Username")
                {
                    lbl_error.Text = "Enter a valid Username.";
                    txt_username.Focus();
                }
                else if (txt_password.Text.Length == 0)
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
                else if (txt_fullname.Text.Length == 0)
                {
                    lbl_error.Text = "Name cannot be blank.";
                    txt_fullname.Focus();
                }
                else if (txt_fullname.Text == "Enter your Full Name")
                {
                    lbl_error.Text = "Enter your Name";
                    txt_fullname.Focus();
                }
                else if (txt_fullname.Text.Any(char.IsDigit))
                {
                    lbl_error.Text = "Name cannot have digits";
                    txt_fullname.Focus();
                }
                else if (!Regex.IsMatch(txt_fullname.Text, @"^[a-zA-Z]{3,}(?: [a-zA-Z]+){0,2}$"))
                {
                    lbl_error.Text = "Name cannot have symbols. Must include at least 3 letters";
                    txt_fullname.Focus();
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
                else if (txt_address.Text == "Enter your Address")
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
                    cmd = new SqlCommand("INSERT INTO Owner VALUES ('" + txt_username.Text + "', '" + txt_password.Text + "', '" + txt_fullname.Text + "', '" + txt_nic.Text + "', '" + txt_tp.Text + "',  '" + txt_address.Text + "', '" + txt_email.Text + "', '" + dob_picker.Value + "') ", con);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        KryptonMessageBox.Show("Data Saved Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //MessageBox.Show("Data saved Successfully");

                        Hide();

                    }
                    else
                    {
                        KryptonMessageBox.Show("Registration Unsuccessful, Please Checkk Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //MessageBox.Show("Invalid Request,Please Check Your Data Again");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            lbl_error.Text = "";
            txt_username.Clear();
            txt_password.Clear();
            txt_fullname.Clear();
            txt_nic.Clear();
            txt_tp.Clear();
            txt_address.Clear();
            txt_email.Clear();
            dob_picker.ResetCalendarTodayText();
        }
    }
}
