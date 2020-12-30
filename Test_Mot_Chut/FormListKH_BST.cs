using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Mot_Chut
{
    public partial class FormListKH_BST : Form
    {
        string maBST = FormLogIn.maNV;
        string maPhieu;
        ConnectionStringSettings conSet = ConfigurationManager.ConnectionStrings["MyDBConnectionString"];

        public FormListKH_BST()
        {
            InitializeComponent();
            scr_val = 0;
        }

        int scr_val;
        SqlDataAdapter da;
        DataSet ds;
        int soKH = 0;

        private void FormListKH_BST_Load(object sender, EventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(source);
            sqlCon.Open();

            string sql = "SELECT MaPhieuTiem AS 'Mã PT',TenKhachHang AS 'Tên Khách Hàng',Tuoi AS 'Tuổi' FROM dbo.KhachHang,dbo.PhieuTiem " +
                 "WHERE KhachHang.MaKH = PhieuTiem.MaKH AND maNVtiem = '" + @maBST + "' AND GhiChu NOT LIKE '%Hoàn thành' AND GhiChu IS NOT NULL";
            //SqlCommand cmd_ThongTinPTiem = new SqlCommand("XuatPTiem_TheoBST", sqlCon);
            //cmd_ThongTinPTiem.CommandType = CommandType.StoredProcedure;
            //cmd_ThongTinPTiem.Parameters.AddWithValue("@maBST", SqlDbType.Char).Value = maBST.Trim();
            //DataTable dt_KH = new DataTable();

            //dt_KH.Load(cmd_ThongTinPTiem.ExecuteReader());
            //dgvListKH.DataSource = dt_KH;

            da = new SqlDataAdapter(sql, sqlCon);
            ds = new DataSet();
            //da.Fill(ds, "info");
            da.Fill(ds, scr_val, 5, "info");
            dgvListKH.DataSource = ds;
            dgvListKH.DataMember = "info";
            soKH = dgvListKH.Rows.Count;

            #region Đổi màu dataGridView
            dgvListKH.Columns[0].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvListKH.Columns[0].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvListKH.Columns[0].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvListKH.Columns[0].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dgvListKH.Columns[0].HeaderCell.Style.BackColor = Color.White;
            dgvListKH.Columns[0].DefaultCellStyle.BackColor = Color.White;

            dgvListKH.Columns[1].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvListKH.Columns[1].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvListKH.Columns[1].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvListKH.Columns[1].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dgvListKH.Columns[1].HeaderCell.Style.BackColor = Color.White;
            dgvListKH.Columns[1].DefaultCellStyle.BackColor = Color.White;

            dgvListKH.Columns[2].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvListKH.Columns[2].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvListKH.Columns[2].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvListKH.Columns[2].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dgvListKH.Columns[2].HeaderCell.Style.BackColor = Color.White;
            dgvListKH.Columns[2].DefaultCellStyle.BackColor = Color.White;

            dgvListKH.EnableHeadersVisualStyles = false;


            #endregion
        }

        private void dgvListKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(source);
            sqlCon.Open();

            int selectedRowIndex = e.RowIndex;

            DataGridViewRow row = new DataGridViewRow();

            row = dgvListKH.Rows[selectedRowIndex];

            maPhieu = row.Cells[0].Value.ToString();
            string tenKH = row.Cells[1].Value.ToString();
            string tuoi = row.Cells[2].Value.ToString();


            SqlCommand cmd_MaKH = new SqlCommand("Lay_MaKH", sqlCon);
            cmd_MaKH.CommandType = CommandType.StoredProcedure;
            cmd_MaKH.Parameters.AddWithValue("@maBSK", SqlDbType.Char).Value = maBST.Trim();
            cmd_MaKH.Parameters.AddWithValue("@maPhieu", SqlDbType.Char).Value = maPhieu.Trim();

            string maKH = (string)cmd_MaKH.ExecuteScalar();

            lbMaKH.Text = maKH;
            lbMaPT.Text = maPhieu;
            lbTuoiKH.Text = tuoi;
            lbTenKH.Text = tenKH;

            SqlDataAdapter sqlData_VX = new SqlDataAdapter("SELECT VX.MaLoai AS 'Loại Vacxine',VX.MaVacxin AS 'Mã Vacxine',PTCT.LieuLuong AS 'Liều Lượng' FROM dbo.PT_ChiTiet AS PTCT,dbo.Vacxin AS VX WHERE MaPhieuTiem ='" + maPhieu + "' AND PTCT.MaVacXin = VX.MaVacxin", sqlCon);
            SqlCommand cmd_LayVX = new SqlCommand("Lay_vacxine", sqlCon);
            cmd_LayVX.CommandType = CommandType.StoredProcedure;
            cmd_LayVX.Parameters.AddWithValue("@maPhieu", SqlDbType.Char).Value = maPhieu.Trim();
            DataTable dt_VX = new DataTable();

            dt_VX.Load(cmd_LayVX.ExecuteReader());
            dgvDSVacxine.DataSource = dt_VX;

            SqlCommand cmd_LayGhiChu = new SqlCommand("Lay_ghiChu_PhieuTiem", sqlCon);
            cmd_LayGhiChu.CommandType = CommandType.StoredProcedure;
            cmd_LayGhiChu.Parameters.AddWithValue("@maPhieu", SqlDbType.Char).Value = maPhieu.Trim();

            txtGhiChu.Text = (string)cmd_LayGhiChu.ExecuteScalar();

            dgvDSVacxine.Columns[0].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[0].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[0].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvDSVacxine.Columns[0].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dgvDSVacxine.Columns[0].HeaderCell.Style.BackColor = Color.White;
            dgvDSVacxine.Columns[0].DefaultCellStyle.BackColor = Color.White;

            dgvDSVacxine.Columns[1].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[1].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[1].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvDSVacxine.Columns[1].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dgvDSVacxine.Columns[1].HeaderCell.Style.BackColor = Color.White;
            dgvDSVacxine.Columns[1].DefaultCellStyle.BackColor = Color.White;

            dgvDSVacxine.Columns[2].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[2].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[2].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvDSVacxine.Columns[2].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dgvDSVacxine.Columns[2].HeaderCell.Style.BackColor = Color.White;
            dgvDSVacxine.Columns[2].DefaultCellStyle.BackColor = Color.White;

            dgvDSVacxine.EnableHeadersVisualStyles = false;
            sqlCon.Close();
        }

        private void btnTiemXong_Click(object sender, EventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(source);
            sqlCon.Open();

            SqlCommand cmd_UpdatePT = new SqlCommand("CapNhat_ghiChu_PhieuTiem", sqlCon);
            cmd_UpdatePT.CommandType = CommandType.StoredProcedure;
            cmd_UpdatePT.Parameters.AddWithValue("@ghiChu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
            cmd_UpdatePT.Parameters.AddWithValue("@maPhieu", SqlDbType.Char).Value = maPhieu.Trim();
            cmd_UpdatePT.ExecuteNonQuery();

            SqlCommand cmd_CapNhatThanhTich = new SqlCommand("CapNhat_ThanhTich", sqlCon);
            cmd_CapNhatThanhTich.CommandType = CommandType.StoredProcedure;
            cmd_CapNhatThanhTich.Parameters.AddWithValue("@maNV", SqlDbType.Char).Value = maBST.Trim();

            cmd_CapNhatThanhTich.ExecuteNonQuery();

            string qry_XoaPTCT = "DELETE FROM dbo.PT_ChiTiet WHERE MaPhieuTiem = '" + maPhieu + "' AND NgayHenTiem IS NULL";
            SqlCommand cmd_XoaPTCT = new SqlCommand(qry_XoaPTCT, sqlCon);
            cmd_XoaPTCT.ExecuteNonQuery();

            sqlCon.Close();
            FormListKH_BST_Load(sender, e);
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            scr_val = scr_val - 5;
            if (scr_val <= 0)
            {
                scr_val = 0;
            }
            ds.Clear();
            da.Fill(ds, scr_val, 5, "info");
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            scr_val = scr_val + 5;
            if (scr_val > soKH)
            {
                scr_val = 10;
            }
            ds.Clear();
            da.Fill(ds, scr_val, 5, "info");
        }
    }
}
