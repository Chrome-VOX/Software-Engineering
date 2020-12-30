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
    public partial class FormLogIn : Form
    {
        ConnectionStringSettings conSet = ConfigurationManager.ConnectionStrings["MyDBConnectionString"];
        public static string[] arrchucVu;
        public static string maNV;

        #region Khi nhap, tha chuot vao cac o Text: Enter la nhan vao, Leave la tha chuot ra
       

        #endregion 
        public FormLogIn()
        {
            InitializeComponent();
        }

        private void linkLBForgotPassw_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false;
            FormVerifyCode fvc = new FormVerifyCode();
            fvc.ShowDialog();
            this.Visible = true;
        }

        private void btnTurnOff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLBNewRegis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false;
            FormSignUp form3 = new FormSignUp();
            form3.ShowDialog();
            this.Visible = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            while (true)
            {
                #region Kiểm tra nhập đủ email và pass chưa
                if (txtDangNhap.Text == "Email")
                {
                    MessageBox.Show("Chưa nhập email");
                    break;
                }

                if (txtMatKhau.Text == "Mật Khẩu")
                {
                    MessageBox.Show("Chưa nhập mật khẩu");
                    break;
                }
                #endregion

                string source = conSet.ConnectionString;
                SqlConnection cn = new SqlConnection(source);
                cn.Open();

                SqlCommand cmd_Check = new SqlCommand("KiemTra_DangNhap", cn);
                cmd_Check.CommandType = CommandType.StoredProcedure;
                cmd_Check.Parameters.AddWithValue("@Email", SqlDbType.Char).Value = txtDangNhap.Text.Trim();
                cmd_Check.Parameters.AddWithValue("@matKhau", SqlDbType.Char).Value = txtMatKhau.Text.Trim();
                SqlDataReader dr_Check;
                dr_Check = cmd_Check.ExecuteReader();
                int dem = 0;
                while (dr_Check.Read())
                {
                    dem += 1;
                }
                dr_Check.Close();

                // Xem ma Chuc vu va ma nhan vien cua nguoi vua dang nhap 
                if (dem == 1)
                {
                    SqlCommand cmd_ChucVu = new SqlCommand("Xem_MaChucVu", cn);
                    SqlCommand cmd_maNV = new SqlCommand("Xem_MaNhanVien", cn);
                    cmd_ChucVu.CommandType = CommandType.StoredProcedure;
                    cmd_maNV.CommandType = CommandType.StoredProcedure;
                    cmd_ChucVu.Parameters.AddWithValue("@Email", SqlDbType.Char).Value = txtDangNhap.Text;
                    cmd_maNV.Parameters.AddWithValue("@Email", SqlDbType.Char).Value = txtDangNhap.Text;
                    string chucVu = (string)cmd_ChucVu.ExecuteScalar();
                    arrchucVu = chucVu.Split(' ');
                    maNV = (string)cmd_maNV.ExecuteScalar();
                    this.Hide();
                    FormMain fMain = new FormMain();
                    fMain.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai !");
                }

                cn.Close();
                break;
            }
        }

        private void txtDangNhap_Leave(object sender, EventArgs e)
        {
            if (txtDangNhap.Text == "")
            {
                txtDangNhap.Text = "Username";
            }
        }

        private void txtDangNhap_Enter(object sender, EventArgs e)
        {
            txtDangNhap.Text = "";
        }

        private void txtMatKhau_Leave(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "")
            {
                txtMatKhau.Text = "Password";
            }
        }

        private void txtMatKhau_Enter(object sender, EventArgs e)
        {
            txtMatKhau.Text = "";
        }
    }
}
