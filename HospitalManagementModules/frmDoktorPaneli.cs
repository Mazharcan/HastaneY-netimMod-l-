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
    public partial class frmDoktorPaneli : Form
    {
        public frmDoktorPaneli()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();

        private void frmDoktorPaneli_Load(object sender, EventArgs e)
        {
            try
            {
                //DOKTORLARI DATAGRİDE AKTARMA
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter da1 = new SqlDataAdapter("select * from tbl_Doktor", bgl.baglanti()))
                {
                    da1.Fill(dt1);
                }
                dataGridView1.DataSource = dt1;

                //BRASNLARI COMBOBOXA AKTARMA
                using (SqlCommand komut2 = new SqlCommand("Select BransAd From tbl_Branslar", bgl.baglanti()))
                {
                    using (SqlDataReader dr2 = komut2.ExecuteReader())
                    {
                        while (dr2.Read())
                        {
                            cmbBrans.Items.Add(dr2[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                bgl.baglanti().Close();
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand komut = new SqlCommand("insert into tbl_Doktor (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@d1", txtAd.Text);
                    komut.Parameters.AddWithValue("@d2", txtSoyad.Text);
                    komut.Parameters.AddWithValue("@d3", cmbBrans.Text);
                    komut.Parameters.AddWithValue("@d4", mskTC.Text);
                    komut.Parameters.AddWithValue("@d5", txtSifre.Text);
                    komut.ExecuteNonQuery();
                }
                MessageBox.Show("Doktor Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                bgl.baglanti().Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagridde satıra tıkladığında bilgileri ilgili yerlere aktarması için yazıcağın kod
            int secilen = dataGridView1.SelectedCells[0].RowIndex;      //seçilen hücrenin 0.sütununa göre satır indexini al
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand komut = new SqlCommand("delete from tbl_doktor where doktorTc = @p1", bgl.baglanti())) //tc kimlik değeri bu olanı tablodan sil
                {
                    komut.Parameters.AddWithValue("@p1", mskTC.Text);
                    komut.ExecuteNonQuery();
                }
                MessageBox.Show("Doktor Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                bgl.baglanti().Close();
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand komut2 = new SqlCommand("update tbl_doktor set doktorad=@d1,doktorsoyad=@d2,doktorbrans=@d3,doktorsifre=@d5 where doktortc = @d4 ", bgl.baglanti()))
                {
                    komut2.Parameters.AddWithValue("@d1", txtAd.Text);
                    komut2.Parameters.AddWithValue("@d2", txtSoyad.Text);
                    komut2.Parameters.AddWithValue("@d3", cmbBrans.Text);
                    komut2.Parameters.AddWithValue("@d4", mskTC.Text);
                    komut2.Parameters.AddWithValue("@d5", txtSifre.Text);
                    komut2.ExecuteNonQuery();
                }
                MessageBox.Show("Doktor Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                bgl.baglanti().Close();
            }
        }
    }
}
