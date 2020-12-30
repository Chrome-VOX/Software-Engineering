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
    public partial class FormLichHen : Form
    {
        string maBSK = FormLogIn.maNV;
        ConnectionStringSettings conSet = ConfigurationManager.ConnectionStrings["MyDBConnectionString"];
        int scr_val;
        SqlDataAdapter da;
        DataSet ds;
        int soKH = 0;
        string maPhieu;
        public FormLichHen()
        {
            InitializeComponent();
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            while (true)
            {
                string ngayHenTiem_Temp = dtpkNgayHen.Value.ToShortDateString();
                string[] nHenTiem = ngayHenTiem_Temp.Split(new char[] { '/' });
                string namHenTiem = string.Concat(nHenTiem[2], "-", nHenTiem[0]);
                string ngayHenTiem = string.Concat(namHenTiem, "-", nHenTiem[1]);

                string source = conSet.ConnectionString;
                SqlConnection con = new SqlConnection(source);
                con.Open();
                string sql = "SELECT PhieuTiem.MaPhieuTiem AS 'Mã Phiếu Tiêm',KhachHang.TenKhachHang AS 'Họ Và Tên',MaVacXin AS 'Mã Vacxine',NgayHenTiem AS 'Ngày Hẹn' " +
                     "FROM dbo.PT_ChiTiet,dbo.PhieuTiem,dbo.KhachHang WHERE  NgayHenTiem = '" + ngayHenTiem + "' AND PT_ChiTiet.MaPhieuTiem = '" + txtMaPhieu.Text + "' " +
                     "AND PhieuTiem.MaPhieuTiem = PT_ChiTiet.MaPhieuTiem AND KhachHang.MaKH = PhieuTiem.MaKH";




                SqlCommand cmd_Check = new SqlCommand(sql, con);
                SqlDataReader dr_Check;
                dr_Check = cmd_Check.ExecuteReader();
                int dem = 0;
                while (dr_Check.Read())
                {
                    dem += 1;
                }
                dr_Check.Close();

                if (dem == 1)
                {
                    da = new SqlDataAdapter(sql, con);
                    ds = new DataSet();

                    //da.Fill(ds, "info");
                    da.Fill(ds, scr_val, 5, "info");
                    dgvListKH.DataSource = ds;
                    dgvListKH.DataMember = "info";
                    soKH = dgvListKH.Rows.Count;

                }
                else
                {
                    MessageBox.Show("Không có lịch hẹn này");
                    break;
                }

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

                dgvListKH.Columns[3].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
                dgvListKH.Columns[3].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                dgvListKH.Columns[3].HeaderCell.Style.ForeColor = Color.DodgerBlue;
                dgvListKH.Columns[3].DefaultCellStyle.ForeColor = Color.DodgerBlue;
                dgvListKH.Columns[3].HeaderCell.Style.BackColor = Color.White;
                dgvListKH.Columns[3].DefaultCellStyle.BackColor = Color.White;

                dgvListKH.EnableHeadersVisualStyles = false;


                #endregion
                con.Close();
                break;
            }
        } 
        private void FormLichHen_Load(object sender, EventArgs e)
        {
            if(FormLogIn.arrchucVu[0] == "QLTT")
            {
                btnXoa.Visible = false;
            }
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
            string sql = "SELECT PhieuTiem.MaPhieuTiem AS 'Mã Phiếu Tiêm',KhachHang.TenKhachHang AS 'Họ Và Tên',MaVacXin AS 'Mã Vacxine',NgayHenTiem AS 'Ngày Hẹn' " +
                      "FROM dbo.PT_ChiTiet,dbo.PhieuTiem,dbo.KhachHang WHERE  NgayHenTiem IS NOT NULL AND PhieuTiem.MaPhieuTiem = PT_ChiTiet.MaPhieuTiem AND KhachHang.MaKH = PhieuTiem.MaKH";
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "info");
            //da.Fill(ds, scr_val, 5, "info");
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

            dgvListKH.Columns[3].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvListKH.Columns[3].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvListKH.Columns[3].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvListKH.Columns[3].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dgvListKH.Columns[3].HeaderCell.Style.BackColor = Color.White;
            dgvListKH.Columns[3].DefaultCellStyle.BackColor = Color.White;

            dgvListKH.EnableHeadersVisualStyles = false;
            #endregion
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
        String value = "";
        private void dgvListKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            value=dgvListKH.Rows[e.RowIndex].Cells[0].Value.ToString();
            MessageBox.Show(value);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }
    }
}
