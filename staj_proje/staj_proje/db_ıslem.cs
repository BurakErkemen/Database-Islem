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

namespace staj_proje
{
    public partial class db_ıslem : Form
    {
        public db_ıslem()
        {
            InitializeComponent();
        }
        private string connectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True";
        private string Secılen_DB;
        private void db_ıslem_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Adını Güncellemek İstediğiniz Veri Tabanına Çift Tıklayınız", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Focus();
            listBox1.Enabled = true;
            listBox1.Font = new System.Drawing.Font("Arial", 14,FontStyle.Bold);
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Secılen_DB = listBox1.SelectedItem.ToString();
            textBox1.Text = Secılen_DB;
        }

        private void btn_olustur_Click(object sender, EventArgs e)
        {
            string DBad = textBox1.Text;
            try
            {
                //Veritabanı oluşturuluyor
                string sorgu = $"CREATE DATABASE {DBad}";

                string sql = "SELECT name FROM sys.databases WHERE name NOT IN ('tempdb', 'master', 'model', 'msdb')";
                listBox1.Items.Clear();
                using(SqlConnection  connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string databaseName = reader.GetString(0);
                                // Veritabanı adını kullanarak istediğiniz işlemi yapabilirsiniz
                                // Örneğin, veritabanı adını bir ListBox'a ekleyebilirsiniz:
                                listBox1.Items.Add(databaseName);
                                listBox1.Refresh();
                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand(sorgu, connection))
                    {
                        command.ExecuteNonQuery();
                        listBox1.Refresh();
                    }
                    UpdateDatabaseList();
                    MessageBox.Show("Veritabanı Oluşturuldu", "Çıktı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK);
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            string DBad = textBox1.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Veritabanı adı değiştiriliyor
                    string sorgu = $"ALTER DATABASE {Secılen_DB} MODIFY NAME = {DBad}";

                    using (SqlCommand command = new SqlCommand(sorgu, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Listbox güncelleniyor
                    UpdateDatabaseList();

                    MessageBox.Show("Veritabanı Adı Değiştirildi", "Çıktı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK);
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            string DBad = textBox1.Text;
            try
            {
                //Veritabanı siliniyor
                string sorgu = $"DROP DATABASE {DBad}";

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
                        string sql1 = "SELECT name FROM sys.databases WHERE name NOT IN ('tempdb', 'master', 'model', 'msdb')";

                        using (SqlCommand command = new SqlCommand(sql1, connection))
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
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }

                MessageBox.Show("Veritabanı Silindi", "Çıktı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK);
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Secılen_DB = listBox1.SelectedItem.ToString();
                textBox1.Text = Secılen_DB;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK);
            }   
        }
    }
}
