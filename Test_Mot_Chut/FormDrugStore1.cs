using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_Mot_Chut.DAO;
using Test_Mot_Chut.DTO;

namespace Test_Mot_Chut
{
    public partial class FormDrugStore1 : Form
    {
        BindingSource loaiVaxinList = new BindingSource();
        BindingSource vacxinList = new BindingSource();
        

        bool checkSuaNhap = true;
        bool checkSuaNhapLoai = true;
        public FormDrugStore1()
        {

            InitializeComponent();
            //if (FormLogIn.arrchucVu[0] == "TK")
            //{
            //    btnVietPhieu.Enabled = true;
            //}
            //else
            //{
            //    btnVietPhieu.Enabled = false;
            //}
        }

        #region Method
        public void getVacxin()
        {
            vacxinList.DataSource = VacxinDAO.Instance.GetVacxin();
        }

        public void addVacXinBinding()
        {
            txtMaVacxin.DataBindings.Add(new Binding("Text", dtgVacxin.DataSource, "MaVacxin", true, DataSourceUpdateMode.Never));
            txtTenVacxin.DataBindings.Add(new Binding("Text", dtgVacxin.DataSource, "TenVacxin", true, DataSourceUpdateMode.Never));
            txtXuatXu.DataBindings.Add(new Binding("Text", dtgVacxin.DataSource, "XuatXu", true, DataSourceUpdateMode.Never));
            txtGiaThanh.DataBindings.Add(new Binding("Text", dtgVacxin.DataSource, "GiaThanh", true, DataSourceUpdateMode.Never));

        }
        public void addLoaiVacxinDataBinding()
        {
            txtTenLoai.DataBindings.Add(new Binding("Text", dtgLoaiVacxin.DataSource, "TenLoai", true, DataSourceUpdateMode.Never));
            txtMaLoai.DataBindings.Add(new Binding("Text", dtgLoaiVacxin.DataSource, "MaLoai", true, DataSourceUpdateMode.Never));
        }
        void loadCbbLoaiVaxin(ComboBox cb)
        {
            List<LoaiVacxin> loai = LoaiVacxinDAO.Instance.getListLoaiVacxin();

            cb.DataSource = loai;
            //cb.DataSource = LoaiVacxinDAO.Instance.getDataLoaiVacxin();
            cb.DisplayMember = "TenLoai";
        }

        public void addLSPhieu()
        {
            String query = "SELECT * FROM dbo.PhieuNhap";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            dtgPhieuTiem.DataSource = dt;
        }

        private List<Vacxin> SearchVacxinByName(String name)
        {
            List<Vacxin> listVx = new List<Vacxin>();
            listVx = VacxinDAO.Instance.SearchVacxinByName(name);
            return listVx;
        }

        private List<Vacxin> SearchVacxinByID(String id)
        {
            List<Vacxin> listVx = new List<Vacxin>();
            listVx = VacxinDAO.Instance.SearchVacxinByID(id);
            return listVx;
        }
        private List<LoaiVacxin> SearchLoaiVacxinByName(String name)
        {
            List<LoaiVacxin> listVx = new List<LoaiVacxin>();
            listVx = LoaiVacxinDAO.Instance.SearchLoaiVacxinByName(name);
            return listVx;
        }

        private List<LoaiVacxin> SearchLoaiVacxinByID(String id)
        {
            List<LoaiVacxin> listVx = new List<LoaiVacxin>();
            listVx = LoaiVacxinDAO.Instance.SearchLoaiVacxinByID(id);
            return listVx;
        }
        public void getLoaiVacxin()
        {
            loaiVaxinList.DataSource = LoaiVacxinDAO.Instance.getDataLoaiVacxin();
        }
        #endregion

