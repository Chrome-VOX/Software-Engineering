namespace Test_Mot_Chut
{
    partial class FormVerifyCode
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVerifyCode));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtVerifyCode = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnTat = new System.Windows.Forms.Button();
            this.btnNhap = new System.Windows.Forms.Button();
            this.btnGui = new System.Windows.Forms.Button();
            this.errNhapThieu = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errNhapThieu)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 166);
            this.panel1.TabIndex = 8;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(52, 53);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 52);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // txtVerifyCode
            // 
            this.txtVerifyCode.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVerifyCode.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtVerifyCode.Location = new System.Drawing.Point(135, 100);
            this.txtVerifyCode.Name = "txtVerifyCode";
            this.txtVerifyCode.Size = new System.Drawing.Size(187, 26);
            this.txtVerifyCode.TabIndex = 9;
            this.txtVerifyCode.Text = "Mã Xác Nhận";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtEmail.Location = new System.Drawing.Point(135, 42);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(187, 26);
            this.txtEmail.TabIndex = 10;
            this.txtEmail.Text = "Email";
            // 
            // btnTat
            // 
            this.btnTat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTat.FlatAppearance.BorderSize = 0;
            this.btnTat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnTat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnTat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTat.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnTat.Location = new System.Drawing.Point(408, 0);
            this.btnTat.Name = "btnTat";
            this.btnTat.Size = new System.Drawing.Size(34, 32);
            this.btnTat.TabIndex = 13;
            this.btnTat.Text = "X";
            this.btnTat.UseVisualStyleBackColor = true;
            this.btnTat.Click += new System.EventHandler(this.btnTat_Click);
            // 
            // btnNhap
            // 
            this.btnNhap.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhap.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhap.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNhap.Location = new System.Drawing.Point(357, 99);
            this.btnNhap.Name = "btnNhap";
            this.btnNhap.Size = new System.Drawing.Size(76, 29);
            this.btnNhap.TabIndex = 12;
            this.btnNhap.Text = "Nhập";
            this.btnNhap.UseVisualStyleBackColor = false;
            this.btnNhap.Click += new System.EventHandler(this.btnNhap_Click);
            // 
            // btnGui
            // 
            this.btnGui.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnGui.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGui.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGui.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGui.Location = new System.Drawing.Point(357, 40);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(76, 29);
            this.btnGui.TabIndex = 11;
            this.btnGui.Text = "Gửi Mã";
            this.btnGui.UseVisualStyleBackColor = false;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            // 
            // errNhapThieu
            // 
            this.errNhapThieu.ContainerControl = this;
            // 
            // FormVerifyCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 166);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtVerifyCode);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnTat);
            this.Controls.Add(this.btnNhap);
            this.Controls.Add(this.btnGui);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormVerifyCode";
            this.Text = "FormVerifyCode";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errNhapThieu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtVerifyCode;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnTat;
        private System.Windows.Forms.Button btnNhap;
        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.ErrorProvider errNhapThieu;
    }
}