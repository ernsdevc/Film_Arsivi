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

namespace Film_Arsivi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=MACHINEX;Initial Catalog=DBFilmArsivi;Integrated Security=True");

        void Filmler()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLFilmler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Filmler();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLFilmler (FilmAdi,Kategori,Link) VALUES(@P1,@P2,@P3)", baglanti);
            komut.Parameters.AddWithValue("@P1", txtFilmAdi.Text);
            komut.Parameters.AddWithValue("@P2", txtKategori.Text);
            komut.Parameters.AddWithValue("@P3", txtLink.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Film listeye eklendi","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Filmler();
        }

        string link;

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            link = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            webBrowser1.Navigate(link);
        }

        private void btnHakkimizda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu Proje Erensu Deveci tarafından 6 Aralık 2023 tarihinde kodlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTamEkran_Click(object sender, EventArgs e)
        {
            FrmTamEkran fr = new FrmTamEkran(link);
            fr.Show();
        }
    }
}
