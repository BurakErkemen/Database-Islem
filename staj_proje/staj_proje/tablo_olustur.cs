using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace staj_proje
{
    public partial class tablo_olustur : Form
    {
        public tablo_olustur()
        {
            InitializeComponent();
        }
        public string database_name_taşı { get; set; }
        private static string connectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True;MultipleActiveResultSets=True";

        private string combo_selected2, combo_selected3, combo_selected4, combo_selected5, combo_selected6, combo_selected7, combo_selected8;
        private void tablo_olustur_Load(object sender, EventArgs e)
        {
            GetDataTypes();
            checkBox1.Checked = true;
            checkBox1.Enabled = false;
            comboBox1.Text = "int";
            comboBox1.Enabled = false;

            combo_selected2 = "";
            combo_selected3 = "";
            combo_selected4 = "";
            combo_selected5 = "";
            combo_selected6 = "";
            combo_selected7 = "";
            combo_selected8 = "";
        }
        private void tbl_olustur_btn_Click(object sender, EventArgs e)
        {

            string sutun2 = "";
            string sutun3 = "";
            string sutun4 = "";
            string sutun5 = "";
            string sutun6 = "";
            string sutun7 = "";
            string sutun8 = "";

            if (!string.IsNullOrEmpty(sutun2_txt.Text))
            {
                combo_selected2 = comboBox2.SelectedItem.ToString().ToUpper();

                if (combo_selected2 == "CHAR" || combo_selected3 == "NCHAR" || combo_selected3 == "VARCHAR" || combo_selected3 == "NVARCHAR")
                {
                    if (sutun2_txt.Text != sutun1_txt.Text)
                    {
                        sutun2 = $", {sutun2_txt.Text} {combo_selected2} (55) {checkbox_verial()} ";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (sutun2_txt.Text != sutun1_txt.Text)
                    {
                        sutun2 = $", {sutun2_txt.Text} {combo_selected2} {checkbox_verial()} ";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
            }

            if (!string.IsNullOrEmpty(sutun3_txt.Text))
            {
                combo_selected3 = comboBox3.SelectedItem.ToString().ToUpper();

                if (combo_selected3 == "CHAR" || combo_selected3 == "NCHAR" || combo_selected3 == "VARCHAR" || combo_selected3 == "NVARCHAR")
                {
                    if (sutun3_txt.Text != sutun2_txt.Text & sutun3_txt.Text != sutun1_txt.Text)
                    {
                        sutun3 = $", {sutun3_txt.Text} {combo_selected3} (55) {checkbox_verial()} ";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (sutun3_txt.Text != sutun2_txt.Text & sutun3_txt.Text != sutun1_txt.Text)
                    {
                        sutun3 = $", {sutun3_txt.Text} {combo_selected3} {checkbox_verial()} ";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
                
            }

            if (!string.IsNullOrEmpty(sutun4_txt.Text))
            {
                combo_selected4 = comboBox4.SelectedItem.ToString().ToUpper();

                if (combo_selected4 == "CHAR" || combo_selected3 == "NCHAR" || combo_selected3 == "VARCHAR" || combo_selected3 == "NVARCHAR")
                {
                    if (sutun4_txt.Text != sutun3_txt.Text & sutun4_txt.Text != sutun2_txt.Text & sutun4_txt.Text != sutun1_txt.Text)
                    {
                        sutun4 = $", {sutun4_txt.Text} {combo_selected4} (55) {checkbox_verial()} ";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (sutun4_txt.Text != sutun3_txt.Text & sutun4_txt.Text != sutun2_txt.Text & sutun4_txt.Text != sutun1_txt.Text)
                    {
                        sutun4 = $", {sutun4_txt.Text} {combo_selected4} {checkbox_verial()} ";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
            }

            if (!string.IsNullOrEmpty(sutun5_txt.Text))
            {
                combo_selected5 = comboBox5.SelectedItem.ToString().ToUpper();
                if (combo_selected5 == "CHAR" || combo_selected3 == "NCHAR" || combo_selected3 == "VARCHAR" || combo_selected3 == "NVARCHAR")
                {
                    if (sutun5_txt.Text != sutun4_txt.Text & sutun5_txt.Text != sutun3_txt.Text & sutun5_txt.Text != sutun2_txt.Text & sutun5_txt.Text != sutun1_txt.Text)
                    {
                        sutun5 = $", {sutun5_txt.Text} {combo_selected5} (55) {checkbox_verial()}";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (sutun5_txt.Text != sutun4_txt.Text & sutun5_txt.Text != sutun3_txt.Text & sutun5_txt.Text != sutun2_txt.Text & sutun5_txt.Text != sutun1_txt.Text)
                    {
                        sutun5 = $", {sutun5_txt.Text} {combo_selected5} {checkbox_verial()}";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
               
            }

            if (!string.IsNullOrEmpty(sutun6_txt.Text))
            {
                combo_selected6 = comboBox6.SelectedItem.ToString().ToUpper();

                if (combo_selected6 == "CHAR" || combo_selected3 == "NCHAR" || combo_selected3 == "VARCHAR" || combo_selected3 == "NVARCHAR")
                {
                    if (sutun6_txt.Text != sutun5_txt.Text & sutun6_txt.Text != sutun4_txt.Text & sutun6_txt.Text != sutun3_txt.Text & sutun6_txt.Text != sutun2_txt.Text & sutun6_txt.Text != sutun1_txt.Text)
                    {
                        sutun6 = $", {sutun6_txt.Text} {combo_selected6} (55) {checkbox_verial()} ";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (sutun6_txt.Text != sutun5_txt.Text & sutun6_txt.Text != sutun4_txt.Text & sutun6_txt.Text != sutun3_txt.Text & sutun6_txt.Text != sutun2_txt.Text & sutun6_txt.Text != sutun1_txt.Text)
                    {
                        sutun6 = $", {sutun6_txt.Text} {combo_selected6} {checkbox_verial()} ";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
                
            }

            if (!string.IsNullOrEmpty(sutun7_txt.Text))
            {
                combo_selected7 = comboBox7.SelectedItem.ToString().ToUpper();
                if (combo_selected7 == "CHAR" || combo_selected3 == "NCHAR" || combo_selected3 == "VARCHAR" || combo_selected3 == "NVARCHAR")
                {
                    if (sutun7_txt.Text != sutun6_txt.Text & sutun7_txt.Text != sutun5_txt.Text & sutun7_txt.Text != sutun4_txt.Text &
               sutun7_txt.Text != sutun3_txt.Text & sutun7_txt.Text != sutun2_txt.Text & sutun7_txt.Text != sutun1_txt.Text)
                    {
                        sutun7 = $", {sutun7_txt.Text} {combo_selected7}(55) {checkbox_verial()}";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (sutun7_txt.Text != sutun6_txt.Text & sutun7_txt.Text != sutun5_txt.Text & sutun7_txt.Text != sutun4_txt.Text &
               sutun7_txt.Text != sutun3_txt.Text & sutun7_txt.Text != sutun2_txt.Text & sutun7_txt.Text != sutun1_txt.Text)
                    {
                        sutun7 = $", {sutun7_txt.Text} {combo_selected7} {checkbox_verial()}";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
               
            }

            if (!string.IsNullOrEmpty(sutun8_txt.Text))
            {
                combo_selected8 = comboBox7.SelectedItem.ToString().ToUpper();

                if (combo_selected8 == "CHAR" || combo_selected3 == "NCHAR" || combo_selected3 == "VARCHAR" || combo_selected3 == "NVARCHAR")
                {
                    if (sutun8_txt.Text != sutun7_txt.Text & sutun8_txt.Text != sutun6_txt.Text & sutun8_txt.Text != sutun5_txt.Text & sutun8_txt.Text != sutun4_txt.Text
                       & sutun8_txt.Text != sutun3_txt.Text & sutun8_txt.Text != sutun2_txt.Text & sutun8_txt.Text != sutun1_txt.Text)
                    {
                        sutun8 = $", {sutun8_txt.Text} {combo_selected8} (55) {checkbox_verial()}";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (sutun8_txt.Text != sutun7_txt.Text & sutun8_txt.Text != sutun6_txt.Text & sutun8_txt.Text != sutun5_txt.Text & sutun8_txt.Text != sutun4_txt.Text
                       & sutun8_txt.Text != sutun3_txt.Text & sutun8_txt.Text != sutun2_txt.Text & sutun8_txt.Text != sutun1_txt.Text)
                    {
                        sutun8 = $", {sutun8_txt.Text} {combo_selected8} {checkbox_verial()}";
                    }
                    else
                    {
                        MessageBox.Show("Aynı veri adı kullanıldı lütfen değiştiriniz!", "HATA", MessageBoxButtons.OK);
                    }
                }
               
            }

            // verilerin sonunda ,,,,, konulması hatası var! sorgualma işleminde ekstra virgülden kaynaklı gerçekleşen sorgu hatası oluşmaktadır.
            string sorguKomutu = $"USE {database_name_taşı} " +
            $"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '{tablo_ad_txt.Text}') " +
            $"BEGIN " +
            $"CREATE TABLE {tablo_ad_txt.Text} (" +
            $"{sutun1_txt.Text} INT PRIMARY KEY IDENTITY(1,1)" +
            $"{sutun2}" +
            $"{sutun3}" +
            $"{sutun4}" +
            $"{sutun5}" +
            $"{sutun6}" +
            $"{sutun7}" +
            $"{sutun8}" +
            $") " +
            $"END";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sorguKomutu, connection))
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show(database_name_taşı + " Veritabanına " + tablo_ad_txt.Text + " Tablosu Başarıyla Eklendi...", "BİLDİRİ", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK);
            }
        }

        private string checkbox_verial()
        {
            CheckBox checkBox = new CheckBox();
            if (checkBox.Checked != true)
            {
                return "NULL";
            }
            else
            {
                return "NOT NULL";
            }
        }



        private void tbl_ıptal_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetDataTypes()
        {
            using(SqlConnection  connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Veritabanına bağlan ve veri türlerini al
                using (SqlCommand command = new SqlCommand("SELECT * FROM sys.types ORDER BY name ASC", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Veri türünü al ve ComboBox'a ekle
                            string dataType = reader.GetString(0);
                            comboBox1.Items.Add(dataType);
                            comboBox2.Items.Add(dataType);
                            comboBox3.Items.Add(dataType);
                            comboBox4.Items.Add(dataType);
                            comboBox5.Items.Add(dataType);
                            comboBox6.Items.Add(dataType);
                            comboBox7.Items.Add(dataType);
                            comboBox8.Items.Add(dataType);
                        }
                    }
                }
            }
        }
    }
}
