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
    public partial class frmDoktorBrans : Form
    {
        public frmDoktorBrans()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        private void frmDoktorBrans_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); 
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_branslar (bransad) values (@b1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@b1", txtBrans.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Brans Eklenmiştir","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilendeger = dataGridView1.SelectedCells[0].RowIndex;    
            txtID.Text = dataGridView1.Rows[secilendeger].Cells[0].Value.ToString();
            txtBrans.Text = dataGridView1.Rows[secilendeger].Cells[1].Value.ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("delete from tbl_branslar where bransId = @b1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@b1", txtID.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Brans Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("update tbl_branslar set bransad=@p1 where bransıd=@p2", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",txtBrans.Text);
            komut3.Parameters.AddWithValue("@p2", txtID.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Brans Güncellendi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
    }
}
