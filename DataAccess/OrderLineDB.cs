using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Hi_Tech_Order_Management_System.Business;

namespace Hi_Tech_Order_Management_System.DataAccess
{
    public static class OrderLineDB
    {
        public static void DeleteRecord(int ordId)
        {
            //step 1: Connect the DB
            SqlConnection connDB = UtilityDB.ConnectDB();
            //step 2: Perform Delete operation 
            SqlCommand cmdDelete = new SqlCommand("DELETE FROM OrderLines WHERE OrderId = @OrderId", connDB);
            cmdDelete.Parameters.AddWithValue("@OrderId", ordId);
            cmdDelete.ExecuteNonQuery();
            //step 3: Close DB
            connDB.Close();
        }

        public static List<OrderLine> GetRecordList(int ordId)
        {
            List<OrderLine> listOrdLine = new List<OrderLine>();
            // Step 1: Connect the Database
            SqlConnection connDB = UtilityDB.ConnectDB();
            // Step 2: Perform Select all operation
            SqlCommand cmdSelect = new SqlCommand("SELECT * FROM OrderLines " + "WHERE OrderId = @OrderId", connDB);
            cmdSelect.Parameters.AddWithValue("@OrderId", ordId);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            OrderLine ordLine;
            while (sqlReader.Read())
            {
                ordLine = new OrderLine();
                ordLine.OrderId = Convert.ToInt32(sqlReader["OrderId"]);
                ordLine.ISBN = Convert.ToInt32(sqlReader["ISBN"]);
                ordLine.QuantityOrdered = Convert.ToInt32(sqlReader["QuantityOrdered"]);      
                listOrdLine.Add(ordLine);
            }
            // Step 3: CLose the Database
            connDB.Close();
            return listOrdLine;
        }

    }
}
