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
    public partial class FormThongkenhanvien : Form
    {
        /// <summary>ThongkeNhanVien
        private string sql = "";
        
        public FormThongkenhanvien()
        {
            InitializeComponent();
        }

        private void FormThongkenhanvien_Load(object sender, EventArgs e)
        {

            DatabaseNV dbnv = new DatabaseNV();
            sql = "ThongkeNhanVien";
            List<CustomerParameter> lst = new List<CustomerParameter>();
   
            dataGridViewtHONGKE.DataSource = dbnv.SelectData(sql, lst);
        }

        private void dataGridViewtHONGKE_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //MessageBox.Show(dataGridViewNhanVien.Rows[e.RowIndex].Cells["Mã nhân viên"].Value.ToString());
                var mnv = dataGridViewtHONGKE.Rows[e.RowIndex].Cells["Mã nhân viên"].Value.ToString();
                new FormThongtinnhanvien(mnv, 0, 1).ShowDialog();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
