using ChatCoffee.Models;
using ChatCoffee.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatCoffee.Repository
{
    public class SearchByOption
    {
        // tạo đối tượng theo modeldefault
        private ApplicationDbContext db = new ApplicationDbContext();

        // tạo đối tượng theo  modelview
        public MyDataDataContext model = new MyDataDataContext();

        //lấy tất cả sản phẩm theo ID loại


        //lấy tất cả sản phẩm theo ID thương hiệu


        // cấu truy vấn lấy vis đụ theo id

        public void vidu(int id)
        {

        }
    }
}