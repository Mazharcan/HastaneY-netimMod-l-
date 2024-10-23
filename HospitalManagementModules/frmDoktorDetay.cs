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
            try
            {
                using (SqlConnection connection = bgl.baglanti()) // bağlantı açılır
                {
                    using (SqlCommand komut = new SqlCommand("select doktorad, doktorsoyad from tbl_doktor where doktortc = @p1", connection))
                    {
                        komut.Parameters.AddWithValue("@p1", lblTC.Text);

                        // SqlDataReader kullanımı
                        using (SqlDataReader dr = komut.ExecuteReader())
                        {
                            if (dr.Read()) // Veriler varsa
                            {
                                lblAdSoyad.Text = dr[0] + " " + dr[1]; // Doktor adı ve soyadını label'a atar
                            }
                        }
                    }
                    //using bloğu içinde iki farklı using blokları açtık
                    // RANDEVULAR
                    using (SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Randevular where RandevuDoktor = @p2", connection))
                    {
                        // Parametre ekleme (SQL enjeksiyonundan korunmak için)
                        da.SelectCommand.Parameters.AddWithValue("@p2", lblAdSoyad.Text);

                        // DataTable ile verileri doldur ve DataGridView'e ata
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvRandevuListesi.DataSource = dt;
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            this.Close();
            Giris giris = new Giris();
            giris.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvRandevuListesi.SelectedCells[0].RowIndex;
            rchSikayet.Text = dgvRandevuListesi.Rows[secilen].Cells[7].Value.ToString();
            //datagridin.satırları içerisinde secilen satırın(rows[secilen]).hücreleri içerisinde 7. hücre (cells[7]) randevu tablosundaki 7. sütun
        }
    }
    
}