        #region Events
        private void btnNhap_Click(object sender, EventArgs e)
        {
            String ma = txtMaVacxin.Text;
            String name = txtTenVacxin.Text;
            String xuatxu = txtXuatXu.Text;
            String loai = (cbxLoaiVacxin.SelectedItem as LoaiVacxin).MaLoai;
            String giaThanh = txtGiaThanh.Text;

            if (ma.Equals(""))
            {
                MessageBox.Show("Nhập Mã Vacxin");
            }
            else if (name.Equals(""))
            {
                MessageBox.Show("Nhập Tên Vacxin");
            }
            else if (xuatxu.Equals(""))
            {
                MessageBox.Show("Nhập nơi xuất xứ");
            }
            else if (giaThanh.Equals(""))
            {
                MessageBox.Show("Nhập giá thành");
            }
            else
            {
                List<String> listMa = VacxinDAO.Instance.loadMaVacxin();
                bool check = true;

                for (int i = 0; i < listMa.Count; i++)
                {
                    if (ma.Equals(listMa[i]))
                    {
                        check = false;

                    }
                }
                if (check)
                {
                    try
                    {
                        if (VacxinDAO.Instance.insertVacxin(ma, name, xuatxu, loai, Int32.Parse(giaThanh)))
                        {
                            MessageBox.Show("Thêm thành công");
                            getVacxin();
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi khi thêm");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Tên phải Vacxin khác nhau!!!");
                    }

                }
                else
                {
                    MessageBox.Show("Mã Vacxin bị trùng. Mời Nhập lại!!!");
                }


            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormDrugStore_Load(object sender, EventArgs e)
        {
            if (FormLogIn.arrchucVu[0] != "TK")
            {
                this.btnVietPhieu.Visible = false;
                this.lbSua.Visible = false;
                this.btnNhap.Visible = false;
                this.btnSua.Visible = false;
                this.btnXoa.Visible = false;
                this.btnThemLoai.Visible = false;
                this.btnSuaLoai.Visible = false;
                this.lbSuaLoai.Visible = false;
                this.btnXoaPhieu.Visible = false;
                this.txtMaVacxin.Enabled = false;
                this.txtTenVacxin.Enabled = false;
                this.txtXuatXu.Enabled = false;
                this.txtGiaThanh.Enabled = false;
                this.cbxLoaiVacxin.Enabled = false;
                this.txtMaLoai.Enabled = false;
                this.txtTenLoai.Enabled = false;

            }
            String x = DateTime.Today.ToString("yyyy-MM-dd");
            String query1 = String.Format("EXEC dbo.getVXeHetHan @ngay = '{0}'", x);

            DataTable dt = DataProvider.Instance.ExcuteQuery(query1);
            if (dt.Rows.Count > 0 && (FormLogIn.arrchucVu[0] == "QLTT" || FormLogIn.arrchucVu[0] == "TK"))
            {
                FormHetHanVacxin f = new FormHetHanVacxin();
                f.ShowDialog();
            }
            //------------------
            dtgLoaiVacxin.DataSource = loaiVaxinList;
            dtgVacxin.DataSource = vacxinList;

            getVacxin();
            addVacXinBinding();
            loadCbbLoaiVaxin(cbxLoaiVacxin);
            getLoaiVacxin();
            addLoaiVacxinDataBinding();
            addLSPhieu();
            for (int i = 0; i < 6; i++)
            {
                dtgVacxin.Columns[i].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
                dtgVacxin.Columns[i].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                dtgVacxin.Columns[i].HeaderCell.Style.ForeColor = Color.DodgerBlue;
                dtgVacxin.Columns[i].DefaultCellStyle.ForeColor = Color.DodgerBlue;
                dtgVacxin.Columns[i].HeaderCell.Style.BackColor = Color.White;
                dtgVacxin.Columns[i].DefaultCellStyle.BackColor = Color.White;
            }

            dtgVacxin.EnableHeadersVisualStyles = false;



            dtgLoaiVacxin.Columns[0].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dtgLoaiVacxin.Columns[0].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dtgLoaiVacxin.Columns[0].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dtgLoaiVacxin.Columns[0].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dtgLoaiVacxin.Columns[0].HeaderCell.Style.BackColor = Color.White;
            dtgLoaiVacxin.Columns[0].DefaultCellStyle.BackColor = Color.White;

            dtgLoaiVacxin.Columns[1].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dtgLoaiVacxin.Columns[1].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dtgLoaiVacxin.Columns[1].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dtgLoaiVacxin.Columns[1].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dtgLoaiVacxin.Columns[1].HeaderCell.Style.BackColor = Color.White;
            dtgLoaiVacxin.Columns[1].DefaultCellStyle.BackColor = Color.White;

            dtgLoaiVacxin.EnableHeadersVisualStyles = false;

            dtgLoaiVacxin.Columns[0].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dtgLoaiVacxin.Columns[0].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dtgLoaiVacxin.Columns[0].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dtgLoaiVacxin.Columns[0].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dtgLoaiVacxin.Columns[0].HeaderCell.Style.BackColor = Color.White;
            dtgLoaiVacxin.Columns[0].DefaultCellStyle.BackColor = Color.White;

            dtgLoaiVacxin.Columns[1].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
            dtgLoaiVacxin.Columns[1].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dtgLoaiVacxin.Columns[1].HeaderCell.Style.ForeColor = Color.DodgerBlue;
            dtgLoaiVacxin.Columns[1].DefaultCellStyle.ForeColor = Color.DodgerBlue;
            dtgLoaiVacxin.Columns[1].HeaderCell.Style.BackColor = Color.White;
            dtgLoaiVacxin.Columns[1].DefaultCellStyle.BackColor = Color.White;

            dtgLoaiVacxin.EnableHeadersVisualStyles = false;


            for (int i = 0; i < 4; i++)
            {
                dtgPhieuTiem.Columns[i].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
                dtgPhieuTiem.Columns[i].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                dtgPhieuTiem.Columns[i].HeaderCell.Style.ForeColor = Color.DodgerBlue;
                dtgPhieuTiem.Columns[i].DefaultCellStyle.ForeColor = Color.DodgerBlue;
                dtgPhieuTiem.Columns[i].HeaderCell.Style.BackColor = Color.White;
                dtgPhieuTiem.Columns[i].DefaultCellStyle.BackColor = Color.White;
            }
            dtgPhieuTiem.EnableHeadersVisualStyles = false;


            int w = this.panel1.Width;
            int l = this.panel1.Height;
            int X = this.panel1.Location.X;
            int Y = this.panel1.Location.Y;
            this.dtgPhieuTiem.Height = l - 2 * (X - this.dtgPhieuTiem.Location.X);
            this.dtgPhieuTiem.Width = w - 2 * (Y - this.dtgPhieuTiem.Location.Y);


        }


        #endregion

        private void txtMaVacxin_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVietPhieu_Click(object sender, EventArgs e)
        {
            FormThemPhieuNhap f = new FormThemPhieuNhap();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            getVacxin();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String id = txtMaVacxin.Text;

            if (VacxinDAO.Instance.DeleteVacxin(id))
            {
                MessageBox.Show("Xóa Thành Công");
                getVacxin();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String ma = txtMaVacxin.Text;
            String name = txtTenVacxin.Text;
            String xuatxu = txtXuatXu.Text;
            String loai = (cbxLoaiVacxin.SelectedItem as LoaiVacxin).MaLoai;


            if (ma.Equals(""))
            {
                MessageBox.Show("Nhập Mã Vacxin");
            }
            else if (name.Equals(""))
            {
                MessageBox.Show("Nhập Tên Vacxin");
            }
            else if (xuatxu.Equals(""))
            {
                MessageBox.Show("Nhập nơi xuất xứ");
            }
            else
            {
                if (VacxinDAO.Instance.UpdateVacxin(ma, name, loai, xuatxu))
                {
                    MessageBox.Show("Sửa thành công");
                    getVacxin();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi Sửa");
                }

            }
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            lbSua.ForeColor = Color.Red;
        }

        private void lbSua_MouseLeave(object sender, EventArgs e)
        {
            lbSua.ForeColor = System.Drawing.SystemColors.Highlight;
        }

        private void lbSua_Click(object sender, EventArgs e)
        {
            if (checkSuaNhap)
            {
                lbSua.Text = "Bạn muốn nhập?";
                btnSua.Enabled = true;
                txtMaVacxin.Enabled = false;
                checkSuaNhap = false;
            }
            else
            {
                lbSua.Text = "Bạn muốn sửa Vacxin?";
                btnSua.Enabled = false;
                txtMaVacxin.Enabled = true;
                checkSuaNhap = true;
            }


        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<Vacxin> list = SearchVacxinByName(txtSearch.Text);
            if (list.Count == 0)
            {
                MessageBox.Show("Không tìm thấy Vacxin");
            }
            else
            {
                vacxinList.DataSource = list;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String maLoai = txtMaLoai.Text;
            String tenLoai = txtTenLoai.Text;

            if (maLoai.Equals(""))
            {
                MessageBox.Show("Nhập mã Loại");
            }
            else if (tenLoai.Equals(""))
            {
                MessageBox.Show("Nhập Tên Loại");
            }
            else
            {
                List<String> listMa = LoaiVacxinDAO.Instance.getListMaLoaiVacxin();
                bool check = true;

                for (int i = 0; i < listMa.Count; i++)
                {
                    if (maLoai.Equals(listMa[i]))
                    {
                        check = false;

                    }
                }
                if (check)
                {
                    if (LoaiVacxinDAO.Instance.InsertLoaiVacxin(tenLoai, maLoai))
                    {
                        MessageBox.Show("Thêm thành công");
                        getLoaiVacxin();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi thêm");
                    }
                }
                else
                {
                    MessageBox.Show("Mã loại bị trùng. Mời bạn nhập lại mã Loại mới");
                }

            }


        }

        private void lbSuaLoai_Click(object sender, EventArgs e)
        {
            if (checkSuaNhapLoai)
            {
                lbSuaLoai.Text = "Bạn muốn nhập Loại Vacxin?";
                btnSuaLoai.Enabled = true;
                txtMaLoai.Enabled = false;
                checkSuaNhapLoai = false;
            }
            else
            {
                lbSuaLoai.Text = "Bạn muốn sửa Loại Vacxin?";
                btnSuaLoai.Enabled = false;
                txtMaLoai.Enabled = true;
                checkSuaNhapLoai = true;
            }
        }

        private void lbSuaLoai_MouseHover(object sender, EventArgs e)
        {
            lbSuaLoai.ForeColor = Color.Red;
        }

        private void lbSuaLoai_MouseLeave(object sender, EventArgs e)
        {
            lbSuaLoai.ForeColor = System.Drawing.SystemColors.Highlight;
        }

        private void btnSuaLoai_Click(object sender, EventArgs e)
        {
            String ma = txtMaLoai.Text;
            String name = txtTenLoai.Text;

            if (name.Equals(""))
            {
                MessageBox.Show("Nhập Tên loại mới");
            }
            else
            {
                if (LoaiVacxinDAO.Instance.UpdateLoaiVacxin(name, ma))
                {
                    MessageBox.Show("Sửa thành công");
                    getLoaiVacxin();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi Sửa");
                }
            }
        }

        private void btnXemLoai_Click(object sender, EventArgs e)
        {
            getLoaiVacxin();
        }

        private void btnSearchLoai_Click(object sender, EventArgs e)
        {
            int index = cbbLoai.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Chọn Cách tìm kiếm!!!");
            }
            else
            {
                if (index == 0)
                {
                    List<LoaiVacxin> list = SearchLoaiVacxinByName(txtSearchLoai.Text);
                    if (list.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy Loại Vacxin");
                    }
                    else
                    {
                        loaiVaxinList.DataSource = list;
                    }
                }
                else
                {
                    List<LoaiVacxin> list = SearchLoaiVacxinByID(txtSearchLoai.Text);
                    if (list.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy Loại Vacxin");
                    }
                    else
                    {
                        loaiVaxinList.DataSource = list;
                    }
                }

            }


        }
        String value = "";
        private void dtgPhieuTiem_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                value = dtgPhieuTiem.Rows[e.RowIndex].Cells[0].Value.ToString();

                String query = String.Format("EXEC dbo.ChonPhieuTheoMaPhieu @maPhieu = '{0}'", value);

                dtgPT_CT.DataSource = DataProvider.Instance.ExcuteQuery(query);

                for (int i = 0; i < 7; i++)
                {
                    dtgPT_CT.Columns[i].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
                    dtgPT_CT.Columns[i].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                    dtgPT_CT.Columns[i].HeaderCell.Style.ForeColor = Color.DodgerBlue;
                    dtgPT_CT.Columns[i].DefaultCellStyle.ForeColor = Color.DodgerBlue;
                    dtgPT_CT.Columns[i].HeaderCell.Style.BackColor = Color.White;
                    dtgPT_CT.Columns[i].DefaultCellStyle.BackColor = Color.White;
                }
                dtgPT_CT.EnableHeadersVisualStyles = false;
            }
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            addLSPhieu();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            String id = txtMaLoai.Text;
            if (LoaiVacxinDAO.Instance.DeleteLoaiVacxin(id))
            {
                MessageBox.Show("Xóa loại Vacxin thành công!!");
                getLoaiVacxin();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index == 0)
            {
                dtNgayNhap_PN.Visible = true;
                txtMa_NV.Visible = false;
            }
            else
            {
                txtMa_NV.Visible = true;
                dtNgayNhap_PN.Visible = false;
            }
        }
        private void btnXoaPhieu_Click(object sender, EventArgs e)
        {
            if (value.Equals(""))
            {
                MessageBox.Show("Chọn phiếu tiêm cần xóa");
            }
            else
            {
                try
                {
                    DataProvider.Instance.ExcuteNoneQuery(String.Format("EXEC dbo.XoaPhieuNhapCTTheoMa @maPhieu = '{0}'", value));
                    DataProvider.Instance.ExcuteNoneQuery(String.Format("EXEC dbo.XoaPhieuTiemCTTheoMa @maPhieu = '{0}'", value));
                    MessageBox.Show("Xóa thành công");
                    addLSPhieu();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa Không thành công");
                    throw ex;
                }


            }


        }

        private void btnTK_PN_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            String query = "";
            if (dtNgayNhap_PN.Visible && index == 0)
            {
                String date = dtNgayNhap_PN.Value.ToString("yyyy-MM-dd");
                query = String.Format("EXEC dbo.chonPhieuNhapTheoNgayNhap @ngay = '{0}'", date);
                DataTable data = DataProvider.Instance.ExcuteQuery(query);
                dtgPhieuTiem.DataSource = data;

            }
            else if (txtMa_NV.Visible)
            {
                if (index == 1)
                {
                    String maNV = txtMa_NV.Text;
                    query = String.Format("EXEC dbo.chonPhieuNhapTheoMaNV @maNV = '{0}", maNV);
                }
                else if (index == 2)
                {
                    String maPhieu = txtMa_NV.Text;
                    query = String.Format("EXEC dbo.chonPhieuNhapTheoMaPhieu @maPhieu = '{0}'", maPhieu);
                }
                DataTable data = DataProvider.Instance.ExcuteQuery(query);
                dtgPhieuTiem.DataSource = data;
            }

        }

