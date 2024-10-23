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
    public partial class frmDoktorGiris : Form
    {
        public frmDoktorGiris()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand komut = new SqlCommand("select * from tbl_doktor where doktortc = @p1 and doktorsifre = @p2", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@p1", mskTC.Text);
                    komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            frmDoktorDetay fr = new frmDoktorDetay();
                            fr.TC = mskTC.Text;
                            fr.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "Bİlgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                       
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
