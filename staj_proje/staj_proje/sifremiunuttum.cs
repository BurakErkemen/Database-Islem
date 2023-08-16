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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace staj_proje
{
    public partial class sifremiunuttum : Form
    {
        public sifremiunuttum()
        {
            InitializeComponent();
        }
        private int verificationCode;
        private void sifremiunuttum_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
            else if (checkBox1.CheckState == CheckState.Unchecked)
            {
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
            }
        }

        private void ıptal_btn_Click(object sender, EventArgs e)
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

            verificationCode = random.Next(1000, 10000); // 999-10000 arasında rastgele bir doğrulama kodu oluşturulur

            string email = textBox1.Text; // textbox1'den e-posta adresi alınır

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
                        label5.Visible = true; // Label5'i etkinleştir
                        label5.Text = "Kalan süre: " + remainingSeconds.ToString() + " sn"; // Label5'e kalan süreyi yazdır

                        if (remainingSeconds <= 0)
                        {
                            verificationCode = -1;
                            label5.Text = "Doğrulama kodu süresi doldu."; // Label5'e mesaj yazdır
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


        private void onay_btn_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)
            {
                textBox2.BackColor = Color.Red;
                textBox3.BackColor = Color.Red;
                MessageBox.Show("Şifreler Uyuşmamaktadır.");
            }
            else if (textBox4.Text != verificationCode.ToString())
            {
                MessageBox.Show("Girilen Kod Hatalıdır!","Onay Kodu Hatası!",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (verificationCode.ToString() != textBox4.Text)
                {
                    MessageBox.Show("Hatalı doğrulama kodu girdiniz", "Doğrulama Kodu", MessageBoxButtons.OK);
                }
                else
                {
                    string connectionString = "Data Source=DESKTOP-1TTOTC5\\SQLEXPRESS;Initial Catalog=staj_proje;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            string mail = textBox1.Text; // Güncellenecek kullanıcının e-posta adresi
                            string yeniSifre = textBox2.Text; // Yeni şifre

                            // Şifreler tablosunda yeni şifreyi güncelleme
                            string sifreUpdateQuery = "UPDATE Sifrelers SET sifre = @yeniSifre WHERE Sifre_Id IN (SELECT Sifreler_Sifre_Id FROM Uyelers WHERE mail = @mail)";
                            using (SqlCommand sifreUpdateCommand = new SqlCommand(sifreUpdateQuery, connection))
                            {
                                sifreUpdateCommand.Parameters.AddWithValue("@yeniSifre", yeniSifre);
                                sifreUpdateCommand.Parameters.AddWithValue("@mail", mail);
                                sifreUpdateCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Şifre Güncellendi");
                            giris_sayfası giris = new giris_sayfası();
                            giris.Show();
                            this.Hide();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata: " + ex.Message,"HATA!");
                        }
                    }
                }

            }
        }

        
        
    }
}
