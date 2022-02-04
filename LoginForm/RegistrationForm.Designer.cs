﻿
namespace LoginForm
{
    partial class RegistrationForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonReg = new System.Windows.Forms.Button();
            this.clickableLabel2 = new LoginForm.ClickableLabel();
            this.textBoxLogin = new LoginForm.HintTextBox();
            this.textBoxPass2 = new LoginForm.HintTextBox();
            this.textBoxPass = new LoginForm.HintTextBox();
            this.textBoxSurname = new LoginForm.HintTextBox();
            this.textBoxName = new LoginForm.HintTextBox();
            this.clickableLabel1 = new LoginForm.ClickableLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(67)))));
            this.panel1.Controls.Add(this.clickableLabel1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 100);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 100);
            this.label1.TabIndex = 0;
            this.label1.Text = "РЕГИСТРАЦИЯ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = global::LoginForm.Properties.Resources.img_296750;
            this.pictureBox2.Location = new System.Drawing.Point(12, 258);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::LoginForm.Properties.Resources.img_311846;
            this.pictureBox1.Location = new System.Drawing.Point(12, 128);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // buttonReg
            // 
            this.buttonReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(177)))));
            this.buttonReg.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(101)))), ((int)(((byte)(141)))));
            this.buttonReg.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(101)))), ((int)(((byte)(127)))));
            this.buttonReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReg.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReg.Location = new System.Drawing.Point(50, 349);
            this.buttonReg.Name = "buttonReg";
            this.buttonReg.Size = new System.Drawing.Size(280, 50);
            this.buttonReg.TabIndex = 6;
            this.buttonReg.Text = "Зарегистрировать пользователя";
            this.buttonReg.UseVisualStyleBackColor = false;
            this.buttonReg.Click += new System.EventHandler(this.buttonReg_Click);
            // 
            // clickableLabel2
            // 
            this.clickableLabel2.AutoSize = true;
            this.clickableLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.clickableLabel2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.clickableLabel2.Location = new System.Drawing.Point(110, 415);
            this.clickableLabel2.MouseClickBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.clickableLabel2.MouseClickForeColor = System.Drawing.Color.RoyalBlue;
            this.clickableLabel2.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.clickableLabel2.MouseOverForeColor = System.Drawing.Color.CadetBlue;
            this.clickableLabel2.Name = "clickableLabel2";
            this.clickableLabel2.Size = new System.Drawing.Size(154, 13);
            this.clickableLabel2.TabIndex = 9;
            this.clickableLabel2.Text = "Вернуться на главный экран";
            this.clickableLabel2.Click += new System.EventHandler(this.clickableLabel2_Click);
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.textBoxLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLogin.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Italic);
            this.textBoxLogin.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBoxLogin.HintText = "Логин";
            this.textBoxLogin.Location = new System.Drawing.Point(78, 192);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(260, 26);
            this.textBoxLogin.TabIndex = 7;
            this.textBoxLogin.Text = "Логин";
            // 
            // textBoxPass2
            // 
            this.textBoxPass2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.textBoxPass2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPass2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Italic);
            this.textBoxPass2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBoxPass2.HintText = "Подтверждение пароля";
            this.textBoxPass2.Location = new System.Drawing.Point(78, 286);
            this.textBoxPass2.Name = "textBoxPass2";
            this.textBoxPass2.Size = new System.Drawing.Size(260, 26);
            this.textBoxPass2.TabIndex = 5;
            this.textBoxPass2.Text = "Подтверждение пароля";
            this.textBoxPass2.UseSystemPasswordChar = true;
            // 
            // textBoxPass
            // 
            this.textBoxPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.textBoxPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPass.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Italic);
            this.textBoxPass.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBoxPass.HintText = "Пароль";
            this.textBoxPass.Location = new System.Drawing.Point(78, 254);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.Size = new System.Drawing.Size(260, 26);
            this.textBoxPass.TabIndex = 4;
            this.textBoxPass.Text = "Пароль";
            this.textBoxPass.UseSystemPasswordChar = true;
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.textBoxSurname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSurname.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Italic);
            this.textBoxSurname.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBoxSurname.HintText = "Фамилия";
            this.textBoxSurname.Location = new System.Drawing.Point(78, 160);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new System.Drawing.Size(260, 26);
            this.textBoxSurname.TabIndex = 2;
            this.textBoxSurname.Text = "Фамилия";
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Italic);
            this.textBoxName.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBoxName.HintText = "Имя";
            this.textBoxName.Location = new System.Drawing.Point(78, 128);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(260, 26);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.Text = "Имя";
            // 
            // clickableLabel1
            // 
            this.clickableLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.clickableLabel1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clickableLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(67)))));
            this.clickableLabel1.Location = new System.Drawing.Point(360, 0);
            this.clickableLabel1.MouseClickBackColor = System.Drawing.Color.LightCoral;
            this.clickableLabel1.MouseClickForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.clickableLabel1.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(177)))));
            this.clickableLabel1.MouseOverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.clickableLabel1.Name = "clickableLabel1";
            this.clickableLabel1.Size = new System.Drawing.Size(20, 20);
            this.clickableLabel1.TabIndex = 9;
            this.clickableLabel1.Text = "✖";
            this.clickableLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.clickableLabel1.Click += new System.EventHandler(this.clickableLabel1_Click);
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.ClientSize = new System.Drawing.Size(380, 456);
            this.Controls.Add(this.clickableLabel2);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.buttonReg);
            this.Controls.Add(this.textBoxPass2);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.textBoxSurname);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistrationForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private LoginForm.HintTextBox textBoxName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private LoginForm.HintTextBox textBoxSurname;
        private System.Windows.Forms.PictureBox pictureBox2;
        private LoginForm.HintTextBox textBoxPass;
        private LoginForm.HintTextBox textBoxPass2;
        private System.Windows.Forms.Button buttonReg;
        private ClickableLabel clickableLabel1;
        private HintTextBox textBoxLogin;
        private ClickableLabel clickableLabel2;
    }
}