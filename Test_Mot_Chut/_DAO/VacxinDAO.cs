using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Mot_Chut.DTO;

namespace Test_Mot_Chut.DAO
{
    class VacxinDAO
    {
        public static VacxinDAO instance;
        public static VacxinDAO Instance
        {
            get { if (instance == null) instance = new VacxinDAO(); return VacxinDAO.instance; }
            private set { VacxinDAO.instance = value; }
        }

        private VacxinDAO() { }


        public List<Vacxin> LoadVacxinList()
        {
            List<Vacxin> list = new List<Vacxin>();
            DataTable data = DataProvider.Instance.ExcuteQuery("EXEC dbo.getVacxin");

            foreach (DataRow item in data.Rows)
            {
                Vacxin vx = new Vacxin(item);
                list.Add(vx);
            }
            return list;

        }

        public List<String> loadMaVacxin()
        {
            List<String> list = new List<String>();
            DataTable data = DataProvider.Instance.ExcuteQuery("EXEC dbo.getVacxin");

            foreach (DataRow item in data.Rows)
            {
                Vacxin vx = new Vacxin(item);
                list.Add(vx.MaVacxin);
            }
            return list;

        }

        public void DeleteVacxinByLoai(String id)
        {
            DataProvider.Instance.ExcuteQuery(String.Format("EXEC dbo.deletePNCTTheoMaLoai @maLoai = '{0}'", id));
            DataProvider.Instance.ExcuteQuery(String.Format("EXEC dbo.deletePTCTTheoMaLoai @maLoai = '{0}')", id));
            DataProvider.Instance.ExcuteQuery(String.Format("EXEC dbo.deleteVacxinTheoMaLoai @maLoai = '{0}'", id));
        }

        public DataTable GetVacxin()
        {
            return DataProvider.Instance.ExcuteQuery("select * from Vacxin");
        }

        public bool insertVacxin(String ma, String ten, String xuatxu, String loai, int gia)
        {
            String query = String.Format("EXEC dbo.insertVacxin @maVx = '{0}',@tenVx = '{1}',@maLoai = '{2}',@xuatxu = N'{3}',@giaThanh = {4}", ma, ten, loai, xuatxu, gia);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }

        public bool DeleteVacxin(string id)
        {
            String query3 = String.Format("EXEC dbo.deleteKho @ma = '{0}'", id);
            DataProvider.Instance.ExcuteNoneQuery(query3);

            String query = String.Format("EXEC dbo.deletePNCTTheoMaVX @maVx = '{0}'", id);
            DataProvider.Instance.ExcuteNoneQuery(query);

            String query1 = String.Format("EXEC dbo.deletePTCTTheoMaVX @maVx = '{0}'", id);
            DataProvider.Instance.ExcuteNoneQuery(query1);

            String query2 = String.Format("EXEC dbo.deleteVacxintheoMa @maVx = '{0}'", id);

            int result = DataProvider.Instance.ExcuteNoneQuery(query2);

            return result > 0;
        }

        public bool UpdateVacxin(string ma, string name, string loai, string xuatxu)
        {
            String query = String.Format("EXEC dbo.updateVXtheoMaVacxin @tenVX = '{0}',@maLoai = '{1}',@xxu = N'{2}',@maVx = '{3}'", name, loai, xuatxu, ma);


            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
            // return query;
        }

        public List<Vacxin> SearchVacxinByName(string name)
        {
            List<Vacxin> list = new List<Vacxin>();
            String query = string.Format("EXEC timVXTheoTen @tenVx ='{0}'", "%"+name+"%");
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Vacxin vx = new Vacxin(item);
                list.Add(vx);
            }
            return list;

        }

        public List<Vacxin> SearchVacxinByID(string id)
        {
            List<Vacxin> list = new List<Vacxin>();
            String query = string.Format("EXEC timVacxinTheoMaVx @maVx='{0}'", "%"+id+"%");
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Vacxin vx = new Vacxin(item);
                list.Add(vx);
            }
            return list;

        }

        public List<Vacxin> getVacxinByLoai(string name)
        {
            List<Vacxin> list = new List<Vacxin>();
            String query = string.Format(" EXEC layVacxinTheoMaLoai @maLoai='{0}'", name);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Vacxin vx = new Vacxin(item);
                list.Add(vx);
            }
            return list;

        }

    }
}
