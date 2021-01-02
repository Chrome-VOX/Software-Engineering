using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Mot_Chut
{
    public partial class FormNhanVien : Form
    {
        private int Modeaccess;
        public FormNhanVien(int a)
        {
            this.Modeaccess = a;
            InitializeComponent();
            this.radioButton1.Checked = true;
        }
        public void LoadAgain()
        {
            var db = new DatabaseNV();
            List<CustomerParameter> lst = new List<CustomerParameter>();
            lst.Add(new CustomerParameter() { key = "@tukhoa", value = textBoxTimkiem.Text });
            if (this.radioButton1.Checked == true)
            {
                lst.Add(new CustomerParameter() { key = "@flag", value = "0" });
            }
            if (this.radioButton2.Checked == true)
            {
                lst.Add(new CustomerParameter() { key = "@flag", value = "1" });
            }

            dataGridViewNhanVien.DataSource = db.SelectData("SelectAllNhanVien", lst);
        }

        private void buttonTimkiem_Click(object sender, EventArgs e)
        {
            LoadAgain();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            if (this.Modeaccess == 0)
                {
                this.radioButton1.Visible = false;
                this.radioButton2.Visible = false;
                this.buttonthemmoi.Enabled = false;
                }
                LoadAgain();
        }

        private void dataGridViewNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //MessageBox.Show(dataGridViewNhanVien.Rows[e.RowIndex].Cells["Mã nhân viên"].Value.ToString());
                var mnv = dataGridViewNhanVien.Rows[e.RowIndex].Cells["Mã nhân viên"].Value.ToString();
                new FormThongtinnhanvien(mnv, 1, this.Modeaccess).ShowDialog();
                LoadAgain();
            }
        }

        private void buttonthemmoi_Click(object sender, EventArgs e)
        {
            FormSignUp form3 = new FormSignUp();
            form3.ShowDialog();
        }

        private void buttonTHONGKE_Click(object sender, EventArgs e)
        {
            FormThongkenhanvien f = new FormThongkenhanvien();
            f.ShowDialog();
        }


        private void label3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
