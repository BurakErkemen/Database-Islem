using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace staj_proje
{
    public partial class backup : Form
    {
        public backup()
        {
            InitializeComponent();
        }

        private List<DateTime> backupDates = new List<DateTime>();
        private string serverName; // Sunucu adı
        private string backupPath; // Yedekleme dosyasının kaydedileceği yol
        private string connectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True;MultipleActiveResultSets=True";
        private void backup_Load(object sender, EventArgs e)
        {
            txtbox_server.Text = @"DESKTOP-1TTOTC5\SQLEXPRESS";
            listBox1.Font = new Font("Arial", 14);
            listBox1.BackColor = Color.Olive;
            listBox1.ForeColor = Color.White;
            UpdateBackupList();
        }

        private void btn_path_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Backup Files|*.bak";
            saveFileDialog.Title = "Save Backup File";
            saveFileDialog.FileName = DateTime.Now.ToString("dd/MM/yyy-HH.mm") + ".bak";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                backupPath = saveFileDialog.FileName;
                txtbox_dosyayolu.Text = backupPath;
            }
        }

        private void onayla_btn_Click(object sender, EventArgs e)
        {
            serverName = txtbox_server.Text;
            string username = txtbox_username.Text;
            string password = txtbox_sifre.Text;
            string connString;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                connString = $"Data Source={serverName};User ID={username};Password={password};Integrated Security=False;MultipleActiveResultSets=True";
            }
            else
            {
                connString = $"Data Source={serverName};Integrated Security=True;MultipleActiveResultSets=True";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string databaseQuery = "SELECT name FROM sys.databases WHERE state_desc = 'ONLINE' AND name NOT IN ('tempdb', 'master', 'model', 'msdb')";

                    using (SqlCommand command = new SqlCommand(databaseQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string databaseName = reader.GetString(0);
                                string backupQuery = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupPath}'";

                                using (SqlCommand backupCommand = new SqlCommand(backupQuery, connection))
                                {
                                    backupCommand.ExecuteNonQuery();
                                    MessageBox.Show($"{databaseName} veritabanı başarıyla yedeklendi.", "Bildiri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
                UpdateBackupList();
                MessageBox.Show("Yedekleme işlemi tamamlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBackupList()
        {
            listBox1.Items.Clear();
            backupDates.Clear(); // Liste temizleniyor
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string backupHistoryQuery = "SELECT TOP 10 backup_start_date FROM msdb.dbo.backupset ORDER BY backup_start_date DESC";

                    using (SqlCommand command = new SqlCommand(backupHistoryQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime backupDate = reader.GetDateTime(0);

                                // Check if the backup date is already in the list
                                if (!backupDates.Contains(backupDate.Date))
                                {
                                    listBox1.Items.Add(backupDate.ToString());
                                    backupDates.Add(backupDate.Date);
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
