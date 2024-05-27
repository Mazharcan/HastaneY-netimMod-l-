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
using System.Runtime.InteropServices;

namespace HastaneYönetimModülü
{
    public partial class frmSekreterDetay : Form
    {
        public frmSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
      

        SqlBaglanti bgl = new SqlBaglanti();
        private void frmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCnumara;
           


            //AD SOYAD
            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad from tbl_Sekreter where SekreterTC = @p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while(dr1.Read()) 
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti();

            //BRANŞLARI DATAGRİDE AKTARMA
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar",bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //DOKTORLARI DATAGRİDE AKTARMA
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (DoktorAd +' '+ DoktorSoyad) as 'Doktorlar',DoktorBrans from tbl_Doktor", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //BRANSI COMBOBAXA AKATARMA      formun loadına yaz kaydeet butonunun içine değil
            SqlCommand komut2 = new SqlCommand("Select BransAd From tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into tbl_randevular(RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)",bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");


        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select doktorad,doktorsoyad from tbl_doktor where doktorbrans = @b1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@b1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while ( dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();

        }

        private void btnDuyuruOluştur_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("insert into tbl_duyurular(duyuru) values ( @d1)", bgl.baglanti());
            komut4.Parameters.AddWithValue("@d1", rchDuyuru.Text);
            komut4.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyurunuz Oluşturuldu");
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            frmDoktorPaneli frmDoktorPaneli = new frmDoktorPaneli();
            frmDoktorPaneli.ShowDialog();
            
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            frmDoktorBrans frb = new frmDoktorBrans();
            frb.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            frmRandevuListesi frl = new frmRandevuListesi();
            frl.Show();

        }

        private void btnDuyurlar_Click(object sender, EventArgs e)
        {
            frmDoktorDuyurular fr = new frmDoktorDuyurular();
            fr.Show();
        }
    }
}