        private void btnXem_PN_Click(object sender, EventArgs e)
        {
            addLSPhieu();
        }

        private void cbbLoai_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtSearchLoai.Enabled = true;
        }

        private void dtgLoaiVacxin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                String id = dtgLoaiVacxin.Rows[e.RowIndex].Cells[0].Value.ToString();
                DataTable data = DataProvider.Instance.ExcuteQuery(String.Format("EXEC dbo.chonVacxinTheoMaLoai @maLoai = '{0}'", id));
                dtgVacxin_Ma.DataSource = data;
                for (int i = 0; i < 6; i++)
                {
                    dtgVacxin_Ma.Columns[i].HeaderCell.Style.Font = new Font("Arial", 12, FontStyle.Bold);
                    dtgVacxin_Ma.Columns[i].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                    dtgVacxin_Ma.Columns[i].HeaderCell.Style.ForeColor = Color.DodgerBlue;
                    dtgVacxin_Ma.Columns[i].DefaultCellStyle.ForeColor = Color.DodgerBlue;
                    dtgVacxin_Ma.Columns[i].HeaderCell.Style.BackColor = Color.White;
                    dtgVacxin_Ma.Columns[i].DefaultCellStyle.BackColor = Color.White;
                }
                dtgVacxin_Ma.EnableHeadersVisualStyles = false;
            }

        }

        private void cbbtypeTK_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtSearch.Enabled = true;
        }

        private void btnVX_TK_Click(object sender, EventArgs e)
        {
            int index = cbbtypeTK.SelectedIndex;
            if (index == 0)
            {
                List<Vacxin> list = SearchVacxinByID(txtSearch.Text);
                if (list.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy Vacxin");
                }
                else
                {
                    vacxinList.DataSource = list;
                }
            }
            else if (index == 1)
            {
                List<Vacxin> list = SearchVacxinByName(txtSearch.Text);
                if (list.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy Vacxin");
                }
                else
                {
                    vacxinList.DataSource = list;
                }
            }
            else
            {
                String query = String.Format("dbo.timVacxinTheoMaLoai @maLoai = '%{0}%'", txtSearch.Text);
                DataTable data = DataProvider.Instance.ExcuteQuery(query);
                vacxinList.DataSource = data;

            }
        }

        private void metroTabControl1_Selected(object sender, TabControlEventArgs e)
        {
            addLSPhieu();
        }
    }
}
