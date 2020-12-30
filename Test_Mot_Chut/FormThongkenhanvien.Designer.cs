namespace Test_Mot_Chut
{
    partial class FormThongkenhanvien
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
            this.dataGridViewtHONGKE = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewtHONGKE)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewtHONGKE
            // 
            this.dataGridViewtHONGKE.AllowUserToAddRows = false;
            this.dataGridViewtHONGKE.AllowUserToDeleteRows = false;
            this.dataGridViewtHONGKE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewtHONGKE.Location = new System.Drawing.Point(94, 32);
            this.dataGridViewtHONGKE.Name = "dataGridViewtHONGKE";
            this.dataGridViewtHONGKE.ReadOnly = true;
            this.dataGridViewtHONGKE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewtHONGKE.Size = new System.Drawing.Size(447, 355);
            this.dataGridViewtHONGKE.TabIndex = 0;
            this.dataGridViewtHONGKE.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewtHONGKE_CellClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(246, 393);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 56);
            this.button1.TabIndex = 1;
            this.button1.Text = "THOÁT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormThongkenhanvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(652, 476);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewtHONGKE);
            this.Name = "FormThongkenhanvien";
            this.Text = "Thống kê theo thành tích";
            this.Load += new System.EventHandler(this.FormThongkenhanvien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewtHONGKE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewtHONGKE;
        private System.Windows.Forms.Button button1;
    }
}