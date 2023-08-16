using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Contexts;
using System.Data.SqlClient;
using System.Security.Policy;

namespace staj_proje
{
    public partial class giris_sayfası : Form
    {
        public giris_sayfası()
        {
            InitializeComponent();
            sifre_tb.Text = "";
            sifre_tb.PasswordChar = '*';
            sifre_tb.MaxLength = 14;
        }
        private static string connectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True";
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                sifre_tb.UseSystemPasswordChar = true;
            }
            else if (checkBox1.CheckState == CheckState.Unchecked)
            {
                sifre_tb.UseSystemPasswordChar = false;
            }
        }


       
        private void kayıt_btn_Click(object sender, EventArgs e)
        {
            //Kayıt olma sayfasına yönlendirilecek
            Kayıtol kayıtol = new Kayıtol();
            kayıtol.Show();
            this.Hide();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //şifremi unuttum sayfasına
            sifremiunuttum sifremiunuttum = new sifremiunuttum();
            sifremiunuttum.Show();
            this.Hide();
            
        }

        private void giris_btn_Click(object sender, EventArgs e)
        {
            string mail = mail_tb.Text;
            string şifre = sifre_tb.Text;

            string sql = "SELECT Uyelers.*, Sifrelers.* FROM Uyelers JOIN Sifrelers ON Uyelers.Sifreler_Sifre_Id = Sifrelers.Sifre_Id WHERE Uyelers.mail = @mail AND Sifrelers.sifre = @sifre";
            
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@mail", mail);
                        command.Parameters.AddWithValue("@sifre", şifre);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string data = mail;
                            homepage anasayfa = new homepage();
                            anasayfa.Show();
                            anasayfa.Data = data;
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Giriş bilgileri geçersiz!");
                        }
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
