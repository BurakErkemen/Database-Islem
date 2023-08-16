namespace staj_proje
{
    partial class backup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(backup));
            this.label1 = new System.Windows.Forms.Label();
            this.txtbox_server = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbox_dosyayolu = new System.Windows.Forms.TextBox();
            this.btn_path = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbox_username = new System.Windows.Forms.TextBox();
            this.txtbox_sifre = new System.Windows.Forms.TextBox();
            this.onayla_btn = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(307, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Server : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbox_server
            // 
            this.txtbox_server.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtbox_server.Location = new System.Drawing.Point(393, 7);
            this.txtbox_server.Name = "txtbox_server";
            this.txtbox_server.Size = new System.Drawing.Size(207, 24);
            this.txtbox_server.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(277, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Dosya Yolu :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbox_dosyayolu
            // 
            this.txtbox_dosyayolu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtbox_dosyayolu.Location = new System.Drawing.Point(393, 97);
            this.txtbox_dosyayolu.Multiline = true;
            this.txtbox_dosyayolu.Name = "txtbox_dosyayolu";
            this.txtbox_dosyayolu.Size = new System.Drawing.Size(207, 28);
            this.txtbox_dosyayolu.TabIndex = 10;
            // 
            // btn_path
            // 
            this.btn_path.AutoSize = true;
            this.btn_path.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_path.FlatAppearance.BorderSize = 0;
            this.btn_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_path.ForeColor = System.Drawing.Color.Black;
            this.btn_path.Location = new System.Drawing.Point(606, 97);
            this.btn_path.Name = "btn_path";
            this.btn_path.Size = new System.Drawing.Size(94, 28);
            this.btn_path.TabIndex = 11;
            this.btn_path.Text = "Dosya Yolu";
            this.btn_path.UseVisualStyleBackColor = false;
            this.btn_path.Click += new System.EventHandler(this.btn_path_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(277, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Kullanıcı Adı:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(325, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Şifre :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbox_username
            // 
            this.txtbox_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtbox_username.Location = new System.Drawing.Point(393, 37);
            this.txtbox_username.Name = "txtbox_username";
            this.txtbox_username.Size = new System.Drawing.Size(207, 24);
            this.txtbox_username.TabIndex = 14;
            // 
            // txtbox_sifre
            // 
            this.txtbox_sifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtbox_sifre.Location = new System.Drawing.Point(393, 67);
            this.txtbox_sifre.Name = "txtbox_sifre";
            this.txtbox_sifre.Size = new System.Drawing.Size(207, 24);
            this.txtbox_sifre.TabIndex = 15;
            // 
            // onayla_btn
            // 
            this.onayla_btn.BackColor = System.Drawing.Color.RoyalBlue;
            this.onayla_btn.Cursor = System.Windows.Forms.Cursors.Default;
            this.onayla_btn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.onayla_btn.FlatAppearance.BorderSize = 0;
            this.onayla_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.onayla_btn.ForeColor = System.Drawing.Color.White;
            this.onayla_btn.Location = new System.Drawing.Point(393, 131);
            this.onayla_btn.Name = "onayla_btn";
            this.onayla_btn.Size = new System.Drawing.Size(91, 36);
            this.onayla_btn.TabIndex = 16;
            this.onayla_btn.Text = "Onayla";
            this.onayla_btn.UseVisualStyleBackColor = false;
            this.onayla_btn.Click += new System.EventHandler(this.onayla_btn_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(2, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(269, 290);
            this.listBox1.TabIndex = 17;
            // 
            // backup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(700, 303);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.onayla_btn);
            this.Controls.Add(this.txtbox_sifre);
            this.Controls.Add(this.txtbox_username);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbox_server);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbox_dosyayolu);
            this.Controls.Add(this.btn_path);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "backup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Back Up";
            this.Load += new System.EventHandler(this.backup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbox_server;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbox_dosyayolu;
        private System.Windows.Forms.Button btn_path;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtbox_username;
        private System.Windows.Forms.TextBox txtbox_sifre;
        private System.Windows.Forms.Button onayla_btn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ListBox listBox1;
    }
}