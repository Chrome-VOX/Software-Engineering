using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_Mot_Chut.DAO;

namespace Test_Mot_Chut
{
    public partial class FormHetHanVacxin : Form
    {
        public FormHetHanVacxin()
        {
            InitializeComponent();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void showlistVXhethan()
        {
            String x = DateTime.Today.ToString("yyyy-MM-dd");
            String query1 = String.Format("EXEC dbo.getVXeHetHan @ngay = '{0}'", x);
            DataTable dt = DataProvider.Instance.ExcuteQuery(query1);
            listVXhetHan.DataSource = dt;
        }
        
      
        private void FormHetHanVacxin_Load(object sender, EventArgs e)
        {
            showlistVXhethan();

            for (int i = 0; i < 6; i++)
            {
                listVXhetHan.Columns[i].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
                listVXhetHan.Columns[i].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                listVXhetHan.Columns[i].HeaderCell.Style.ForeColor = Color.DodgerBlue;
                listVXhetHan.Columns[i].DefaultCellStyle.ForeColor = Color.DodgerBlue;
                listVXhetHan.Columns[i].HeaderCell.Style.BackColor = Color.White;
                listVXhetHan.Columns[i].DefaultCellStyle.BackColor = Color.White;
            }
            listVXhetHan.EnableHeadersVisualStyles = false;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            

        }

        private void btnYes_Click_1(object sender, EventArgs e)
        {
            String x = DateTime.Today.ToString("yyyy-MM-dd");
            if (ma.Equals(""))
            {
                MessageBox.Show("Chọn Vacxin cần xóa");
            }
            else
            {
                String query = String.Format("EXEC dbo.xoaHetVXhethan @ngay = '{0}',@ma = '{1}' ", x, ma);
                DataProvider.Instance.ExcuteQuery(query);
                MessageBox.Show("Đã xóa xong");
                showlistVXhethan();
            }
            
        }
        String ma = "";
        private void listVXhetHan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ma=listVXhetHan.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btnXoaallVXHetHan_Click(object sender, EventArgs e)
        {
            String x = DateTime.Today.ToString("yyyy-MM-dd");
            string query = String.Format("EXEC dbo.deleteAllVXhetHan @ngay='{0}'", x);
            DataProvider.Instance.ExcuteQuery(query);

            showlistVXhethan();

            MessageBox.Show("Đã xóa hết các Vacxin hết hạn");
            Close();
        }
    }
}
