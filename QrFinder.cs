using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;

namespace Pet_Clinic_Project
{
    public partial class QrFinder : KryptonForm
    {
        private string petname;
        private string fullname;
        private int owner_id;
        private string tp;
        private string address;
        public QrFinder(string fullname,int owner_id, string tp, string address)
        {
            InitializeComponent();
            this.fullname = fullname;
            this.owner_id = owner_id;
            this.tp = tp;
            this.address = address;

        }
        SqlConnection con = new SqlConnection("Data Source=LAKSHAN-PC;Initial Catalog=PetClinic;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader da;

        
        private void QrFinder_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT Pet_Name FROM Pet WHERE Pet.Owner_Id='" + owner_id + "'", con);
                da = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Pet_Name",typeof(string));
                dt.Load(da);
                cmb_name.ValueMember = "Pet_Name";
                cmb_name.DataSource = dt;
                con.Close();

            }
            catch (Exception)
            { 
            
            }
            
        }

        private void btn_qrgenerate_Click(object sender, EventArgs e)
        {

            petname = this.cmb_name.GetItemText(this.cmb_name.SelectedItem);
            
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pbox_qr.Image = qrcode.Draw("Owner Name: " + fullname + System.Environment.NewLine + "Telephone: " + tp + System.Environment.NewLine + "Address: " + address + System.Environment.NewLine + "Pet Name: " + petname + "", 50);
        }

        private void btn_qrprint_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument pDoc = new PrintDocument();
            pDoc.PrintPage += PrintPicture;
            pd.Document = pDoc;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                pDoc.Print();
            }
        }
        private void PrintPicture(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(pbox_qr.Width, pbox_qr.Height);
            pbox_qr.DrawToBitmap(bmp, new Rectangle(0, 0, pbox_qr.Width, pbox_qr.Height));
            e.Graphics.DrawImage(bmp, 0, 0);
            bmp.Dispose();
        }
    }
}
