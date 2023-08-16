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
    public partial class sql_sorgusu_yaz : Form
    {
        public sql_sorgusu_yaz()
        {
            InitializeComponent();
        }
        private static string connnectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True";
        private string sql_sorgusu;
        private void sql_sorgusu_yaz_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Görünen Metin Girme Kısmına SQL Sorgularınızı Yazınız. Control + Enter Tuşlaması İle Alt Satıra Geçebilirsiniz. ", "Mesaj", MessageBoxButtons.OK);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sql_sorgusu = richTextBox1.Text;
            tableList();
        }
        private void tableList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql_sorgusu, connection))
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

        private void richTextBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
                
            if (e.Control && e.KeyCode == Keys.Enter)    
            {
                // Control+Enter tuş kombinasyonuna basıldığında alt satıra geç    
                richTextBox1.AppendText(Environment.NewLine);
                e.IsInputKey = false; // Olayın işlendiğini belirtmek için gerekli    
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Enter tuşunun işlendiğini belirtmek için gerekli

                // Butonu tıklanmış gibi işle
                button1.PerformClick();
            }
        }
    }
}
