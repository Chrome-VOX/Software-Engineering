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
    public partial class FormSignUp : Form
    {
        ConnectionStringSettings conSet = ConfigurationManager.ConnectionStrings["MyDBConnectionString"];
        string maNV;

        public FormSignUp()
        {
            InitializeComponent();
        }

        #region Nhấp,rời chuột các textbox
        private void txtDangNhap_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtMatKhau_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtDangNhap_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtMatKhau_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtHoVaTen_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtHoVaTen_Leave(object sender, EventArgs e)
        {
            
        }



        private void txtQueQuan_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtQueQuan_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtSDT_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtSDT_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtMaNV_Enter(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "Mã Nhân Viên")
            {
                txtMaNV.Text = "";
            }
        }

        private void txtMaNV_Leave(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                txtMaNV.Text = "Mã Nhân Viên";
            }
        }
        #endregion

        private void cbxChucVu_TextChanged(object sender, EventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection cn = new SqlConnection(source);
            cn.Open();

            string[] maCV = cbxChucVu.Text.Split(new char[] { ' ' });
            string maNV_Moi;
            if (maCV[0] == "LT" || maCV[0] == "TK")
            {
                SqlCommand cmd_MaNV_LT = new SqlCommand("maNV_LeTan", cn);
                cmd_MaNV_LT.CommandType = CommandType.StoredProcedure;
                cmd_MaNV_LT.Parameters.AddWithValue("@MaChucVu", SqlDbType.Char).Value = maCV[0].Trim();
                int maNVLTorTK;
                //try
                //{
                //    maNVLTorTK = (int)(cmd_MaNV_LT.ExecuteScalar()) + 1;
                //}
                //catch (Exception)
                //{
                //    maNVLTorTK = 1;
                //}
                if(cmd_MaNV_LT.ExecuteScalar() == null)
                {
                    maNVLTorTK = 1;
                }
                else
                {
                    maNVLTorTK = (int)(cmd_MaNV_LT.ExecuteScalar()) + 1;
                }
                if (maNVLTorTK < 10)
                {
                    maNV_Moi = string.Concat("0", maNVLTorTK.ToString());
                }
                else maNV_Moi = maNVLTorTK.ToString();
                maNV = string.Concat(maCV[0], maNV_Moi);
                txtMaNV.Text = maNV;
            }
            else
            {
                SqlCommand cmd_MaNV_BS = new SqlCommand("maNV_BacSi", cn);
                cmd_MaNV_BS.CommandType = CommandType.StoredProcedure;
                cmd_MaNV_BS.Parameters.AddWithValue("@MaChucVu", SqlDbType.Char).Value = maCV[0];
                int maNVBSKorBST;
                //try
                //{
                //    maNVBSKorBST = (int)(cmd_MaNV_BS.ExecuteScalar()) + 1;
                //}
                //catch(Exception)
                //{
                //    maNVBSKorBST = 1;
                //}
                if(cmd_MaNV_BS.ExecuteScalar() == null)
                {
                    maNVBSKorBST = 1;
                }
                else
                {
                    maNVBSKorBST = (int)(cmd_MaNV_BS.ExecuteScalar()) + 1;
                }
                if (maNVBSKorBST < 10)
                {
                    maNV_Moi = string.Concat("0", maNVBSKorBST.ToString());
                }
                else maNV_Moi = maNVBSKorBST.ToString();
                maNV = string.Concat(maCV[0], maNV_Moi);
                txtMaNV.Text = maNV;
            }

            cn.Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection cn = new SqlConnection(source);
            cn.Open();
            while (true)
            {
                if (txtDangNhap.Text == "Tên Đăng Nhập" || txtMatKhau.Text == "Mật Khẩu" || txtHoVaTen.Text == "Họ Và Tên" || cbxChucVu.Text == "Chức Vụ")
                {
                    MessageBox.Show("Điền đầy đủ những mục có dấu *");
                    break;
                }

                int dem_TenDangNhap = 0;



                #region Kiểm tra trùng email
                SqlCommand cmd_CheckTenDangNhap = new SqlCommand("KiemTraEmail", cn);
                cmd_CheckTenDangNhap.CommandType = CommandType.StoredProcedure;
                cmd_CheckTenDangNhap.Parameters.AddWithValue("@Email", SqlDbType.Char).Value = txtDangNhap.Text.Trim();
                SqlDataReader dr_CheckTenDangNhap;
                dr_CheckTenDangNhap = cmd_CheckTenDangNhap.ExecuteReader();
                while (dr_CheckTenDangNhap.Read())
                {
                    dem_TenDangNhap += 1;
                }
                if (dem_TenDangNhap >= 1)
                {
                    MessageBox.Show("Email đã được đăng kí");
                    cn.Close();
                    break;
                }
                dr_CheckTenDangNhap.Close();
                #endregion

                // Nếu k trùng email
                if (dem_TenDangNhap < 1)
                {
                    String qry_1 = "insert into DangNhap (email,matKhau) values ('" + txtDangNhap.Text + "','" + txtMatKhau.Text + "')";
                    SqlCommand cmd_ThemTaiKhoan = new SqlCommand("ThemTaiKhoan", cn);
                    cmd_ThemTaiKhoan.CommandType = CommandType.StoredProcedure;
                    cmd_ThemTaiKhoan.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = txtDangNhap.Text.Trim();
                    cmd_ThemTaiKhoan.Parameters.AddWithValue("@matKhau", SqlDbType.VarChar).Value = txtMatKhau.Text.Trim();

                    //cmd_ThemTaiKhoan.Parameters.AddWithValue("@Email",txtDangNhap.Text.Trim());
                    //cmd_ThemTaiKhoan.Parameters.AddWithValue("@matKhau", txtMatKhau.Text.Trim());


                    cmd_ThemTaiKhoan.ExecuteNonQuery();

                    string[] maCV = cbxChucVu.Text.Split(new char[] { ' ' });

                    string ngaySinh_Temp = dtNgaySinh.Value.ToString("yyyy-M-dd");
                    //string[] nSinh = ngaySinh_Temp.Split(new char[] { '/' });
                    //string namSinh = string.Concat(nSinh[2], "-", nSinh[0]);
                    //string ngaySinh = string.Concat(namSinh, "-", nSinh[1]);

                    string ngayVaoLam_Temp = dtNgayVaoLam.Value.ToString("yyyy-M-dd");
                    //string[] nVaoLam = ngayVaoLam_Temp.Split(new char[] { '/' });
                    //string namVaoLam = string.Concat(nVaoLam[2], "-", nVaoLam[0]);
                    //string ngayVaoLam = string.Concat(namVaoLam, "-", nVaoLam[1]);

                    SqlCommand cmd_ThemNhanVien = new SqlCommand("ThemNhanVien", cn);
                    cmd_ThemNhanVien.CommandType = CommandType.StoredProcedure;
                    cmd_ThemNhanVien.Parameters.AddWithValue("@MaNV", SqlDbType.VarChar).Value = maNV.Trim();
                    cmd_ThemNhanVien.Parameters.AddWithValue("@HoTen", SqlDbType.NVarChar).Value = txtHoVaTen.Text;
                    cmd_ThemNhanVien.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = txtDangNhap.Text.Trim();
                    cmd_ThemNhanVien.Parameters.AddWithValue("@NgaySinh", SqlDbType.Char).Value = ngaySinh_Temp.Trim();
                    cmd_ThemNhanVien.Parameters.AddWithValue("@QueQuan", SqlDbType.NVarChar).Value = txtQueQuan.Text;
                    cmd_ThemNhanVien.Parameters.AddWithValue("@NgayVaoLam", SqlDbType.Char).Value = ngayVaoLam_Temp.Trim();
                    cmd_ThemNhanVien.Parameters.AddWithValue("@GioiTinh", SqlDbType.NVarChar).Value = cbxGioiTinh.Text.Trim();
                    cmd_ThemNhanVien.Parameters.AddWithValue("@SDT", SqlDbType.Char).Value = txtSDT.Text.Trim();
                    cmd_ThemNhanVien.Parameters.AddWithValue("@MaChucVu", SqlDbType.VarChar).Value = maCV[0].Trim();

                    //cmd_ThemNhanVien.Parameters.AddWithValue("@MaNV", maNV.Trim()) ;
                    //cmd_ThemNhanVien.Parameters.AddWithValue("@HoTen", txtHoVaTen.Text);
                    //cmd_ThemNhanVien.Parameters.AddWithValue("@Email", txtDangNhap.Text.Trim()); 
                    //cmd_ThemNhanVien.Parameters.AddWithValue("@NgaySinh", ngaySinh_Temp.Trim());
                    //cmd_ThemNhanVien.Parameters.AddWithValue("@QueQuan", txtQueQuan.Text);
                    //cmd_ThemNhanVien.Parameters.AddWithValue("@NgayVaoLam",ngayVaoLam_Temp.Trim()) ;
                    //cmd_ThemNhanVien.Parameters.AddWithValue("@GioiTinh", cbxGioiTinh.Text.Trim()) ;
                    //cmd_ThemNhanVien.Parameters.AddWithValue("@SDT", txtSDT.Text.Trim());
                    //cmd_ThemNhanVien.Parameters.AddWithValue("@MaChucVu", maCV[0].Trim());

                    cmd_ThemNhanVien.ExecuteNonQuery();

                    // Tao thanh tich = 0 cho nhan vien moi
                    SqlCommand cmd_ThemThanhTich = new SqlCommand("ThemThanhTich", cn);
                    cmd_ThemThanhTich.CommandType = CommandType.StoredProcedure;
                    cmd_ThemThanhTich.Parameters.AddWithValue("@MaNV", SqlDbType.VarChar).Value = maNV.Trim();
                    cmd_ThemThanhTich.Parameters.AddWithValue("@soKH", SqlDbType.Char).Value = 0;

                    //cmd_ThemThanhTich.Parameters.AddWithValue("@MaNV", maNV.Trim());
                    //cmd_ThemThanhTich.Parameters.AddWithValue("@soKH", "0" );


                    cmd_ThemThanhTich.ExecuteNonQuery();

                    MessageBox.Show("Đăng kí thành công");
                    cn.Close();
                    this.Close();
                    //FormLogIn fli = new FormLogIn();
                    //fli.ShowDialog();
                    break;
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDangNhap_Leave_1(object sender, EventArgs e)
        {
            if (txtDangNhap.Text == "")
            {
                txtDangNhap.Text = "Email";
            }
        }

        private void txtDangNhap_Enter_1(object sender, EventArgs e)
        {
            if (txtDangNhap.Text == "Email")
            {
                txtDangNhap.Text = "";
            }
        }

        private void txtMatKhau_Leave_1(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "")
            {
                txtMatKhau.Text = "Mật Khẩu";
            }
        }

        private void txtMatKhau_Enter_1(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "Mật Khẩu")
            {
                txtMatKhau.Text = "";
            }
        }

        private void txtHoVaTen_Leave_1(object sender, EventArgs e)
        {
            if (txtHoVaTen.Text == "")
            {
                txtHoVaTen.Text = "Họ Và Tên";
            }
        }

        private void txtHoVaTen_Enter_1(object sender, EventArgs e)
        {
            if (txtHoVaTen.Text == "Họ Và Tên")
            {
                txtHoVaTen.Text = "";
            }
        }

        private void txtQueQuan_Leave_1(object sender, EventArgs e)
        {
            if (txtQueQuan.Text == "")
            {
                txtQueQuan.Text = "Quê Quán";
            }
        }

        private void txtQueQuan_Enter_1(object sender, EventArgs e)
        {
            if (txtQueQuan.Text == "Quê Quán")
            {
                txtQueQuan.Text = "";
            }
        }

        private void txtSDT_Leave_1(object sender, EventArgs e)
        {
            
            if (txtSDT.Text == "")
            {
                txtSDT.Text = "Số Điện Thoại";
            }
        }

        private void txtSDT_Enter_1(object sender, EventArgs e)
        {
            if (txtSDT.Text == "Số Điện Thoại")
            {
                txtSDT.Text = "";
            }

        }
    }
}
