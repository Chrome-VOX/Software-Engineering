using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Mot_Chut.DTO
{
     class Vacxin
     {
          private String maVacxin, tenVacxin, maLoai, xuatXu;
        private String giaThanh;

          public string MaVacxin { get => maVacxin; set => maVacxin = value; }
          public string TenVacxin { get => tenVacxin; set => tenVacxin = value; }
          public string MaLoai { get => maLoai; set => maLoai = value; }
          public string XuatXu { get => xuatXu; set => xuatXu = value; }
        public String GiaThanh { get => giaThanh; set => giaThanh = value; }

        public  Vacxin(String ma,String ten,String maloai,String xuat,String gt)
          {
               maVacxin = ma;
               tenVacxin = ten;
               maLoai = maloai;
               xuatXu = xuat;
            this.giaThanh = gt;
          }

          public Vacxin(DataRow row)
          {
               this.maVacxin = row["MaVacxin"].ToString();
               this.tenVacxin = row["TenVacxin"].ToString();
               this.maLoai = row["MaLoai"].ToString();
               this.xuatXu = row["XuatXu"].ToString();
            this.giaThanh = row["GiaThanh"].ToString();

          }
     }
}
