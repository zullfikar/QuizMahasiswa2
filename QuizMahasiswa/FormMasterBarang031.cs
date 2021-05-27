using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuizMahasiswa
{
    public partial class FormMasterBarang031 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-CIJN2E2A\SQLEXPRESS;Initial Catalog=QuizAgit;Integrated Security=True;");
        public FormMasterBarang031()
        {
            InitializeComponent();
        }

        DatabaseZulDataContext db = new DatabaseZulDataContext();
        void LoadData()
        {
            var st = from tb in db.tbl_barangs select tb;
            dataGridView1.DataSource = st;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            string Nama = txtNama.Text;
            int harga = int.Parse(txtHarga.Text);
            int stok = int.Parse(txtStok.Text);
            string namasupplier = cmbNamaSup.Text;

            var data = new tbl_barang
            {
                id_barang = id,
                nama_barang = Nama,
                harga = harga,
                stok = stok,
                nama_supplier = namasupplier
            };

            db.tbl_barangs.InsertOnSubmit(data);
            db.SubmitChanges();
            MessageBox.Show("Data Berhasil Disimpan");
            txtHarga.Clear();
            txtNama.Clear();
            txtStok.Clear();
            LoadData();
            IdOtomatis();
        }
        public void IdOtomatis()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select isnull(max (cast (id_barang as int)), 0) +1 from tbl_barang", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtID.Text = dt.Rows[0][0].ToString();
            LoadData();
            con.Close();
        }

        private void FormMasterBarang031_Load(object sender, EventArgs e)
        {
            IdOtomatis();
        }
    }
}
