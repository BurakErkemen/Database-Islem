using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace staj_proje
{
    public partial class Kayıtol : Form
    {
        public Kayıtol()
        {
            InitializeComponent();
        }
        private void Kayıtol_Load(object sender, EventArgs e)
        {
            label7.Visible = false;

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                textBox5.UseSystemPasswordChar = true;
                textBox4.UseSystemPasswordChar = true;
            }
            else if (checkBox1.CheckState == CheckState.Unchecked)
            {
                textBox5.UseSystemPasswordChar = false;
                textBox4.UseSystemPasswordChar = false;
            }
        }

        private void kayıtol_btn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == String.Empty)
            {
                textBox1.BackColor = Color.Yellow;
                MessageBox.Show("Sarı Rekli Alanları Boş Geçemezsiniz", "Boş Alan Hatası");
            }
            else if (textBox2.Text == "" || textBox2.Text == String.Empty)
            {
                textBox2.BackColor = Color.Yellow;
                MessageBox.Show("Sarı Rekli Alanları Boş Geçemezsiniz", "Boş Alan Hatası");
            }
            else if (textBox3.Text == "" || textBox3.Text == String.Empty)
            {
                textBox3.BackColor = Color.Yellow;
                MessageBox.Show("Sarı Rekli Alanları Boş Geçemezsiniz", "Boş Alan Hatası");
            }
            else if (textBox4.Text == "" || textBox5.Text == "" ||
                textBox5.Text == String.Empty || textBox6.Text == String.Empty)
            {
                textBox4.BackColor = Color.Yellow;
                textBox5.BackColor = Color.Yellow;
                MessageBox.Show("Sarı Renkli Alanları Boş Geçemezsiniz", "Boş Alan Hatası");

            }
            else if (textBox4.Text != textBox5.Text)
            {
                textBox5.BackColor = Color.Red; textBox6.BackColor = Color.Red;
                MessageBox.Show("ŞİFRENİZİ KONTROL EDİNİZ");
            }
            else if (textBox6.Text == null)
            {
                MessageBox.Show("Kod gönderiniz!");
            }
            else
            {
                
                string connectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True;MultipleActiveResultSets=True";
                string ad = textBox1.Text;
                string soyad = textBox2.Text;
                string mail = textBox3.Text;
                string sifre = textBox4.Text;

                int sifreId = -1; // Varsayılan değer

                // Önce veritabanında bu şifrenin olup olmadığını kontrol edelim
                string checkSifreQuery = "SELECT Sifre_Id FROM Sifrelers WHERE sifre = @sifre";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand checkSifreCommand = new SqlCommand(checkSifreQuery, connection))
                    {
                        checkSifreCommand.Parameters.AddWithValue("@sifre", sifre);
                        object result = checkSifreCommand.ExecuteScalar();
                        if (result != null)
                        {
                            sifreId = Convert.ToInt32(result);
                        }
                    }
                }

                // Eğer şifre daha önce kaydedilmişse, sifreId değeri güncellenecek
                // Eğer şifre kaydedilmemişse, sifreId hala -1 olarak kalacak

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Sifrelers tablosuna şifre ekleme (eğer daha önce kaydedilmemişse)
                        if (sifreId == -1)
                        {
                            string sifreInsertQuery = "INSERT INTO Sifrelers (sifre) VALUES (@sifre); SELECT SCOPE_IDENTITY();";
                            using (SqlCommand sifreInsertCommand = new SqlCommand(sifreInsertQuery, connection))
                            {
                                sifreInsertCommand.Parameters.AddWithValue("@sifre", sifre);
                                sifreId = Convert.ToInt32(sifreInsertCommand.ExecuteScalar());
                            }
                        }

                        // Uyelers tablosuna kayıt ekleme
                        string uyelerInsertQuery = "INSERT INTO Uyelers (ad, soyad, mail, Sifreler_Sifre_Id) VALUES (@ad, @soyad, @mail, @sifreId)";
                        using (SqlCommand uyelerInsertCommand = new SqlCommand(uyelerInsertQuery, connection))
                        {
                            uyelerInsertCommand.Parameters.AddWithValue("@ad", ad);
                            uyelerInsertCommand.Parameters.AddWithValue("@soyad", soyad);
                            uyelerInsertCommand.Parameters.AddWithValue("@mail", mail);
                            uyelerInsertCommand.Parameters.AddWithValue("@sifreId", sifreId);
                            uyelerInsertCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Kayıt Başarılı");

                        // Giriş sayfasını aç
                        giris_sayfası giris = new giris_sayfası();
                        giris.Show();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }

        private void iptal_btn_Click(object sender, EventArgs e)
        {
            giris_sayfası giris_Sayfası = new giris_sayfası();
            giris_Sayfası.Show();
            this.Hide();
        }

        private void kodgonder_btn_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            Timer timer = new Timer();
            int remainingSeconds = 120; // Kalan süre (saniye)
            timer.Interval = 1000; // 1 saniye = 1000 milisaniye

            int verificationCode = random.Next(1000, 10000); // 999-10000 arasında rastgele bir doğrulama kodu oluşturulur

            string email = textBox3.Text; // textbox3'den e-posta adresi alınır

            // SMTP sunucusu ve kimlik bilgilerini ayarlayın
            string smtpServer = "smtp-mail.outlook.com"; // SMTP sunucusunun adresini girin
            int smtpPort = 587; // SMTP sunucusunun port numarasını girin
            string smtpUsername = "burakerkemen@hotmail.com"; // SMTP sunucusu için kullanıcı adınızı girin
            string smtpPassword = "05414133440burak"; // SMTP sunucusu için parolanızı girin

            try
            {
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.EnableSsl = true; // SSL/TLS kullanarak güvenli bağlantıyı etkinleştirin
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(smtpUsername); // Gönderen e-posta adresi
                    mailMessage.To.Add(email); // Alıcı e-posta adresi
                    mailMessage.Subject = "Doğrulama Kodu"; // E-posta konusu
                    mailMessage.Body = "Doğrulama kodunuz: " + verificationCode.ToString(); // E-posta içeriği

                    smtpClient.Send(mailMessage); // E-postayı gönder

                    MessageBox.Show("Doğrulama kodu e-posta olarak gönderildi.");


                    // İki dakika sonra doğrulama kodunun etkisiz hale gelmesini sağlamak için
                    // bir zamanlayıcı (Timer) kullanabilirsiniz


                    timer.Tick += (timerSender, timerArgs) =>
                    {
                        remainingSeconds--; // Kalan süreyi azalt
                        label7.Visible = true; // Label5'i etkinleştir
                        label7.Text = "Kalan süre: " + remainingSeconds.ToString() + " saniye"; // Label5'e kalan süreyi yazdır

                        if (remainingSeconds <= 0)
                        {
                            verificationCode = -1;
                            label7.Text = "Doğrulama kodu süresi doldu."; // Label5'e mesaj yazdır
                            timer.Stop(); // Zamanlayıcıyı durdur
                            timer.Dispose(); // Zamanlayıcıyı kaldır
                        }
                    };
                    timer.Start(); // Zamanlayıcıyı başlat
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-posta gönderme hatası: " + ex.Message);
            }
        }

        
    }
}
