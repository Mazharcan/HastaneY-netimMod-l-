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
    public partial class frmSekreterGiris : Form
    {
        public frmSekreterGiris()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand komut = new SqlCommand("select * from tbl_Sekreter where SekreterTC = @p1 and SekreterSifre = @p2", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@p1", mskTC.Text);
                    komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                    using (SqlDataReader dr = komut.ExecuteReader())    //sekretertc and sekreter sifre yerine , yazdın HATA dikkat et
                    {
                        if (dr.Read())                                                    //sorgulama yaptığımızdan if
                        {
                            frmSekreterDetay frm = new frmSekreterDetay();
                            frm.TCnumara = mskTC.Text;
                            frm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Hatalı TC veya Sifre", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { bgl.baglanti().Close(); }
        }
    }
}
