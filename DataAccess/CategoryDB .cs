using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Hi_Tech_Order_Management_System.Business;

namespace Hi_Tech_Order_Management_System.DataAccess
{
    public static class CategoryDB
    {
        public static Category SearchRecord(int catId)
        {
            SqlConnection connectDB = UtilityDB.ConnectDB();
            Category cate = new Category();
            SqlCommand cmdSearch = new SqlCommand("SELECT * FROM Categories WHERE CategoryId = @CategoryId", connectDB);
            cmdSearch.Parameters.AddWithValue("@CategoryId", catId);
            SqlDataReader sqlRead = cmdSearch.ExecuteReader();
            if (sqlRead.Read())
            {
                cate.CategoryId = Convert.ToInt32(sqlRead["CategoryId"]);
                cate.CategoryName = sqlRead["CategoryName"].ToString();
            }
            else
            {
                cate = null;
            }

            return cate;
        }
    }
}
