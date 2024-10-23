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
    public partial class frmDoktorBrans : Form
    {
        public frmDoktorBrans()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        private void frmDoktorBrans_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection connection = bgl.baglanti()) // Bağlantıyı using içinde açıyoruz
                {
                    // SQL sorgusunu ve veritabanı bağlantısını using içinde aç
                    using (SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar", connection))
                    {
                        // DataTable'ı doldur
                        da.Fill(dt);
                    }
                }
                // DataTable'ı DataGridView'e ata
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand komut = new SqlCommand("insert into tbl_branslar (bransad) values (@b1)", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@b1", txtBrans.Text);
                    komut.ExecuteNonQuery();
                }
                MessageBox.Show("Brans Eklenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { bgl.baglanti(); } 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int secilendeger = dataGridView1.SelectedCells[0].RowIndex;
                txtID.Text = dataGridView1.Rows[secilendeger].Cells[0].Value.ToString();
                txtBrans.Text = dataGridView1.Rows[secilendeger].Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand komut2 = new SqlCommand("delete from tbl_branslar where bransId = @b1", bgl.baglanti()))
                {
                    komut2.Parameters.AddWithValue("@b1", txtID.Text);
                    komut2.ExecuteNonQuery();
                }
                MessageBox.Show("Brans Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { bgl.baglanti(); }

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand komut3 = new SqlCommand("update tbl_branslar set bransad=@p1 where bransıd=@p2", bgl.baglanti()))
                {
                    komut3.Parameters.AddWithValue("@p1", txtBrans.Text);
                    komut3.Parameters.AddWithValue("@p2", txtID.Text);
                    komut3.ExecuteNonQuery();
                }
                MessageBox.Show("Brans Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { bgl.baglanti(); }
        }
    }
}
