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
    public partial class MyPets : KryptonForm
    {
        private int owner_id;

        public MyPets(int owner_id)
        {
            InitializeComponent();
            this.owner_id = owner_id;

        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;


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

        

        private void MyPets_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
        }

        private void btn_addpet_Click(object sender, EventArgs e)
        {
            openChildForm(new AddPets(owner_id));
        }

        private void btn_select_Click(object sender, EventArgs e)
        {

            try
            {
                if (radiobtn_id.Checked == true && txt_petid.Text.Length != 0 && (txt_petid.Text.Any(char.IsDigit)))
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Pet WHERE Pet.Pet_Id ='" + Convert.ToInt32(txt_petid.Text) + "'AND Pet.Owner_Id='" + owner_id + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagrid_mypets.DataSource = dt;
                    con.Close();
                }
                else if (radiobtn_name.Checked == true)
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Pet WHERE Pet.Pet_Name ='" + txt_petname.Text + "'AND Pet.Owner_Id='" + owner_id + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagrid_mypets.DataSource = dt;
                    con.Close();
                }
                else
                {
                    con.Open();
                    da = new SqlDataAdapter("SELECT * FROM Pet WHERE Pet.Owner_Id='" + owner_id + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    datagrid_mypets.DataSource = dt;
                    con.Close();
                }
            }
            catch (SqlException)
            {
                KryptonMessageBox.Show("Database Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                KryptonMessageBox.Show("Invalid Request,Please Check Your Data Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            openChildForm(new UpdatePets(owner_id));
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (txt_petid.Text.Length == 0)
                {
                    MessageBox.Show("Please enter Pet Id to be removed");
                }
                else if (!(txt_petid.Text.Any(char.IsDigit)))
                {
                    MessageBox.Show("Please enter your Pet Id Number");
                }
                else {
                    cmd = new SqlCommand("DELETE FROM Pet WHERE Pet_Id = '" + txt_petid.Text + "'AND Pet.Owner_Id='" + owner_id+ "'", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        KryptonMessageBox.Show("Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show("Data Deleted Successfully");
                    }
                    else
                    {
                        KryptonMessageBox.Show("Data Could Not be Deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       //MessageBox.Show("Data Could Not be Deleted", "Error");
                    }
                    cmd.Dispose();
                }
                con.Close();
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
