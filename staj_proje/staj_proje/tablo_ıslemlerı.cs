using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace staj_proje
{
    public partial class tablo_ıslemlerı : Form
    {
        private static string connectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True;MultipleActiveResultSets=True"; 
        private string secilenDB;
        private string secilenTabloAdi;

        public tablo_ıslemlerı()
        {
            InitializeComponent();
        }

        private void tablo_ıslemlerı_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Tablo Adını Güncellemek İstediğiniz Tabloyu Seçiniz", "Uyarı!", MessageBoxButtons.OK);
            listBox1.Font = new Font("Arial", 14);
            listBox1.BackColor = Color.Olive;
            listBox1.ForeColor = Color.White;
            try
            {
                UpdateDatabaseList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string databaseName = listBox1.SelectedItem.ToString();
                textBox1.Text = databaseName;
                //DataGridView'e tablo yüklensin 
                GridTablo(databaseName);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        tablo_olustur tablo_olustur_form;
        private void bt_tbl_olustur_Click(object sender, EventArgs e)
        {
            if (tablo_olustur_form == null || tablo_olustur_form.IsDisposed)
            {

                tablo_olustur_form = new tablo_olustur();
                tablo_olustur_form.database_name_taşı = secilenDB;
                tablo_olustur_form.Show();

            }
            else
            {
                tablo_olustur_form.BringToFront();
            }
        }

        private void btn_tbl_sil_Click(object sender, EventArgs e)
        {
            string tabloAD = textBox1.Text;
            try
            {
                //Tablo siliniyor
                string sorgu = $"DROP TABLE {secilenDB}.[dbo].{tabloAD}";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    
                    using (SqlCommand command = new SqlCommand(sorgu, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Listbox güncelleniyor
                    listBox1.Items.Clear();
                    try
                    {
                        UpdateDatabaseList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }

                    MessageBox.Show("Veritabanı Silindi", "Çıktı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK);
            }
        }

        private void btn_tbl_gncl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(secilenTabloAdi))
            {
                string yeniTabloAdi = textBox1.Text; // Yeni tablo adını alıyoruz

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;

                            // Tablo adını güncellemek için ALTER TABLE komutunu kullanıyoruz
                            string sorgu = $"USE {secilenDB}; EXEC sp_rename '[dbo].{secilenTabloAdi}', '{yeniTabloAdi}'";

                            command.CommandText = sorgu;

                            command.ExecuteNonQuery();
                        }
                    } // connection otomatik olarak burada kapanır

                    MessageBox.Show("Tablo adı güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // DataGridView'ı güncellemek için tabloları yeniden yüklüyoruz
                    UpdateDatabaseList();
                    GridTablo(secilenDB);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek tabloyu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void listBox1_Click(object sender, EventArgs e)
        {
            secilenDB = listBox1.SelectedItem.ToString();
            tablo_olustur tablo_Olustur = new tablo_olustur();
            tablo_Olustur.database_name_taşı = secilenDB;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            secilenDB = listBox1.SelectedItem.ToString();
            tablo_olustur tablo_Olustur = new tablo_olustur();
            tablo_Olustur.database_name_taşı = secilenDB;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];

                    // DataGridView'dan seçilen tablo adını alıyoruz
                    secilenTabloAdi = selectedRow.Cells["TABLE_NAME"].Value.ToString();
                    textBox1.Text = secilenTabloAdi; // TextBox'a tablo adını yazdırma
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];

                    // DataGridView'dan seçilen tablo adını alıyoruz
                    secilenTabloAdi = selectedRow.Cells["TABLE_NAME"].Value.ToString();
                    textBox1.Text = secilenTabloAdi; // TextBox'a tablo adını yazdırma
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateDatabaseList()
        {
            listBox1.Items.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT name FROM sys.databases WHERE name NOT IN ('tempdb', 'master', 'model', 'msdb')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string databaseName1 = reader.GetString(0);
                                listBox1.Items.Add(databaseName1);
                            }
                        }
                    }
                } // connection otomatik olarak burada kapanır
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void GridTablo(string databaseName)
        {
            //Listbox içindeki veritabanının içindeki tabloları açan sorgusu
            string sqlkomut = $"SELECT TABLE_NAME FROM {databaseName}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlkomut, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // DataGridView'e tablo verilerini yükle
                        dataGridView1.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
