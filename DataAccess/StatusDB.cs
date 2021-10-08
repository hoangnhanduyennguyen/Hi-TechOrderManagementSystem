using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Order_Management_System.Business;
using System.Data.SqlClient;

namespace Hi_Tech_Order_Management_System.DataAccess
{
    public static class StatusDB
    {
        public static List<Status> GetRecordList(string select)
        {
            List<Status> listStatus = new List<Status>();
            // Step 1: Connect the Database
            SqlConnection connDB = UtilityDB.ConnectDB();
            // Step 2: Perform Select all operation
            SqlCommand cmdSelectAll;
            SqlDataReader sqlReader;
            if (select == "Order")
            {
                cmdSelectAll = new SqlCommand("SELECT * FROM Statuses WHERE Id <> 5 AND Id <> 6", connDB);
                sqlReader = cmdSelectAll.ExecuteReader();
                Status status;
                while (sqlReader.Read())
                {
                    status = new Status();
                    status.Id = Convert.ToInt32(sqlReader["Id"]);
                    status.Description = sqlReader["Description"].ToString();
                    listStatus.Add(status);
                }
            }else if (select == "Customer" || select == "Book" || select == "UserAccount")
            {
                cmdSelectAll = new SqlCommand("SELECT * FROM Statuses WHERE Id = 5 OR Id = 6", connDB);
                sqlReader = cmdSelectAll.ExecuteReader();
                Status status;
                while (sqlReader.Read())
                {
                    status = new Status();
                    status.Id = Convert.ToInt32(sqlReader["Id"]);
                    status.Description = sqlReader["Description"].ToString();
                    listStatus.Add(status);
                }
            }
            
            // Step 3: Close the database 
            connDB.Close();
            return listStatus;
        }

    }
}
