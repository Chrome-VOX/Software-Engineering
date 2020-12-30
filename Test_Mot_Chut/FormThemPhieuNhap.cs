using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_Mot_Chut._DTO;
using Test_Mot_Chut.DAO;
using Test_Mot_Chut.DTO;

namespace Test_Mot_Chut
{
    public partial class FormThemPhieuNhap : Form
    {
        List<PhieuCTMin> listPhieu = new List<PhieuCTMin>();
        int index = 0;
        int tongTien = 0;
        String maPhieuNhap;
        public FormThemPhieuNhap()
        {
            InitializeComponent();
            txtMaNVNhap.Text = FormLogIn.maNV;
        }
        void loadCbbLoaiVaxin(ComboBox cb)
        {
            List<LoaiVacxin> loai = LoaiVacxinDAO.Instance.getListLoaiVacxin();

            cb.DataSource = loai;
            //cb.DataSource = LoaiVacxinDAO.Instance.getDataLoaiVacxin();
            cb.DisplayMember = "MaLoai";
        }

        void loadCbbVaxin(ComboBox cb)
        {
            List<Vacxin> loai = VacxinDAO.Instance.LoadVacxinList();

            cb.DataSource = loai;
            //cb.DataSource = LoaiVacxinDAO.Instance.getDataLoaiVacxin();
            cb.DisplayMember = "MaVacxin";
        }

        private void FormThemPhieuNhap_Load(object sender, EventArgs e)
        {
          
            loadCbbLoaiVaxin(cbxLoaiVx);
            //loadCbbVaxin(cbxMaVx);
            cbxMaVx.DataSource = null;
            cbxMaVx.Enabled = false;

            listView1.Columns.Add("Mã Vacxin", 200);
            listView1.Columns.Add("Số Lượng", 200);
            listView1.Columns.Add("Đơn giá", 200);

            String queryTaoMa = String.Format("SELECT TOP(1) (CAST(RIGHT(MaPhieuNhap, LEN(MaPhieuNhap) - 1) AS INT)) FROM dbo.PhieuNhap ORDER BY CAST(RIGHT(MaPhieuNhap, LEN(MaPhieuNhap) - 1) AS INT)	DESC");

            
            String CreateMaPhieuNhap;
            int MAxMAPhieuNhap;
            //try
            //{
            //    MAxMAPhieuNhap = (int)DataProvider.Instance.ExcuteScalar(queryTaoMa) + 1;
            //}
            //catch (Exception)
            //{
            //    MAxMAPhieuNhap = 1;
            //}
            if(DataProvider.Instance.ExcuteScalar(queryTaoMa) == null)
            {
                MAxMAPhieuNhap = 1;
            }
            else
            {
                MAxMAPhieuNhap = (int)DataProvider.Instance.ExcuteScalar(queryTaoMa) + 1;
            }
            CreateMaPhieuNhap = string.Concat("P", MAxMAPhieuNhap.ToString());
            this.txtMaPhieuNhap.Text = CreateMaPhieuNhap;
            txtMaPhieuNhap.Enabled = false;
            txtMaNVNhap.Enabled = false;
          
        }

