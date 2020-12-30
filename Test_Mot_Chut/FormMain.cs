using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_Mot_Chut.DAO;

namespace Test_Mot_Chut
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wsmg, int wparam, int lparam);

        private void openFormInPanel(object FormChild)
        {
            // neu panelContain dang chua cai gi thi xoa di de hien thi form moi len
            if (pnlContain.Controls.Count > 0)
            {
                pnlContain.Controls.RemoveAt(0);
            }
            Form fc = FormChild as Form;
            fc.TopLevel = false;
            fc.Dock = DockStyle.Fill;
            fc.TopMost = true;
            pnlContain.Controls.Add(fc);
            pnlContain.Tag = fc;
            fc.Show();
        }

       

        private void btnKhoThuoc_Click_1(object sender, EventArgs e)
        {
            openFormInPanel(new FormDrugStore());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            if(FormLogIn.arrchucVu[0] != "QLTT")
            {
                FormNhanVien f = new FormNhanVien(0);
                f.ShowDialog();
            }
            else
            {
                FormNhanVien f = new FormNhanVien(1);
                f.ShowDialog();
            }
        }

        private void btnPhieuKham_Click(object sender, EventArgs e)
        {
            FormCustomer fc = new FormCustomer();
            fc.ShowDialog();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            if (FormLogIn.arrchucVu[0] == "BSK")
            {
                openFormInPanel(new FormListKH_BSK());
            }

            if (FormLogIn.arrchucVu[0] == "BST")
            {
                openFormInPanel(new FormListKH_BST());
            }
        }

        private void btnSlide_Click(object sender, EventArgs e)
        {
            if (pnlMenu.Width == 170)
            {
                pnlMenu.Width = 58;
                btnSlide.Location = new Point(60, 1);
                lbVaccine.Visible = false;

                //pnlControl.Width = 819;
            }
            else
            {
                pnlMenu.Width = 170;
                btnSlide.Location = new Point(170, 0);
                lbVaccine.Visible = true;
                //pnlControl.Width = 661;
            }
        }

        private void iconHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            buttonCNCSDL.Visible = false;
          
            if (FormLogIn.arrchucVu[0] == "LT")
            {
                btnKhachHang.Enabled = true;
                btnLichHen.Enabled = false;
                btnKhoThuoc.Enabled = false;
                btnNhanVien.Enabled = true;
                btnPhieuKham.Enabled = true;

            }
            if (FormLogIn.arrchucVu[0] == "BSK")
            {
                btnPhieuKham.Enabled = false;
                btnLichHen.Enabled = true;
                btnNhanVien.Enabled = true;
                btnKhoThuoc.Enabled = true;
                btnKhachHang.Enabled = true;

            }
            if (FormLogIn.arrchucVu[0] == "BST")
            {
                btnPhieuKham.Enabled = false;
                btnLichHen.Enabled = true;
                btnNhanVien.Enabled = true;
                btnKhoThuoc.Enabled = true;
                btnKhachHang.Enabled = true;
            }
            if (FormLogIn.arrchucVu[0] == "TK")
            {
                btnKhachHang.Enabled = true;
                btnLichHen.Enabled = false;
                btnKhoThuoc.Enabled = true;
                btnNhanVien.Enabled = true;
                btnPhieuKham.Enabled = false;

            }
            if (FormLogIn.arrchucVu[0] == "QLTT")
            {
                btnKhachHang.Enabled = true;
                btnLichHen.Enabled = true;
                btnKhoThuoc.Enabled = true;
                btnNhanVien.Enabled = true;
                btnPhieuKham.Enabled = false;
                buttonCNCSDL.Visible = true;

            }


        }

        private void btnLichHen_Click(object sender, EventArgs e)
        {
            openFormInPanel(new FormLichHen());
        }

        private void buttonCNCSDL_Click(object sender, EventArgs e)
        {
            string sql = "";
            sql = "CapNhatCSDL";
            List<CustomerParameter> lst = new List<CustomerParameter>();
            var rs = new DatabaseNV().Excute(sql, lst, null);
            if (rs >= 0)
            {

                MessageBox.Show("Cơ sở dữ liệu cập nhật thành công");

            }
            else
            {
                MessageBox.Show("Cập nhật thất bại");




            }
        }
    }
}
