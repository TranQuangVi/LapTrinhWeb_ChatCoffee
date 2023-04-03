using ChatCoffee.Models;
using ChatCoffee.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COFFEE = ChatCoffee.Models.ModelsDefault.COFFEE;

namespace ChatCoffee.Repository
{
    public class SearchByOption
    {
        // tạo đối tượng theo modeldefault
        private ApplicationDbContext db = new ApplicationDbContext();

        // tạo đối tượng theo  modelview
        public MyDataDataContext model = new MyDataDataContext();

        public List<COFFEE> LocGiaGiam()
        {
            var list = db.COFFEEs.Where(c => c.GIA <= int.MaxValue).OrderByDescending(c => c.GIA);
            return list.ToList();
        }
        public List<COFFEE> LocGiaTang()
        {
            var list = db.COFFEEs.Where(c => c.GIA >= int.MinValue).OrderBy(c => c.GIA);
            return list.ToList();
        }
        public List<COFFEE> TuAZ()
        {
            var list = db.COFFEEs.OrderBy(c => c.TENCF);
            return list.ToList();
        }
        public List<COFFEE> TuZA()
        {
            var list = db.COFFEEs.OrderByDescending(c => c.TENCF);
            return list.ToList();
        }
        public List<COFFEE> locgia1()
        {
            var list = db.COFFEEs.Where(c => c.GIA <= 500000).OrderBy(c => c.GIA);
            return list.ToList();
        }
        public List<COFFEE> locgia2()
        {
            var list = db.COFFEEs.Where(c => c.GIA >= 500000 && c.GIA <= 1000000).OrderBy(c => c.GIA);
            return list.ToList();
        }
        public List<COFFEE> locgia3()
        {
            var list = db.COFFEEs.Where(c => c.GIA >= 1000000 && c.GIA <= 2000000).OrderBy(c => c.GIA);
            return list.ToList();
        }
        public static int getSLinCard(int magh)
        {
            MyDataDataContext model = new MyDataDataContext();
            GIOHANG gh = model.GIOHANGs.Where(g => g.MAGH == magh).FirstOrDefault();
            return (int)gh.TONGSP;
        }


        // tìm theo thương hiệu
        // tìm theo loại
    }
}