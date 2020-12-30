using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Mot_Chut
{
    public partial class FormThongtinnhanvien : Form
    {
        private string mnv;
        private int flag;
        private int quyentruycap;
        string imgLocation = "";
        public FormThongtinnhanvien(string mnv, int flag, int q)
        {
            this.mnv = mnv;
            this.flag = flag;
            this.quyentruycap = q;
            InitializeComponent();
        }

        private void FormThongtinnhanvien_Load(object sender, EventArgs e)
        {
            string mode;
            if (this.quyentruycap == 0)
            {
                this.buttonSua.Visible = false;
                this.buttonLuu.Visible = false;
                this.buttonXoa.Visible = false;
                this.Buttonbrowser.Visible = false;
            }
            if (flag == 0)
            {
                this.buttonLuu.Visible = false;
                this.buttonSua.Visible = false;
                this.Buttonbrowser.Visible = false;
                this.buttonXoa.Visible = false;
            }
            textBoxHoTen.Enabled = false;
            textBoxQuequan.Enabled = false;
            textBoxSDT.Enabled = false;
            comboBoxChucvu.Enabled = false;
            comboBoxGioitinh.Enabled = false;
            maskedTextBoxNgaysinh.Enabled = false;
            maskedTextBoxNgayvaolam.Enabled = false;
            buttonLuu.Enabled = false;
            Buttonbrowser.Enabled = false;
            var r = new DatabaseNV().Select("SELECTNhanVien '" + mnv + "'");
            textBoxMaNV.Text = mnv;
            textBoxHoTen.Text = r["HoTen"].ToString();
            textBoxEmail.Text = r["Email"].ToString();
            textBoxSDT.Text = r["SDT"].ToString();
            textBoxQuequan.Text = r["QueQuan"].ToString();
            maskedTextBoxNgaysinh.Text = r["NgaySinh"].ToString(); //// Convert to Date có vấn đề
            maskedTextBoxNgayvaolam.Text = r["NgayVaoLam"].ToString();
            comboBoxGioitinh.Text = r["GioiTinh"].ToString();
            mode = r["TrangThai"].ToString().Trim();
            if (mode == "1")
            {
                this.buttonSua.Visible = false;
                this.buttonLuu.Visible = false;
                this.buttonXoa.Text = "KHÔI PHỤC";
                this.Buttonbrowser.Visible = false;
            }
            //comboBoxGioitinh.Text = "wwwwwwwwwwww";
            if (r["MaChucVu"].ToString().Contains("QLTT")) { comboBoxChucvu.Text = "Quản lý trung tâm"; }
            if (r["MaChucVu"].ToString().Contains("BSK")) { comboBoxChucvu.Text = "Bác sỹ khám"; }
            if (r["MaChucVu"].ToString().Contains("BST")) { comboBoxChucvu.Text = "Bác sỹ tiêm"; }
            if (r["MaChucVu"].ToString().Trim() == "LT") { comboBoxChucvu.Text = "Lễ tân"; }
            if (r["MaChucVu"].ToString().Contains("TK")) { comboBoxChucvu.Text = "Thủ kho"; }

            try
            {
                byte[] images = (byte[])r["Avatar"];
                if (images == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(images);
                    pictureBox1.Image = Image.FromStream(mst);
                }
            }
            catch (Exception)
            {
                pictureBox1.Image = null;
            }
        }
        public bool Check()
        {
            if (string.IsNullOrEmpty(comboBoxGioitinh.Text))
            {
                MessageBox.Show("Giới tính trống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBoxGioitinh.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxChucvu.Text))
            {
                MessageBox.Show("Chức vụ trống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBoxChucvu.Focus();
                return false;
            }

            return true;
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            textBoxHoTen.Enabled = true;
            textBoxQuequan.Enabled = true;
            textBoxSDT.Enabled = true;
            //comboBoxChucvu.Enabled = true;
            comboBoxGioitinh.Enabled = true;
            maskedTextBoxNgaysinh.Enabled = true;
            //maskedTextBoxNgayvaolam.Enabled = true;
            buttonLuu.Enabled = true;
            buttonSua.Enabled = false;
            Buttonbrowser.Enabled = true;
            textBoxEmail.Enabled = true;
        }

        private void buttonLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            string Name = textBoxHoTen.Text;
            string Gioitinh = comboBoxGioitinh.Text;
            DateTime ngaysinh;
            try
            {
                ngaysinh = DateTime.ParseExact(maskedTextBoxNgaysinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                MessageBox.Show("Ngày sinh không hợp lệ");
                maskedTextBoxNgaysinh.Select();
                return;
            }
            string Quequan = textBoxQuequan.Text;
            DateTime ngayvaolam = DateTime.ParseExact(maskedTextBoxNgayvaolam.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string ChucVu = "";
            if (comboBoxChucvu.Text == "Quản lý trung tâm") { ChucVu = "QLTT"; }
            if (comboBoxChucvu.Text == "Bác sỹ khám") { ChucVu = "BSK"; }
            if (comboBoxChucvu.Text == "Bác sỹ tiêm") { ChucVu = "BST"; }
            if (comboBoxChucvu.Text == "Lễ tân") { ChucVu = "LT"; }
            if (comboBoxChucvu.Text == "Thủ kho") { ChucVu = "TK"; }
            string sdt = textBoxSDT.Text;
            string email = textBoxEmail.Text;
            byte[] images = null;
            if (imgLocation != "")
            {
                FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brr = new BinaryReader(stream);
                images = brr.ReadBytes((int)stream.Length);
            }



            List<CustomerParameter> lstPara = new List<CustomerParameter>();
            if (images == null)
            {
                sql = "UpdateNhanVienNoImage";
            }
            else
            {
                sql = "UpdateNhanVien";
            }

            lstPara.Add(new CustomerParameter() { key = "@manhanvien", value = mnv });
            lstPara.Add(new CustomerParameter() { key = "@hoten", value = Name });
            lstPara.Add(new CustomerParameter() { key = "@email", value = email });
            lstPara.Add(new CustomerParameter() { key = "@ngaysinh", value = ngaysinh.ToString("yyyy-MM-dd") });
            lstPara.Add(new CustomerParameter() { key = "@quequan", value = Quequan });
            lstPara.Add(new CustomerParameter() { key = "@ngayvaolam", value = ngayvaolam.ToString("yyyy-MM-dd") });
            lstPara.Add(new CustomerParameter() { key = "@gioitinh", value = Gioitinh });
            lstPara.Add(new CustomerParameter() { key = "@sdt", value = sdt });
            lstPara.Add(new CustomerParameter() { key = "@machucvu", value = ChucVu });
            var rs = new DatabaseNV().Excute(sql, lstPara, images);
            if (rs > 0)
            {
                MessageBox.Show("Cập nhật thành công.");
                this.Dispose();

            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.");
                textBoxHoTen.Enabled = false;
                textBoxQuequan.Enabled = false;
                textBoxSDT.Enabled = false;
                comboBoxChucvu.Enabled = false;
                comboBoxGioitinh.Enabled = false;
                maskedTextBoxNgaysinh.Enabled = false;
                maskedTextBoxNgayvaolam.Enabled = false;
                buttonLuu.Enabled = false;
                Buttonbrowser.Enabled = false;

            }
            buttonSua.Enabled = true;
            buttonLuu.Enabled = false;


        }

        private void Buttonbrowser_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imgLocation = openFileDialog1.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;

            }
        }

        private void buttontExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            string sql = "";
            string manvxoa = textBoxMaNV.Text;
            List<CustomerParameter> lstPara = new List<CustomerParameter>();
            sql = "XoaNhanVien";
            lstPara.Add(new CustomerParameter() { key = "@manhanvien", value = manvxoa });
            var rs = new DatabaseNV().Excute(sql, lstPara, null);
            if (rs > 0)
            {
                if (buttonXoa.Text == "XÓA")
                {
                    MessageBox.Show("Xóa thành công.");
                }
                else
                {
                    MessageBox.Show("Khôi phục thành công.");
                }

                this.Dispose();

            }
            else
            {
                MessageBox.Show("Xóa thất bại.");




            }
        }
    }
}
