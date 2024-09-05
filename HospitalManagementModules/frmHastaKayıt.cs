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
    public partial class frmHastaKayıt : Form
    {
        public frmHastaKayıt()
        {
            InitializeComponent();
        }
        //sınfı çağırmam gerek
        SqlBaglanti bgl = new SqlBaglanti();  //yani SqlBaglanti sınıfnda ki baglan metoduna ulaşabilicem
        private void btnKayıt_Click(object sender, EventArgs e)
        {
            //baglanti açık olduğu için zatan baglanti.open() yazmadım ve bgl.baglanti bgl nesnesinden ürettiğim baglanti metodu ve bgl nesnesi baglanti sınfındaki özellikleri sağlıyor
            SqlCommand komut = new SqlCommand("insert into tbl_Hasta (HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet)values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTC.Text);
            komut.Parameters.AddWithValue("@p4", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p5", txtSifre.Text);
            komut.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti();                 //baglantiyi direkt kapatamıyorum sınfın içinde ki metoda ulaşıp kapatıyorum bgl.baglanti
            MessageBox.Show("Kaydınız Gerçekleşmiştir Şifreniz: " + txtSifre.Text,"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);


        }
    }
}
