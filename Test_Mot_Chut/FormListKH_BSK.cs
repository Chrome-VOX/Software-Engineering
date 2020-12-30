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
    public partial class FormListKH_BSK : Form
    {
        string maBSK = FormLogIn.maNV;
        ConnectionStringSettings conSet = ConfigurationManager.ConnectionStrings["MyDBConnectionString"];
        string maPhieu;
        string[] maVX;
        public FormListKH_BSK()
        {
            InitializeComponent();
            scr_val = 0;
        }
        int scr_val;
        SqlDataAdapter da;
        DataSet ds;
        int soKH = 0;

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

        private void btnKhamXong_Click(object sender, EventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(source);
            sqlCon.Open();

            string query_UpdatePT = "UPDATE dbo.PhieuTiem SET GhiChu = '" + txtGhiChu.Text + "' WHERE MaPhieuTiem = '" + maPhieu + "'";
            SqlCommand cmd_UpdatePT = new SqlCommand("CapNhat_ghiChu_PhieuTiem", sqlCon);
            cmd_UpdatePT.CommandType = CommandType.StoredProcedure;
            cmd_UpdatePT.Parameters.AddWithValue("@ghiChu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
            cmd_UpdatePT.Parameters.AddWithValue("@maPhieu", SqlDbType.Char).Value = maPhieu.Trim();
            cmd_UpdatePT.ExecuteNonQuery();

            SqlCommand cmd_CapNhatThanhTich = new SqlCommand("CapNhat_ThanhTich", sqlCon);
            cmd_CapNhatThanhTich.CommandType = CommandType.StoredProcedure;
            cmd_CapNhatThanhTich.Parameters.AddWithValue("@maNV", SqlDbType.Char).Value = maBSK.Trim();

            cmd_CapNhatThanhTich.ExecuteNonQuery();

            //this.Close();
            sqlCon.Close();
            FormListKH_BSK_Load(sender, e);
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
            cmd_MaKH.Parameters.AddWithValue("@maBSK", SqlDbType.Char).Value = maBSK.Trim();
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

            #region đổi thuộc tính dgv
            dgvDSVacxine.DataSource = dt_VX;

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
            #endregion

            sqlCon.Close();
        }

        private void dgvDSVacxine_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(source);
            sqlCon.Open();

            int selectedRowIndex = e.RowIndex;

            DataGridViewRow row = new DataGridViewRow();

            row = dgvDSVacxine.Rows[selectedRowIndex];

            txtLoaiVX.Text = row.Cells[0].Value.ToString();


            maVX = row.Cells[1].Value.ToString().Split(' ');

            SqlCommand cmd_tenVX = new SqlCommand("ThongTin_Vacxine", sqlCon);
            cmd_tenVX.CommandType = CommandType.StoredProcedure;
            cmd_tenVX.Parameters.AddWithValue("@maVX", SqlDbType.Char).Value = maVX[0].Trim();
            txtTenVX.Text = (string)cmd_tenVX.ExecuteScalar();

            sqlCon.Close();
        }

        private void FormListKH_BSK_Load(object sender, EventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection con = new SqlConnection(source);
            //sqlCon.Open();


            //   SqlCommand cmd_ThongTinPTiem = new SqlCommand("XuatPTiem_TheoBSK", sqlCon);
            //   cmd_ThongTinPTiem.CommandType = CommandType.StoredProcedure;
            //   cmd_ThongTinPTiem.Parameters.AddWithValue("@maBSK", SqlDbType.Char).Value = maBSK.Trim();
            //DataTable dt_KH = new DataTable();

            //dt_KH.Load(cmd_ThongTinPTiem.ExecuteReader());
            //dgvListKH.DataSource = dt_KH;



            //sqlCon.Close();
            string sql = "SELECT MaPhieuTiem AS 'Mã PT',TenKhachHang AS 'Tên Khách Hàng',Tuoi AS 'Tuổi' " +
                 " FROM dbo.KhachHang,dbo.PhieuTiem WHERE KhachHang.MaKH = PhieuTiem.MaKH AND MaBSKham = '" + maBSK + "' AND GhiChu IS NULL";
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            con.Open();
            //da.Fill(ds, "info");
            da.Fill(ds, scr_val, 5, "info");
            dgvListKH.DataSource = ds;
            dgvListKH.DataMember = "info";
            soKH = dgvListKH.Rows.Count;


            con.Close();
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
    }
}