        private void cbxLoaiVx_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCbbVaxin(cbxMaVx);
            String maLoai = (cbxLoaiVx.SelectedItem as LoaiVacxin).MaLoai;
            List<Vacxin> list = VacxinDAO.Instance.getVacxinByLoai(maLoai);
            if (list.Count == 0)
            {
                MessageBox.Show("Mã Loại này chưa có Vacxin!!!");
                cbxMaVx.Enabled = false;
                cbxMaVx.DataSource = null;
            }
            else
            {
                cbxMaVx.Enabled = true;                
                cbxMaVx.DataSource = list;

            }

            
        }

 
        private void btnThem_Click(object sender, EventArgs e)
        {
            
            maPhieuNhap = txtMaPhieuNhap.Text;
            String maNVNhap = txtMaNVNhap.Text;
            String ngayNhap = dtNgayNhap.Value.ToString("MM/dd/yyyy");
            String HSD = dtHSD.Value.ToString("MM/dd/yyyy");
            String maVx;
            try
            {
                 maVx = (cbxMaVx.SelectedItem as Vacxin).MaVacxin;
            }catch(Exception ex)
            {
                maVx = null; 
            }
            
            String donGia = txtDonGia.Text;
            String soLuong = txtSoLuong.Text;

            if (maPhieuNhap.Equals(""))
            {
                MessageBox.Show("Nhập mã phiếu Nhập");
            }
            else if(maVx==null)
            {
                MessageBox.Show("Chọn mã Vacxin");
            }
            else if(soLuong.Equals(""))
            {
                MessageBox.Show("Nhập số lượng");
            }else if (donGia.Equals(""))
            {
                MessageBox.Show("Nhập đơn giá");
            }
            else
            {               
                //TODO: Check cùng mã Phiếu

                String query_checkMa = String.Format("EXEC dbo.chonPNtheoMaPhieu_NotTK @maPhieu = '{0}'", maPhieuNhap);
                DataTable data =DataProvider.Instance.ExcuteQuery(query_checkMa);
                if (data.Rows.Count > 0&&index==0)
                {
                    MessageBox.Show("Mã phiếu nhập bị trùng. Mời Nhập lại!!!");
                }
                else
                {
                    int sl = Int32.Parse(txtSoLuong.Text);
                    int dg = Int32.Parse(txtDonGia.Text);

                    int thanhTien = sl * dg;
                    tongTien += thanhTien;
                    lbThanhTien.Text = (thanhTien).ToString() + " VNĐ";
                    lbTongTien.Text = tongTien.ToString() + " VNĐ";
                    string[] arr = new string[4];

                    //Thêm vào Database
                    bool check1 = true, check2 = true;
                    if (index == 0)
                    {
                        check1 = true;
                        String query = String.Format("EXEC dbo.insertIntoPhieuNhap @maPhieu = '{0}',@maNVNhap = '{1}',@ngayNhap = '{2}'", maPhieuNhap, maNVNhap, ngayNhap);
                        String query1 = String.Format("EXEC dbo.insertIntoPNCT @maPhieu = '{0}',@maVX = '{1}',@hsd = '{2}',@dg = {3},@tt = {4},@sl = {5}", maPhieuNhap, maVx, HSD, donGia, thanhTien.ToString(), soLuong);
                        String query2 = String.Format("EXEC dbo.insertKho @ma = '{0}', @sl = {1},@hsd = '{2}'", maVx, soLuong, HSD);

                        if (DataProvider.Instance.ExcuteNoneQuery(query) > 0 && DataProvider.Instance.ExcuteNoneQuery(query1) > 0&&DataProvider.Instance.ExcuteNoneQuery(query2)>0)
                        {
                            check1 = true;
                        }
                        else check1 = false;
                        index++;
                    }
                    else
                    {
                        String query1 = String.Format("EXEC dbo.insertIntoPNCT @maPhieu = '{0}',@maVX = '{1}',@hsd = '{2}',@dg = {3},@tt = {4},@sl = {5}", maPhieuNhap, maVx, HSD, donGia, thanhTien.ToString(), soLuong);
                        String query2 = String.Format("EXEC dbo.insertKho @ma = '{0}', @sl = {1},@hsd = '{2}'", maVx, soLuong, HSD);
                        check2 = true;
                        if (DataProvider.Instance.ExcuteNoneQuery(query1) > 0&& DataProvider.Instance.ExcuteNoneQuery(query2) > 0)
                        {
                            check2 = true;
                        }
                        else check2 = false;
                    }

                    if (check1 || check2)
                    {
                        MessageBox.Show("Thêm thành công");
                        txtDonGia.Text = "";
                        txtSoLuong.Text = "";
                        arr[0] = maVx;
                        arr[1] = soLuong;
                        arr[2] = thanhTien.ToString();
                        ListViewItem itm = new ListViewItem(arr);
                        listView1.Items.Add(itm);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String query = String.Format("EXEC dbo.updateTongTienTheoMaPhieuNhap @tt = {0},@maPhieuNhap = '{1}'", tongTien.ToString(),maPhieuNhap);
            DataProvider.Instance.ExcuteNoneQuery(query);
            index = 0;
            tongTien = 0;

            this.Close();
            

        }

        private void cbxMaVx_SelectionChangeCommitted(object sender, EventArgs e)
        {
            String maVx = (cbxMaVx.SelectedItem as Vacxin).MaVacxin;
            String query = String.Format("EXEC dbo.chonVacxinTheoMaVX @maVX = '{0}'", maVx);
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            Vacxin vx = new Vacxin(dt.Rows[0]);


            txtDonGia.Text = (Int32.Parse(vx.GiaThanh) * 1.1).ToString();

        }
    }
}
