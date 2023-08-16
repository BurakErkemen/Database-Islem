using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace staj_proje
{
    public partial class homepage : Form
    {
        private string connectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True;MultipleActiveResultSets=True";
        private backup backup;
        private string databaseName;

        private db_ıslem db_islem_form;
        private tablo_ıslemlerı tablo_islemleri_form;
        private veri_ıslemelerı veri_Islemelerı;
        private sql_sorgusu_yaz sql_Sorgusu_Yaz;

        public homepage()
        {
            InitializeComponent();
        }

        public string Data { get; set; }

        private void homepage_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox1.Focus();
            kullanıcı_label.Text = Data ?? "Admin";

            listBox1.Font = new Font("Arial", 14);
            listBox1.BackColor = Color.Olive;
            listBox1.ForeColor = Color.White;

            UpdateDatabaseList();
        }

        private void UpdateDatabaseList()
        {
            listBox1.Items.Clear();
            try
            {
                string sql = "SELECT name FROM sys.databases WHERE name NOT IN ('tempdb', 'master', 'model', 'msdb')";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string databaseName1 = reader.GetString(0);
                                listBox1.Items.Add(databaseName1);
                            }
                            reader.Close();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                databaseName = listBox1.SelectedItem.ToString();
                textBox1.Text = databaseName;
                GridTablo(databaseName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void GridTablo(string databaseName1)
        {
            string sqlkomut = $"SELECT TABLE_NAME FROM {databaseName1}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlkomut, connection))
                    {
                        using(SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            using(DataTable table = new DataTable())
                            {
                                adapter.Fill(table);
                                dataGridView1.DataSource = table;
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    int columnIndex = dataGridView1.SelectedCells[0].ColumnIndex;
                    DataGridViewCell selectedCell = dataGridView1.Rows[rowIndex].Cells[columnIndex];

                    string cellValue = selectedCell.Value.ToString();
                    textBox1.Text = cellValue;

                    string sorgu = $"SELECT * FROM {databaseName}.dbo.{cellValue}";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sorgu, connection))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                using (DataTable table = new DataTable())
                                {
                                    adapter.Fill(table);
                                    dataGridView1.DataSource = table;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK);
            }
        }

        private void btn_db_Click(object sender, EventArgs e)
        {
            if (db_islem_form == null || db_islem_form.IsDisposed)
            {
                db_islem_form = new db_ıslem();
                db_islem_form.Show();
            }
            else
            {
                db_islem_form.BringToFront();
            }
        }

        private void btn_tbl_Click(object sender, EventArgs e)
        {
            if (tablo_islemleri_form == null || tablo_islemleri_form.IsDisposed)
            {
                tablo_islemleri_form = new tablo_ıslemlerı();
                tablo_islemleri_form.Show();
            }
            else
            {
                tablo_islemleri_form.BringToFront();
            }
        }

        private void btn_data_Click(object sender, EventArgs e)
        {
            if (veri_Islemelerı == null || veri_Islemelerı.IsDisposed)
            {
                veri_Islemelerı = new veri_ıslemelerı();
                veri_Islemelerı.Show();
            }
            else
            {
                veri_Islemelerı.BringToFront();
            }
        }

        private void btn_sqlkomut_Click(object sender, EventArgs e)
        {
            if (sql_Sorgusu_Yaz == null || sql_Sorgusu_Yaz.IsDisposed)
            {
                sql_Sorgusu_Yaz = new sql_sorgusu_yaz();
                sql_Sorgusu_Yaz.Show();
            }
            else
            {
                sql_Sorgusu_Yaz.BringToFront();
            }
        }

        private void btn_backup_Click(object sender, EventArgs e)
        {
            if (backup == null || backup.IsDisposed)
            {
                backup = new backup();
                backup.Show();
            }
            else
            {
                backup.BringToFront();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    string tableName = selectedRow.Cells["TABLE_NAME"].Value.ToString();

                    // Tabloyu açmak için veritabanı bağlantısı oluştur
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sorgu = $"SELECT * FROM {databaseName}.dbo.{tableName}";

                        using (SqlCommand command = new SqlCommand(sorgu, connection))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                // Tablo verilerini DataGridView'e yükle
                                dataGridView1.DataSource = table;
                            }
                        }
                    }
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show("HATA: " + ex.Message, "Bildiri",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
