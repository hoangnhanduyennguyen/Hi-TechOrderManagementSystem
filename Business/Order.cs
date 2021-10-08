using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Order_Management_System.DataAccess;

namespace Hi_Tech_Order_Management_System.Business
{
    public class Order
    {
        private int orderId;
        private DateTime orderDate;
        private string orderType;
        private DateTime requiredDate;
        private DateTime shippingDate;
        private int orderStatus;
        private int customerId;
        private int employeeId;

        public int OrderId { get => orderId; set => orderId = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public string OrderType { get => orderType; set => orderType = value; }
        public DateTime RequiredDate { get => requiredDate; set => requiredDate = value; }
        public DateTime ShippingDate { get => shippingDate; set => shippingDate = value; }
        public int OrderStatus { get => orderStatus; set => orderStatus = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }
    
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
