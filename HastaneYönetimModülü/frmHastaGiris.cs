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
    public partial class frmHastaGiris : Form
    {
        public frmHastaGiris()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmHastaKayıt fr = new frmHastaKayıt();
            fr.Show();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            //giriş yp için tc ve şifresi aynı satırda olanlar için parametreyi ona göre atıyorsun
            SqlCommand komut = new SqlCommand("select * from tbl_Hasta where HastaTC = @p1 and HastaSifre = @p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader rd = komut.ExecuteReader();       //rd komuttan gelen verileri oku
            //while : verileri okuyup yazdırmak için ama if : verinin doğruluğunun kontrolu
            if (rd.Read())
            {
                frmHastaDetay frh = new frmHastaDetay();
                frh.tc = mskTC.Text;
                frh.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC Kimlik No veya hatalı Şifre");
                mskTC.Text = "";
                txtSifre.Text = "";
                mskTC.Focus();
            }
            bgl.baglanti().Close();   //baglantıyı zaten açmıştık şimdi onu !!!!
        }
    }
}
