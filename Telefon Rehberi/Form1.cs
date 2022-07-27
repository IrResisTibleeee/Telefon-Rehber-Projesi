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


namespace Telefon_Rehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        SqlConnection baglanti = new SqlConnection(@"Data Source = IRRESISTIBLE\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da =new SqlDataAdapter("Select * From KISILER ", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void temizle()
        {
            TxtID.Text = "";
            TxtAD.Text = "";
            TxtSOYAD.Text = "";
            TxtTELEFON.Text = "";
            TxtMAIL.Text = "";
            TxtAD.Focus();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into KISILER (AD,SOYAD,TELEFON,MAIL) values (@P1,@P2,@P3,@P4)",baglanti);

            komut.Parameters.AddWithValue("@P1", TxtAD.Text);
            komut.Parameters.AddWithValue("@P2", TxtSOYAD.Text);
            komut.Parameters.AddWithValue("@P3", TxtTELEFON.Text);
            komut.Parameters.AddWithValue("@P4", TxtMAIL.Text);
            komut.Parameters.AddWithValue("@P5", TxtFOTOGRAF.Text);
            komut.Parameters.AddWithValue("@P6", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kişi Rehbere Kayıt Edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAD.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSOYAD.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtTELEFON.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtMAIL.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtFOTOGRAF.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

            pictureBox1.ImageLocation = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            DialogResult onay = new DialogResult();
            onay = MessageBox.Show("Bu Kişiyi Silmek İstediğinizden Emin Misiniz ?", "UYARI", MessageBoxButtons.YesNo);
            if (onay == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete From KISILER where ID=" + TxtID.Text, baglanti);
                komut.Parameters.AddWithValue("@P1", TxtID.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi Rehberden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (onay == DialogResult.No)
            {
                MessageBox.Show("TELEFON SİLİNMEDİ");
            }
            listele();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update KISILER set AD=@P1 ,SOYAD=@P2 ,TELEFON=@P3 ,MAIL=@P4, FOTOGRAF=@P5 where ID=@P6", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAD.Text);
            komut.Parameters.AddWithValue("@P2", TxtSOYAD.Text);
            komut.Parameters.AddWithValue("@P3", TxtTELEFON.Text);
            komut.Parameters.AddWithValue("@P4", TxtMAIL.Text);
            komut.Parameters.AddWithValue("@P5", TxtFOTOGRAF.Text);
            komut.Parameters.AddWithValue("@P6", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Bilgsi Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            TxtFOTOGRAF.Text = openFileDialog1.FileName;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            {
                baglanti.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter ara = new SqlDataAdapter("select * from KISILER where AD like '%" + Arama.Text + "%'", baglanti);
                ara.Fill(dt);
                baglanti.Close();
                dataGridView1.DataSource = dt;


            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
