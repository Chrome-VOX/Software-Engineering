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

    public partial class FormCustomer : Form
    {
        ConnectionStringSettings conSet = ConfigurationManager.ConnectionStrings["MyDBConnectionString"];
        string maKH;
        public FormCustomer()
        {
            InitializeComponent();
        }

        private void txtTenKH_Enter(object sender, EventArgs e)
        {
            if (txtTenKH.Text == "Họ và Tên")
            {
                txtTenKH.Text = "";
            }
        }

        private void txtTenKH_Leave(object sender, EventArgs e)
        {
            if (txtTenKH.Text == "")
            {
                txtTenKH.Text = "Họ và Tên";
            }
        }

        private void txtCMND_Leave(object sender, EventArgs e)
        {
            if (txtCMND.Text == "")
            {
                txtCMND.Text = "Số CMND";
            }
        }

        private void txtCMND_Enter(object sender, EventArgs e)
        {
            if (txtCMND.Text == "Số CMND")
            {
                txtCMND.Text = "";
            }
        }

        private void txtTenBM_Leave(object sender, EventArgs e)
        {
            if (txtTenBM.Text == "")
            {
                txtTenBM.Text = "Tên Bố Mẹ ( < 8 tuổi )";
            }
        }

        private void txtTenBM_Enter(object sender, EventArgs e)
        {
            if (txtTenBM.Text == "Tên Bố Mẹ ( < 8 tuổi )")
            {
                txtTenBM.Text = "";
            }
        }

        private void txtSDT_Leave(object sender, EventArgs e)
        {
            if (txtSDT.Text == "")
            {
                txtSDT.Text = "Số Điện Thoại";
            }
        }

        private void txtSDT_Enter(object sender, EventArgs e)
        {
            if (txtSDT.Text == "Số Điện Thoại")
            {
                txtSDT.Text = "";
            }
        }

        private void txtDiaChi_Leave(object sender, EventArgs e)
        {
            if (txtDiaChi.Text == "")
            {
                txtDiaChi.Text = "Địa Chỉ";
            }
        }

        private void txtDiaChi_Enter(object sender, EventArgs e)
        {
            if(txtDiaChi.Text == "Địa Chỉ")
            {
                txtDiaChi.Text = "";
            }
        }

        private void txtTuoi_Leave(object sender, EventArgs e)
        {
            if(txtTuoi.Text == "")
            {
                txtTuoi.Text = "Tuổi";
            }
        }

        private void txtTuoi_Enter(object sender, EventArgs e)
        {
            if (txtTuoi.Text == "Tuổi")
            {
                txtTuoi.Text = "";
            }
        }

        private void btnNhapPT_Click(object sender, EventArgs e)
        {
            string tenBM;
            string source = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(source);
            sqlCon.Open();
            while (true)
            {
                if (txtCMND.Text == "Số CMND" || txtTenKH.Text == "Họ và Tên" || txtSDT.Text == "Số Điện Thoại" || txtTuoi.Text == "Tuổi")
                {
                    MessageBox.Show("Điền đầy đủ những mục có dấu *");
                    break;
                }


                int dem_CMND = 0;
                int dem_SDT = 0;

                #region Check trùng CMND
                SqlCommand cmd_CheckCMND = new SqlCommand("KiemTra_CMND", sqlCon);
                cmd_CheckCMND.CommandType = CommandType.StoredProcedure;
                cmd_CheckCMND.Parameters.AddWithValue("@soCMND", SqlDbType.Char).Value = txtCMND.Text.Trim();
                SqlDataReader dr_CheckCMND;
                dr_CheckCMND = cmd_CheckCMND.ExecuteReader();
                while (dr_CheckCMND.Read())
                {
                    dem_CMND += 1;
                }
                if (dem_CMND >= 1) MessageBox.Show("Số CMND đã được sử dụng");

                dr_CheckCMND.Close();
                #endregion

                #region Check trùng số điện thoại
                SqlCommand cmd_CheckSDT = new SqlCommand("KiemTra_SDT", sqlCon);
                cmd_CheckSDT.CommandType = CommandType.StoredProcedure;
                cmd_CheckSDT.Parameters.AddWithValue("@SDT", SqlDbType.Char).Value = txtSDT.Text;
                SqlDataReader dr_CheckSDT;
                dr_CheckSDT = cmd_CheckSDT.ExecuteReader();
                while (dr_CheckSDT.Read())
                {
                    dem_SDT += 1;
                }
                if (dem_SDT >= 1) MessageBox.Show("SĐT đã được sử dụng");

                dr_CheckSDT.Close();
                #endregion

                while (dem_CMND < 1 && dem_SDT < 1)
                {
                    #region check ô tên bố mẹ
                    // Check trẻ dưới 8 tuổi
                    if (Convert.ToInt32(txtTuoi.Text) < 8 && txtTenBM.Text == "Tên Bố Mẹ ( < 8 tuổi )")
                    {
                        MessageBox.Show("Trẻ dưới 8 tuổi hãy nhập tên bố mẹ");
                        break;
                    }

                    // nếu trên 8 tuổi 
                    if (txtTenBM.Text == "Tên Bố Mẹ ( < 8 tuổi )")
                    {
                        tenBM = "";
                    }
                    else tenBM = txtTenBM.Text;
                    #endregion

                    // Insert khách hàng vào CSDL
                    SqlCommand cmd_ThemKH = new SqlCommand("Them_KhachHang", sqlCon);
                    cmd_ThemKH.CommandType = CommandType.StoredProcedure;
                    cmd_ThemKH.Parameters.AddWithValue("@MaKH", SqlDbType.Char).Value = maKH.Trim();
                    cmd_ThemKH.Parameters.AddWithValue("@TenKhachHang", SqlDbType.NVarChar).Value = txtTenKH.Text;
                    cmd_ThemKH.Parameters.AddWithValue("@SoCMND", SqlDbType.Char).Value = txtCMND.Text.Trim();
                    cmd_ThemKH.Parameters.AddWithValue("@TenBoMe", SqlDbType.NVarChar).Value = tenBM;
                    cmd_ThemKH.Parameters.AddWithValue("@SDT", SqlDbType.Char).Value = txtSDT.Text;
                    cmd_ThemKH.Parameters.AddWithValue("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                    cmd_ThemKH.Parameters.AddWithValue("@Tuoi", SqlDbType.Int).Value = Convert.ToInt32(txtTuoi.Text);
                    cmd_ThemKH.ExecuteNonQuery();
                    FormInsertPT_LeTan fIPT_LT = new FormInsertPT_LeTan();


                    fIPT_LT.maKH = maKH; // Lưu lại mã KH để nhập tự động vào phiếu tiêm ( maKH ở formInsertPT để public nên có thể gọi đc ở đây )
                    fIPT_LT.ShowDialog();
                    this.Close();
                    break;
                }
                break;
            }
            sqlCon.Close();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(source);
            sqlCon.Open();

            SqlCommand cmd_KH = new SqlCommand("SELECT TOP(1)(CAST(RIGHT(MaKH, LEN(MAKH) - 2) AS INT)) FROM dbo.KhachHang ORDER BY(CAST(RIGHT(MaKH, LEN(MaKH) - 2) AS INT)) DESC", sqlCon);

            #region Tự động nhập mã khách hàng
            string CreateMaKH;
            int MaxMAKH;
            //try
            //{
            //    MaxMAKH = (int)(cmd_KH.ExecuteScalar()) + 1;
            //}
            //catch (Exception)
            //{
            //    MaxMAKH = 1;
            //}
            if(cmd_KH.ExecuteScalar() == null)
            {
                MaxMAKH = 1;
            }
            else
            {
                MaxMAKH = (int)(cmd_KH.ExecuteScalar()) + 1;
            }
            if (MaxMAKH < 10)
            {
                CreateMaKH = string.Concat("0", MaxMAKH.ToString());
            }
            else CreateMaKH = MaxMAKH.ToString();
            maKH = string.Concat("KH", CreateMaKH);
            txtMaKH.Text = maKH;
            #endregion
            sqlCon.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
