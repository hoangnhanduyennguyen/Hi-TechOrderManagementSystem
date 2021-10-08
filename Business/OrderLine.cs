using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Order_Management_System.DataAccess;

namespace Hi_Tech_Order_Management_System.Business
{
    public class OrderLine
    {
        private int orderId;
        private int iSBN;
        private int quantityOrdered;

        public int OrderId { get => orderId; set => orderId = value; }
        public int QuantityOrdered { get => quantityOrdered; set => quantityOrdered = value; }
        public int ISBN { get => iSBN; set => iSBN = value; }
        
        public void DeleteOrderLine(int ordId)
        {
            OrderLineDB.DeleteRecord(ordId);
        }
        public void DeleteOrder(int empId)
        {
            OrderDB.DeleteRecord(empId);
        }

        public List<Order> SearchAllOrder(int empId)
        {
            return OrderDB.GetRecordList(empId);
        }
    }
}
