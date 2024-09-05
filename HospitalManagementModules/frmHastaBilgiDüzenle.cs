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
    public partial class frmHastaBilgiDüzenle : Form
    {
        SqlBaglanti bgl = new SqlBaglanti();
        public frmHastaBilgiDüzenle()
        {
            InitializeComponent();
        }
        public string TCno;
        
        private void frmHastaBilgiDüzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCno;
            SqlCommand komut = new SqlCommand("select * from tbl_hasta where HastaTC = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();             // [0] olmamasını sebebi 0. indexte hastaID var oda zaten otomatik artıyor
                txtSoyad.Text = dr[2].ToString();
                mskTelefon.Text = dr[4].ToString();        //tc no zaten yazıyor orda orda 
                txtSifre.Text = dr[5].ToString();
                cmbCinsiyet.Text = dr[6].ToString();
                bgl.baglanti().Close();
            }
        }

        private void btnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update tbl_hasta set HastaAd = @p1,HastaSoyad = @p2,HastaTelefon = @p3,HastaSifre = @p4,HastaCinsiyet = @p5 where HastaTC = @p6", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtAd.Text);
            komut2.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", mskTelefon.Text);
            komut2.Parameters.AddWithValue("@p4", txtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6", mskTC.Text);
            komut2.ExecuteNonQuery();                                           //insert update delete yaptığın işlemleri gerçkeleştirmek için kullandığın komut
            bgl.baglanti();
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
