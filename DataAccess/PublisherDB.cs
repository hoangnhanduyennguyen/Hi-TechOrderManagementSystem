using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Hi_Tech_Order_Management_System.Business;

namespace Hi_Tech_Order_Management_System.DataAccess
{
    public static class PublisherDB
    {
        public static Publisher SearchRecord(int pubId)
        {
            SqlConnection connectDB = UtilityDB.ConnectDB();
            Publisher pub = new Publisher();
            SqlCommand cmdSearch = new SqlCommand("SELECT * FROM Publishers WHERE PublisherId = @PublisherId", connectDB);
            cmdSearch.Parameters.AddWithValue("@PublisherId", pubId);
            SqlDataReader sqlRead = cmdSearch.ExecuteReader();
            if (sqlRead.Read())
            {
                pub.PublisherId = Convert.ToInt32(sqlRead["PublisherId"]);
                pub.PublisherName = sqlRead["PublisherName"].ToString();
                pub.WebAddress = sqlRead["WebAddress"].ToString();
            }
            else
            {
                pub = null;
            }

            return pub;
        }
    }
}
