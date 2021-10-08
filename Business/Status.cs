using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Order_Management_System.DataAccess;

namespace Hi_Tech_Order_Management_System.Business
{
    public class Status
    {
        private int id;
        private string description;

        public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
    
        public List<Status> SearchStatus(string select)
        {
            return StatusDB.GetRecordList(select);
        }
    }
}
