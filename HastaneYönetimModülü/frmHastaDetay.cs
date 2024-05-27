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

namespace HastaneYönetimModülü
{
    public partial class frmHastaDetay : Form
    {
        public frmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        SqlBaglanti bgl = new SqlBaglanti();

        private void frmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;                                                 //hastadetay formunda ki tc labelına publicte ürettiğim ve giriş formunda ki atanan tc yi yaz
                                                                             //giriş formunda da frh nesnesi aracılığıyla public old her formda erişebildik yani giriş formudna ki tc yi hasta deraydaki  public olan tc ye atadım

            //ADSOYAD ÇEKME DATAGRİD
            SqlCommand komut = new SqlCommand("select HastaAd, HastaSoyad from tbl_Hasta where HastaTC = @p1" , bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);                //lbltc de yazan değere eşit olan ad soyadı getir
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())      //listelemek için
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];                      //yazdığım satırda zaten string old satırı ekstra stirnge döndürmeme gerek kalmadı
            }
            bgl.baglanti().Close();
            //RANDEVU GEÇMİŞİ
            DataTable dt = new DataTable();                                //dataadapter dataverid veri aktarmak için kullandığın command
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where HastaTC ="+tc,bgl.baglanti());
            da.Fill(dt);                                                  //da yani data adapterının içini doldur tablodan gelicek olan değerler(dt)
            dataGridView2.DataSource = dt;                                //datagrid dt den gelen değerle doldur

            //BRANŞLARI ÇEKME
            SqlCommand komut2 = new SqlCommand("select BransAd from tbl_branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti();

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //branşı seçtiğimde doktor verileri gelsin
            cmbDoktor.Items.Clear();                                              //biz comboboxta bişey seçtiğimizde comboboxtaki verileri silsin
            SqlCommand komut3 = new SqlCommand("select DoktorAd, DoktorSoyad from tbl_Doktor where DoktorBrans = @p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //doktoru seçtiğinde branşı ve doktoru şu olan randevular gelicek
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where RandevuBrans = '" + cmbBrans.Text+ "'" + " and RandevuDoktor = '"+cmbDoktor.Text+"' and  randevudurum = 0", bgl.baglanti());  //Sql serverda string ifadeler ' ile yazıldığından öyle yazdık !!!
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void lnkBilgiDüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmHastaBilgiDüzenle frm = new frmHastaBilgiDüzenle();
            //açıldığında bilgliler otomatik olarak dolu gelmesi için 
            frm.TCno = lblTC.Text;

            frm.Show();
            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //hastada randevuya tuıkladığında bilgileri aktarsın //HATA
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("update tbl_randevular SET randevudurum = 1,HastaTC = @r1,hastasikayet = @r2 where randevuID = @r3",bgl.baglanti());
            komut4.Parameters.AddWithValue("@r1", lblTC.Text);
            komut4.Parameters.AddWithValue("@r2", rctSikayet.Text);
            komut4.Parameters.AddWithValue("@r3", txtID.Text);
            komut4.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
    }
}
