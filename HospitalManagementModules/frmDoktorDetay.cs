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

namespace HospitalManagementModules
{
    public partial class frmDoktorDetay : Form
    {
        public frmDoktorDetay()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        public string TC;
        private void frmDoktorDetay_Load(object sender, EventArgs e)
        {
            //DOKTOR TC
            lblTC.Text = TC;
            //tc yi nerden alıcak doktorgitiş panelinde frm kısmında fr.TC = mskTC.Text; kodunu yazıcan

            //DOKTOR AD SOYAD
            SqlCommand komut = new SqlCommand("select doktorad,doktorsoyad from tbl_doktor where doktortc = @p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())                 //if sadece girişlerde parametrelerin birbirne eşitliğini sorgulamada
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }

            //RANDEVULAR
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Randevular where RandevuDoktor = '" + lblAdSoyad.Text + "'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            frmDoktorBilgiDüzenle fr = new frmDoktorBilgiDüzenle();
            fr.TCNO = lblTC.Text;
            fr.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            frmDoktorDuyurular fr = new frmDoktorDuyurular();
            fr.Show();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            //datagridin.satırları içerisinde secilen satırın(rows[secilen]).hücreleri içerisinde 7. hücre (cells[7]) randevu tablosundaki 7. sütun

        }
    }
    
}
