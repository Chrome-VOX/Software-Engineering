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
        private Form activeForm = null;
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
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContain.Controls.Add(childForm);
            pnlContain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }



        private void btnKhoThuoc_Click_1(object sender, EventArgs e)
        {
            openFormInPanel(new FormDrugStore1());
            /*FormDrugStore f = new FormDrugStore();
            //f.Dock = System.Windows.Forms.DockStyle.Fill;
            f.TopLevel = false;
            pnlContain.Controls.Add(f);
            openChildForm(f);*/
            
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            if(FormLogIn.arrchucVu[0] != "QLTT")
            {
                FormNhanVien f = new FormNhanVien(0);
                //f.TopLevel = false;
                //pnlContain.Controls.Add(f);
                //openChildForm(f);
                openFormInPanel(f);
            }
            else
            {
                FormNhanVien f = new FormNhanVien(1);
                //f.TopLevel = false;
                //pnlContain.Controls.Add(f);
                //openChildForm(f);
                openFormInPanel(f);
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
            if (pnlMenu.Width == 202)
            {
                pnlMenu.Width = 64;
                btnSlide.Location = new Point(66, 1);
                lbVaccine.Visible = false;
                this.btnKhoThuoc.Text = "";
                this.btnNhanVien.Text = "";
                this.btnPhieuKham.Text = "";
                this.btnKhachHang.Text = "";
                this.btnLichHen.Text = "";
                this.buttonCNCSDL.Text = "";

                //pnlControl.Width = 819;
            }
            else
            {
                
                    pnlMenu.Width = 202;
                    btnSlide.Location = new Point(203, 0);
                    lbVaccine.Visible = true;
                    this.btnKhoThuoc.Text = "   Kho Thuốc";
                    this.btnNhanVien.Text = "   Nhân Viên";
                    this.btnPhieuKham.Text = "   Phiếu Khám";
                    this.btnKhachHang.Text = "   Khách Hàng";
                    this.btnLichHen.Text = "   Lịch Hẹn";
                    this.buttonCNCSDL.Text = "   Cập nhật";

                //pnlControl.Width = 661;
            }
            /*if (this.btnKhoThuoc.Text == "Kho thuốc")
            {
                pnlMenu.Width = 58;
                btnSlide.Location = new Point(60, 1);
                lbVaccine.Visible = false;
                this.btnKhoThuoc.Text = "";
                this.btnNhanVien.Text = "";
                this.btnPhieuKham.Text = "";
                this.btnKhachHang.Text = "";
                this.btnLichHen.Text = "";
                this.buttonCNCSDL.Text = "";

            }
            else
            {
                pnlMenu.Width = 269;
                btnSlide.Location = new Point(277, 0);
                lbVaccine.Visible = true;
                this.btnKhoThuoc.Text = "Kho Thuốc";
                this.btnNhanVien.Text = "Nhân Viên";
                this.btnPhieuKham.Text = "Phiếu Khám";
                this.btnKhachHang.Text = "Khách Hàng";
                this.btnLichHen.Text = "Lịch Hẹn";
                this.buttonCNCSDL.Text = "Cập nhật";
            }*/
            

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
            //this.textBox1.Text = this.pnlMenu.Width.ToString();
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
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
