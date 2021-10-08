using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Order_Management_System.DataAccess;

namespace Hi_Tech_Order_Management_System.Business
{
    public class Publisher
    {
        private int publisherId;
        private string publisherName;
        private string webAddress;

        public int PublisherId { get => publisherId; set => publisherId = value; }
        public string PublisherName { get => publisherName; set => publisherName = value; }
        public string WebAddress { get => webAddress; set => webAddress = value; }
        public Publisher SearchPublisher(int pubId)
        {
            return PublisherDB.SearchRecord(pubId);
        }
    }
}
