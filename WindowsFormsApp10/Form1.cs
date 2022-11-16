using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'kitaplıkDataSet.Kitap3' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.kitap3TableAdapter.Fill(this.kitaplıkDataSet.Kitap3);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection baglantim = new SqlConnection("Data Source=EXCALIBUR4;Initial Catalog=kitaplık;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dk = new SqlDataAdapter("select * from Kitap3", baglantim);
            DataSet ds = new DataSet();
            dk.Fill(ds, "Kitap3");
            dataGridView1.DataSource = ds.Tables["Kitap3"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            SqlCommand kayitekle = new SqlCommand("insert into Kitap3 (kitapNo,KitapAd,KitapYazar,KitapTuru,KitapFiyat,Vergi)" +
                "values(@k1,@k2,@k3,@k4,@k5,@k6)", baglantim);
            kayitekle.Parameters.AddWithValue("@k1", textBox1.Text);
            kayitekle.Parameters.AddWithValue("@k2", textBox2.Text);
            kayitekle.Parameters.AddWithValue("@k3", textBox3.Text);
            kayitekle.Parameters.AddWithValue("@k4", textBox4.Text);
            kayitekle.Parameters.AddWithValue("@k5", textBox5.Text);
            kayitekle.Parameters.AddWithValue("@k6", textBox6.Text);


            kayitekle.ExecuteNonQuery();
            MessageBox.Show("KAYIT EKLENDİ");
            baglantim.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            SqlCommand kayitsil = new SqlCommand("delete from kitap3 where kitapAd=@ad", baglantim);
            kayitsil.Parameters.AddWithValue("@adi", textBox1.Text);
            //girilen kitap adini silecek.
            kayitsil.ExecuteNonQuery();
            MessageBox.Show("Kayit Silindi");
            baglantim.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            SqlCommand kayitgüncelleme = new SqlCommand("update kitap3 set  kitapNo= @m1, kitapAd=@m2, kitapYazar=@m3, kitapTur=@m4, KitapFiyat=@m5, Vergi=@m6 where kitapNo= @m1", baglantim);

            kayitgüncelleme.Parameters.AddWithValue("@m1", textBox1.Text);
            kayitgüncelleme.Parameters.AddWithValue("@m2", textBox2.Text);
            kayitgüncelleme.Parameters.AddWithValue("@m3", textBox3.Text);
            kayitgüncelleme.Parameters.AddWithValue("@m4", textBox4.Text);
            kayitgüncelleme.Parameters.AddWithValue("@m5", textBox5.Text);
            kayitgüncelleme.Parameters.AddWithValue("@m6", textBox6.Text);

            kayitgüncelleme.ExecuteNonQuery();
            MessageBox.Show("Güncelleme Gerçekleşti");
            baglantim.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            SqlCommand hesapla = new SqlCommand("Select Max(KitapFiyat) from Kitap3", baglantim);
            SqlDataReader okuma = hesapla.ExecuteReader();
            while (okuma.Read())
            {
                label3.Text = okuma[0].ToString();
                //0.indeks=max fiyat başka bir okuma işlemi yok!

            }
            baglantim.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            SqlCommand hesapla1=new SqlCommand("select count(kitapNo) from kitap3", baglantim);
            SqlDataReader okuma1=hesapla1.ExecuteReader();
            while (okuma1.Read())
            {
                label2.Text = okuma1[0].ToString();
                //0.Indeks = count başka bir okuma işlemi yok.
            }
            baglantim.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            string kitapNo = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string kitapAd = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string kitapYazar = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string kitapTur = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            string kitapFiyat = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            string Vergi = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

            textBox1.Text = kitapNo;
            textBox2.Text = kitapAd;
            textBox3.Text = kitapYazar;
            textBox4.Text = kitapTur;
            textBox5.Text = kitapFiyat;
            textBox6.Text = Vergi;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlDataAdapter db = new SqlDataAdapter("select*from kitap3 where kitapAd='" + textBox7.Text + "'", baglantim);
            DataSet ds = new DataSet();
            db.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            textBox7.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            SqlCommand hesapla1= new SqlCommand("select sum(kitapFiyat) from kitap3", baglantim);
            SqlDataReader okuma1= hesapla1.ExecuteReader();
            while(okuma1.Read())
            {
                label4.Text = okuma1[0].ToString();
            }
            baglantim.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            SqlCommand hesapla1 = new SqlCommand("select avg(kitapFiyat) from kitap3", baglantim);
            SqlDataReader okuma1 = hesapla1.ExecuteReader();
            while (okuma1.Read())
            {
                label9.Text = okuma1[0].ToString();
            }
            baglantim.Close();
        }

     

    }
}
