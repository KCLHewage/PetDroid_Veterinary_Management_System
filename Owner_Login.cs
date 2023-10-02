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
    public partial class Owner_Login : KryptonForm
    {
        public Owner_Login()
        {
            InitializeComponent();
            
        }
        SqlConnection con;
        SqlCommand cmd;

        private void lbl_adminlogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            Admin_Login admin = new Admin_Login();
            admin.ShowDialog();
        }

        private void btn_signup_Click(object sender, EventArgs e)
        {
            SignUp signup = new SignUp();
            signup.ShowDialog();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT Owner_Username,Owner_Password FROM Owner WHERE Owner_Username='" + txt_username.Text + "'AND Owner_Password='" + txt_password.Text + "'", con);
                SqlParameter usernameParam;
                usernameParam = new SqlParameter("@Owner_Username", this.txt_username.Text);
                SqlParameter passwordParam;
                passwordParam = new SqlParameter("@Owner_Password", this.txt_password.Text);

                cmd.Parameters.Add(usernameParam);
                cmd.Parameters.Add(passwordParam);

                SqlDataReader dr = cmd.ExecuteReader();
                if (txt_username.Text.Length == 0)
                {
                    lbl_error.Text = "Username cannot be blank.";
                }
                else if (txt_password.Text.Length == 0)
                {
                    lbl_error.Text = "Password cannot be blank.";
                }
                else if (dr.HasRows)
                {
                    Hide();
                    Main main = new Main(txt_username.Text);
                    main.ShowDialog();

                }
                else
                {
                    lbl_error.Text = "Username and Password not found.";
                    txt_username.Clear();
                    txt_password.Clear();
                }

                con.Close();
                cmd.Dispose();

            }
            catch (SqlException)
            {
                KryptonMessageBox.Show("Could not connect to the database. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Oops, Something went wrong. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Oops, Something went wrong. Please try again.");
            }
        }

        private void Owner_Login_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
        }
    }
}
