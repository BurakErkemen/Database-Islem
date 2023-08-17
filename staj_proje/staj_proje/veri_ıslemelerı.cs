using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace staj_proje
{
    public partial class veri_ıslemelerı : Form
    {
        private string connectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True;MultipleActiveResultSets=True";
        private string selectedTable;
        private string databaseName;
        private string cellValue = "";

        public veri_ıslemelerı()
        {
            InitializeComponent();
        }

        private void veri_ıslemelerı_Load(object sender, EventArgs e)
        {
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
            if (string.IsNullOrEmpty(cellValue))
            {
                textBox1.Visible = false; textBox2.Visible = false;
                textBox3.Visible = false; textBox4.Visible = false;
                textBox5.Visible = false; textBox6.Visible = false;
                textBox7.Visible = false; textBox8.Visible = false;
                label_tblad_yaz.Visible = false; tablo_adı.Visible = false;
                label1.Visible = false; label2.Visible = false;
                label3.Visible = false; label4.Visible = false;
                label5.Visible = false; label6.Visible = false;
                label7.Visible = false; label8.Visible = false;
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
                            reader.Close();
                        }
                    }
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
                // DataGridView'e tablo yüklensin 
                GridTablo(databaseName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void GridTablo(string databaseName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Listbox içindeki veritabanının içindeki tabloları açan sorgusu
                string sqlkomut = $"SELECT TABLE_NAME FROM {databaseName}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

                try
                {
                    using (SqlCommand command = new SqlCommand(sqlkomut, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            using (DataTable table = new DataTable())
                            {
                                adapter.Fill(table);

                                // DataGridView'e tablo verilerini yükle
                                dataGridView1.DataSource = table;
                            }
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    int columnIndex = dataGridView1.SelectedCells[0].ColumnIndex;
                    DataGridViewCell selectedCell = dataGridView1.Rows[rowIndex].Cells[columnIndex];

                    cellValue = selectedCell.Value.ToString();
                    selectedTable = cellValue; // Seçilen tablonun adını kaydediyoruz
                    label_tblad_yaz.Text = cellValue;
                    // Seçilen tablonun sütun adlarını alıyoruz
                    List<string> columnNames = GetColumnNames(selectedTable);

                    // Label'lara sütun adlarını aktarıyoruz
                    if (cellValue != "")
                    {
                        label1.Text = columnNames.Count > 0 ? columnNames[0] : "";
                        string labelText = label1.Text.ToLower();
                        if (labelText.Contains("ıd") || labelText.Contains("id") || labelText.Contains("%id"))
                        {
                            textBox1.Enabled = true;
                        }
                        else
                        {
                            textBox1.Enabled = false;
                        }

                        label2.Text = columnNames.Count > 1 ? columnNames[1] : "";

                        if (string.IsNullOrEmpty(label2.Text))
                        { textBox2.Visible = false; label2.Visible = false; }
                        else { label2.Visible = true; textBox2.Visible = true; }

                        label3.Text = columnNames.Count > 2 ? columnNames[2] : "";

                        if (string.IsNullOrEmpty(label3.Text))
                        { textBox3.Visible = false; label3.Visible = false; }
                        else { textBox3.Visible = true; label3.Visible = true; }

                        label4.Text = columnNames.Count > 3 ? columnNames[3] : "";

                        if (string.IsNullOrEmpty(label4.Text))
                        { textBox4.Visible = false; label4.Visible = false; }
                        else { textBox4.Visible = true; label4.Visible = true; }

                        label5.Text = columnNames.Count > 4 ? columnNames[4] : "";

                        if (string.IsNullOrEmpty(label5.Text))
                        { textBox5.Visible = false; label5.Visible = false; }
                        else { textBox5.Visible = true; label5.Visible = true; }

                        label6.Text = columnNames.Count > 5 ? columnNames[5] : "";

                        if (string.IsNullOrEmpty(label6.Text))
                        { textBox6.Visible = false; label6.Visible = false; }
                        else { textBox6.Visible = true; label6.Visible = true; }

                        label7.Text = columnNames.Count > 6 ? columnNames[6] : "";

                        if (string.IsNullOrEmpty(label7.Text))
                        { textBox7.Visible = false; label7.Visible = false; }
                        else { textBox7.Visible = true; label7.Visible = true; }

                        label8.Text = columnNames.Count > 7 ? columnNames[7] : "";

                        if (string.IsNullOrEmpty(label8.Text))
                        { textBox8.Visible = false; label8.Visible = false; }
                        else { textBox8.Visible = true; label8.Visible = true; }
                    }
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        connection.Open();
                        string sorgu = $"SELECT * FROM {databaseName}.dbo.{cellValue};";

                        using (SqlCommand command = new SqlCommand(sorgu, connection))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            // DataGridView'e tablo verilerini yükle
                            dataGridView1.DataSource = table;
                        }
                    }
                }
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                    int columnIndex = dataGridView1.SelectedCells[0].ColumnIndex;

                    List<string> columnNames = GetColumnNames(selectedTable); // Doğru şekilde çağrı yapılmalı

                    List<TextBox> textBoxes = new List<System.Windows.Forms.TextBox> { 
                        textBox1, textBox2, textBox3, textBox4, 
                        textBox5, textBox6, textBox7, textBox8, 
                    };
                    // TextBox'lara sütun değerlerini yazdır
                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        textBoxes[i].Text = selectedRow.Cells[columnNames[i]].Value.ToString(); 
                        /*
                        textBox2.Text = selectedRow.Cells[columnNames[1]].Value.ToString();
                        textBox3.Text = selectedRow.Cells[columnNames[2]].Value.ToString();
                        textBox4.Text = selectedRow.Cells[columnNames[3]].Value.ToString();
                        textBox5.Text = selectedRow.Cells[columnNames[4]].Value.ToString();
                        textBox6.Text = selectedRow.Cells[columnNames[5]].Value.ToString();
                        textBox7.Text = selectedRow.Cells[columnNames[6]].Value.ToString();
                        textBox8.Text = selectedRow.Cells[columnNames[7]].Value.ToString();
                        */
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK);
            }
        }

        private List<string> GetColumnNames(string tableName)
        {
            List<string> columnNames = new List<string>();
            string connectionString = $"Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog={databaseName};Integrated Security=True;MultipleActiveResultSets=True";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                // Tablonun sütun adlarını sorgulayın
                string sql = $"SELECT COLUMN_NAME FROM {databaseName}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
                using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string columnName = reader["COLUMN_NAME"].ToString();
                            columnNames.Add(columnName);
                        }
                    }
                }
            }
            return columnNames;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<System.Windows.Forms.TextBox> nonEmptyTextBoxes = new List<System.Windows.Forms.TextBox>
        {
            textBox1, textBox2, textBox3, textBox4,
            textBox5, textBox6, textBox7, textBox8
        };

                List<string> columnNames = new List<string>();

                for (int i = 0; i < nonEmptyTextBoxes.Count; i++)
                {
                    System.Windows.Forms.TextBox textBox = nonEmptyTextBoxes[i];
                    Label label = Controls.Find("label" + (i + 1), true).FirstOrDefault() as Label;

                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        columnNames.Add(label.Text);
                    }
                }

                if (columnNames.Count == 0)
                {
                    MessageBox.Show("En az bir sütun doldurulmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string sql = $"SET IDENTITY_INSERT {selectedTable} ON;USE {databaseName};INSERT INTO {selectedTable} ({string.Join(", ", columnNames)}) VALUES ";

                List<string> values = new List<string>();

                foreach (System.Windows.Forms.TextBox textBox in nonEmptyTextBoxes)
                {
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        values.Add($"'{textBox.Text}'");
                    }
                }

                if (columnNames.Count != values.Count)
                {
                    MessageBox.Show("Tüm sütunlar için değer girilmelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sql += "(" + string.Join(", ", values) + "); SET IDENTITY_INSERT [{selectedTable}] OFF";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        foreach (System.Windows.Forms.TextBox textBox in nonEmptyTextBoxes)
                        {
                            textBox.Clear();
                        }
                    }
                }

                GridTablo(databaseName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Tablo Silme İşlemi
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = $"USE {databaseName}; DROP TABLE {selectedTable};";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Tablo başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        selectedTable = ""; // Silinen tabloyu temizle
                        label_tblad_yaz.Text = "";
                        // DataGridView'i güncelleyin ya da temizleyin, sizin tasarımınıza göre düzenleyin
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    string primaryKeyValue = selectedRow.Cells["PrimaryKeyColumn"].Value.ToString(); // Birincil anahtar sütunu adını değiştirin

                    List<System.Windows.Forms.TextBox> nonEmptyTextBoxes = new List<System.Windows.Forms.TextBox>
            {
                textBox1, textBox2, textBox3, textBox4,
                textBox5, textBox6, textBox7, textBox8
            };

                    List<string> columnNames = new List<string>();
                    List<string> setValues = new List<string>();

                    for (int i = 0; i < nonEmptyTextBoxes.Count; i++)
                    {
                        System.Windows.Forms.TextBox textBox = nonEmptyTextBoxes[i];
                        Label label = Controls.Find("label" + (i + 1), true).FirstOrDefault() as Label;

                        if (!string.IsNullOrWhiteSpace(textBox.Text))
                        {
                            columnNames.Add(label.Text);
                            setValues.Add($"{label.Text} = '{textBox.Text}'");
                        }
                    }

                    if (columnNames.Count == 0)
                    {
                        MessageBox.Show("En az bir sütun doldurulmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string updateQuery = $"UPDATE {selectedTable} SET {string.Join(", ", setValues)} WHERE PrimaryKeyColumn = @PrimaryKeyValue";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@PrimaryKeyValue", primaryKeyValue);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Veri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Veri güncellenirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    ClearTextBoxes();
                    dataGridView1.ClearSelection();
                }
                else
                {
                    MessageBox.Show("Lütfen bir satır seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "BİLDİRİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }
    }
}