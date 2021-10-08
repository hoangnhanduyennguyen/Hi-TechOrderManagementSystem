using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Order_Management_System.Business;
using System.Data.SqlClient;

namespace Hi_Tech_Order_Management_System.DataAccess
{
    public static class OrderDB
    {
        public static void DeleteRecord(int empId)
        {
            //step 1: Connect the DB
            SqlConnection connDB = UtilityDB.ConnectDB();
            //step 2: Perform Delete operation 
            SqlCommand cmdDelete = new SqlCommand("DELETE FROM Orders WHERE EmployeeId = @EmployeeId", connDB);
            cmdDelete.Parameters.AddWithValue("@EmployeeId", empId);
            cmdDelete.ExecuteNonQuery();
            //step 3: Close DB
            connDB.Close();
        }

        public static List<Order> GetRecordList(int empId)
        {
            List<Order> listOrd = new List<Order>();
            // Step 1: Connect the Database
            SqlConnection connDB = UtilityDB.ConnectDB();
            // Step 2: Perform Select all operation
            SqlCommand cmdSelect = new SqlCommand("SELECT * FROM Orders " + "WHERE EmployeeId = @EmployeeId", connDB);
            cmdSelect.Parameters.AddWithValue("@EmployeeId", empId);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            Order ord;
            while (sqlReader.Read())
            {
                ord = new Order();
                ord.OrderId = Convert.ToInt32(sqlReader["OrderId"]);
                ord.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                ord.OrderDate = Convert.ToDateTime(sqlReader["OrderDate"]);
                ord.CustomerId = Convert.ToInt32(sqlReader["CustomerId"]);
                ord.OrderType = sqlReader["OrderType"].ToString();
                ord.OrderStatus = Convert.ToInt32(sqlReader["OrderStatus"]);
                ord.RequiredDate = Convert.ToDateTime(sqlReader["RequiredDate"]);
                ord.ShippingDate = Convert.ToDateTime(sqlReader["ShippingDate"]);
                listOrd.Add(ord);

            }
            // Step 3: CLose the Database
            connDB.Close();
            return listOrd;
        }
    }
}
