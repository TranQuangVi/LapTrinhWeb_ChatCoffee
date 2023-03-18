using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChatCoffee.Models
{
    public class ThongKeTruyCap
    {
        public static string strConnect = ConfigurationManager.ConnectionStrings["ChatCoffeeDBContext"].ToString();
    
        public static ThongKeViewModel ThongKe()
        {
            using(var connect=new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<ThongKeViewModel>("ThongKe", commandType: CommandType.StoredProcedure);
                return item;
            }    
        }
    
    
    }
}