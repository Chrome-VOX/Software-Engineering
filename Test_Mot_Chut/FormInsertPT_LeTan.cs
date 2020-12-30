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
    public partial class FormInsertPT_LeTan : Form
    {
        ConnectionStringSettings conSet = ConfigurationManager.ConnectionStrings["MyDBConnectionString"];
        string sqlSource;
        public string maKH; // Biến này để lưu lại mã khách hàng từ FormCustomer ( public để gọi đc ở bên formCustomer )
        public string maPT;
        string GioTiem;
        string ngayHenTiem_Temp;
        bool phieuTiem_Moi = true; // Biến kiểm soát khi ấn Thêm chỉ đưa Phiếu Tiêm vào database 1 lần
        public FormInsertPT_LeTan()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            while (true)
            {
                #region Đã điền đủ thông tin chưa

                if (cbxBSK.Text == "")
                {
                    MessageBox.Show("Chưa chọn bác sĩ khám");
                    break;
                }

                if (cbxBST.Text == "")
                {
                    MessageBox.Show("Chưa chọn bác sĩ tiêm");
                    break;
                }

                if (cbxLoaiVacxine.Text == "")
                {
                    MessageBox.Show("Chưa chọn loại vacxine");
                    break;
                }

                if (cbxMaVX.Text == "")
                {
                    MessageBox.Show("Chưa chọn mã vacxine");
                    break;
                }

                if (txtLieuLuong.Text == "")
                {
                    MessageBox.Show("Chưa ghi liều lượng");
                    break;
                }

                if (chkMuiTiepTheo.Checked == true)
                {
                    if (dtMuiTiepTheo.Value <= dtNgayTiem.Value)
                    {
                        MessageBox.Show("Ngày hẹn phải sau ngày hôm nay");
                        break;
                    }
                }
                #endregion

                // Lưu giờ Tiêm
                GioTiem = string.Concat(nudHour.Value.ToString(), "h", nudMinute.Value.ToString());

                SqlConnection sqlCon = new SqlConnection(sqlSource);
                sqlCon.Open();

                #region Kiểm tra số vacxine còn lại và cập nhật số vacxine
                SqlCommand cmd_soLuongVX = new SqlCommand("Lay_SoLuongVX_ConLai", sqlCon);
                //sqlCon.Open();
                cmd_soLuongVX.CommandType = CommandType.StoredProcedure;
                cmd_soLuongVX.Parameters.AddWithValue("@maVacxine", SqlDbType.Char).Value = cbxMaVX.Text.Trim();
                int soLuongVX;
                soLuongVX = (int)cmd_soLuongVX.ExecuteScalar();

                if (soLuongVX == 0)
                {
                    MessageBox.Show("Vacxine này đã hết");
                    break;
                }

                if (soLuongVX < Convert.ToInt32(txtLieuLuong.Text))
                {
                    MessageBox.Show("Số lượng Vacxine còn lại không đủ");
                    break;
                }
                #endregion


                // Đưa phiếu tiêm vào database
                if (phieuTiem_Moi == true)
                {
                    //string ngayTiem_Temp = dtNgayTiem.Value.ToShortDateString();
                    string ngayTiem_Temp = dtNgayTiem.Value.ToString("yyyy-M-dd");
                    //string[] nTiem = ngayTiem_Temp.Split(new char[] { '/' });
                    //string namTiem = string.Concat(nTiem[2], "-", nTiem[0]);
                    // string ngayTiem = string.Concat(namTiem, "-", nTiem[1]);

                    SqlCommand cmd_InsertPT = new SqlCommand("Them_PhieuTiem", sqlCon);
                    cmd_InsertPT.CommandType = CommandType.StoredProcedure;
                    cmd_InsertPT.Parameters.AddWithValue("@MaPhieuTiem", SqlDbType.VarChar).Value = txtMaPhieuTiem.Text.Trim();
                    cmd_InsertPT.Parameters.AddWithValue("@MaKH", SqlDbType.VarChar).Value = txtMaKhachHang.Text.Trim();
                    cmd_InsertPT.Parameters.AddWithValue("@NgayTiem", SqlDbType.Date).Value = ngayTiem_Temp.Trim();
                    cmd_InsertPT.Parameters.AddWithValue("@MaNVphieu", SqlDbType.VarChar).Value = txtLTPhieu.Text.Trim();
                    cmd_InsertPT.Parameters.AddWithValue("@MaBSKham", SqlDbType.VarChar).Value = cbxBSK.Text.Trim();
                    cmd_InsertPT.Parameters.AddWithValue("@maNVtiem", SqlDbType.VarChar).Value = cbxBST.Text.Trim();
                    cmd_InsertPT.Parameters.AddWithValue("@TongTien", SqlDbType.Int).Value = Convert.ToInt32(lbTongTien.Text);
                    cmd_InsertPT.ExecuteNonQuery();
                    phieuTiem_Moi = false;
                }


                // Lấy giá tiền vacxine vừa được chọn
                //sqlCon.Close();
                
                SqlCommand cmd_giaVacxine = new SqlCommand("Gia_Vacxine", sqlCon);
                //sqlCon.Open();
                cmd_giaVacxine.CommandType = CommandType.StoredProcedure;
                cmd_giaVacxine.Parameters.AddWithValue("@MaVacxin", SqlDbType.VarChar).Value = cbxMaVX.Text.Trim();
                int giaVacxine;
                giaVacxine = (int)cmd_giaVacxine.ExecuteScalar();

                // Cập nhật tổng tiền
                lbTongTien.Text = (Convert.ToInt32(lbTongTien.Text) + giaVacxine).ToString();

                SqlCommand cmd_CapNhat_TongTien = new SqlCommand("CapNhat_TongTien", sqlCon);
                cmd_CapNhat_TongTien.CommandType = CommandType.StoredProcedure;
                cmd_CapNhat_TongTien.Parameters.AddWithValue("@TongTien", SqlDbType.Int).Value = Convert.ToInt32(lbTongTien.Text);
                cmd_CapNhat_TongTien.Parameters.AddWithValue("@MaPhieuTiem", SqlDbType.Char).Value = txtMaPhieuTiem.Text.Trim();

                cmd_CapNhat_TongTien.ExecuteNonQuery();


                #region Lưu phiếu tiêm chi tiết

                // Nếu có ngày hẹn tiêm mũi tiếp theo
                if (chkMuiTiepTheo.Checked == true)
                {
                    ngayHenTiem_Temp = dtMuiTiepTheo.Value.ToString("yyyy-M-dd");
                    //string[] nHenTiem = ngayHenTiem_Temp.Split(new char[] { '/' });
                    //string namHenTiem = string.Concat(nHenTiem[2], "-", nHenTiem[0]);
                    //ngayHenTiem = string.Concat(namHenTiem, "-", nHenTiem[1]);


                    SqlCommand cmd_Them_PT_ChiTiet = new SqlCommand("Them_PT_ChiTiet", sqlCon);
                    cmd_Them_PT_ChiTiet.CommandType = CommandType.StoredProcedure;
                    cmd_Them_PT_ChiTiet.Parameters.AddWithValue("@MaPhieuTiem", SqlDbType.Int).Value = txtMaPhieuTiem.Text.Trim();
                    cmd_Them_PT_ChiTiet.Parameters.AddWithValue("@MaVacXin", SqlDbType.Char).Value = cbxMaVX.Text.Trim();
                    cmd_Them_PT_ChiTiet.Parameters.AddWithValue("@LieuLuong", SqlDbType.Int).Value = txtLieuLuong.Text.Trim();
                    cmd_Them_PT_ChiTiet.Parameters.AddWithValue("@GioTiem", SqlDbType.Char).Value = GioTiem;
                    cmd_Them_PT_ChiTiet.Parameters.AddWithValue("@ThanhTien", SqlDbType.Int).Value = giaVacxine;
                    cmd_Them_PT_ChiTiet.Parameters.AddWithValue("@NgayHenTiem", SqlDbType.Char).Value = ngayHenTiem_Temp.Trim();
                    cmd_Them_PT_ChiTiet.ExecuteNonQuery();
                }
                // Nếu k có
                else
                {
                    SqlCommand cmd_Them_PT_ChiTiet_KhongHen = new SqlCommand("Them_PT_ChiTiet_KhongHen", sqlCon);
                    cmd_Them_PT_ChiTiet_KhongHen.CommandType = CommandType.StoredProcedure;
                    cmd_Them_PT_ChiTiet_KhongHen.Parameters.AddWithValue("@MaPhieuTiem", SqlDbType.Char).Value = txtMaPhieuTiem.Text.Trim();
                    cmd_Them_PT_ChiTiet_KhongHen.Parameters.AddWithValue("@MaVacXin", SqlDbType.Char).Value = cbxMaVX.Text.Trim();
                    cmd_Them_PT_ChiTiet_KhongHen.Parameters.AddWithValue("@LieuLuong", SqlDbType.Char).Value = txtLieuLuong.Text.Trim();
                    cmd_Them_PT_ChiTiet_KhongHen.Parameters.AddWithValue("@GioTiem", SqlDbType.Char).Value = GioTiem.Trim();
                    cmd_Them_PT_ChiTiet_KhongHen.Parameters.AddWithValue("@ThanhTien", SqlDbType.Int).Value = giaVacxine;

                    cmd_Them_PT_ChiTiet_KhongHen.ExecuteNonQuery();

                    ngayHenTiem_Temp = "";
                }

                // Đẩy danh sách vacxine đc chọn lên dataGridview
                dgvDSVacxine.Rows.Add(cbxLoaiVacxine.Text, cbxMaVX.Text, ngayHenTiem_Temp);

                #endregion 

                sqlCon.Close();
                break;
            }
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(source);
            sqlCon.Open();

            SqlCommand cmd_CapNhatThanhTich = new SqlCommand("CapNhat_ThanhTich", sqlCon);
            cmd_CapNhatThanhTich.CommandType = CommandType.StoredProcedure;
            cmd_CapNhatThanhTich.Parameters.AddWithValue("@maNV", SqlDbType.Char).Value = FormLogIn.maNV.Trim();

            cmd_CapNhatThanhTich.ExecuteNonQuery();
            sqlCon.Close();
            this.Close();
        }

        private void chkMuiTiepTheo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void FormInsertPT_LeTan_Load(object sender, EventArgs e)
        {
            sqlSource = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(sqlSource);
            phieuTiem_Moi = true;

            sqlCon.Open();
            #region Đẩy các mã nhân viên đang có trong CSDL vào combobox  
            //SqlDataAdapter sqlData_BSK = new SqlDataAdapter("SELECT MaNV FROM dbo.NhanVien WHERE MaChucVu = 'BSK'", sqlCon);
            //SqlDataAdapter sqlData_BST = new SqlDataAdapter("SELECT MaNV FROM dbo.NhanVien WHERE MaChucVu = 'BST'", sqlCon);

            SqlCommand cmd_maBSK = new SqlCommand("Cac_BSK_dang_co", sqlCon);
            SqlCommand cmd_maBST = new SqlCommand("Cac_BST_dang_co", sqlCon);

            cmd_maBSK.CommandType = CommandType.StoredProcedure;
            cmd_maBST.CommandType = CommandType.StoredProcedure;
            DataTable dt_BSK = new DataTable();
            DataTable dt_BST = new DataTable();

            dt_BSK.Load(cmd_maBSK.ExecuteReader());
            dt_BST.Load(cmd_maBST.ExecuteReader());

            foreach (DataRow dr in dt_BSK.Rows)
            {
                cbxBSK.Items.Add(dr["MaNV"].ToString());
            }

            foreach (DataRow dr in dt_BST.Rows)
            {
                cbxBST.Items.Add(dr["MaNV"].ToString());
            }


            #endregion

            #region Đẩy Loại Vacxine vào từ database
            SqlCommand cmd_loaiVX = new SqlCommand("LoaiVacXine", sqlCon);
            cmd_loaiVX.CommandType = CommandType.StoredProcedure;

            DataTable dt_LoaiVX = new DataTable();
            dt_LoaiVX.Load(cmd_loaiVX.ExecuteReader());

            foreach (DataRow dr in dt_LoaiVX.Rows)
            {
                cbxLoaiVacxine.Items.Add(dr["MaLoai"].ToString());
            }
            #endregion 

            #region Điền tự động mã Phiếu Tiêm và mã Khách Hàng
            txtLTPhieu.Text = FormLogIn.maNV;
            txtMaKhachHang.Text = maKH; // mã KH



            SqlCommand cmd_maPT = new SqlCommand("ma_PhieuTiem", sqlCon);
            cmd_maPT.CommandType = CommandType.StoredProcedure;
            int stt;
            //try
            //{
            //    stt = (int)cmd_maPT.ExecuteScalar() + 1;
            //}
            //catch(Exception)
            //{
            //    stt = 1;
            //}
            if(cmd_maPT.ExecuteScalar() == null)
            {
                stt = 1;
            }
            else
            {
                stt = (int)cmd_maPT.ExecuteScalar() + 1;
            }
            
            if (stt < 10)
            {
                maPT = string.Concat("PT0", stt.ToString());
            }
            else maPT = string.Concat("PT", stt.ToString());
            txtMaPhieuTiem.Text = maPT;
            #endregion

            #region Đổi màu chữ trên datagridview
            // fore: màu chữ
            // back: màu nền
            // header cell : hàng đầu
            // default cell : từ hàng t2 trở đi
            dgvDSVacxine.Columns[0].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[0].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[0].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvDSVacxine.Columns[0].HeaderCell.Style.BackColor = Color.White;
            dgvDSVacxine.Columns[0].DefaultCellStyle.BackColor = Color.White;

            dgvDSVacxine.Columns[1].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[1].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[1].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvDSVacxine.Columns[1].HeaderCell.Style.BackColor = Color.White;
            dgvDSVacxine.Columns[1].DefaultCellStyle.BackColor = Color.White;

            dgvDSVacxine.Columns[2].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[2].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDSVacxine.Columns[2].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dgvDSVacxine.Columns[2].HeaderCell.Style.BackColor = Color.White;
            dgvDSVacxine.Columns[2].DefaultCellStyle.BackColor = Color.White;


            dgvDSVacxine.EnableHeadersVisualStyles = false;
            #endregion 

            sqlCon.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            sqlSource = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(sqlSource);
            sqlCon.Open();

            // Nếu tắt khi tạo phiếu giữa chừng thì xóa luôn khách hàng đang tạo phiếu
            SqlCommand cmd_xoaPTCT = new SqlCommand("xoa_PTCT", sqlCon);
            cmd_xoaPTCT.CommandType = CommandType.StoredProcedure;
            cmd_xoaPTCT.Parameters.AddWithValue("@maPhieuTiem", SqlDbType.Char).Value = maPT;
            cmd_xoaPTCT.CommandType = CommandType.StoredProcedure;
            cmd_xoaPTCT.ExecuteNonQuery();

            SqlCommand cmd_xoaKH = new SqlCommand("Xoa_KhachHang", sqlCon);
            cmd_xoaKH.CommandType = CommandType.StoredProcedure;
            cmd_xoaKH.Parameters.AddWithValue("@maKH", SqlDbType.Char).Value = txtMaKhachHang.Text.Trim();
            cmd_xoaKH.ExecuteNonQuery();

            

            sqlCon.Close();
            this.Close();
        }

        private void cbxLoaiVacxine_TextChanged(object sender, EventArgs e)
        {
            // Xóa các item mã vacxin cũ
            cbxMaVX.Items.Clear();

            // Lấy mã vacxin theo mã loại
            SqlConnection sqlCon = new SqlConnection(sqlSource);
            sqlCon.Open();

            SqlCommand cmd_maVX = new SqlCommand("Vacxine_TheoLoai", sqlCon);
            cmd_maVX.CommandType = CommandType.StoredProcedure;
            cmd_maVX.Parameters.AddWithValue("@LoaiVX", SqlDbType.Char).Value = cbxLoaiVacxine.Text.Trim();
            DataTable dt_maVX = new DataTable();
            dt_maVX.Load(cmd_maVX.ExecuteReader());

            foreach (DataRow dr in dt_maVX.Rows)
            {
                cbxMaVX.Items.Add(dr["MaVacxin"].ToString());
            }
            sqlCon.Close();
        }

        private void chkMuiTiepTheo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMuiTiepTheo.Checked == true)
            {
                dtMuiTiepTheo.Enabled = true;
                nudHour.Enabled = true;
                nudMinute.Enabled = true;
            }
            else
            {
                dtMuiTiepTheo.Enabled = false;
                nudHour.Enabled = false;
                nudMinute.Enabled = false;
            }
        }
    }
}
