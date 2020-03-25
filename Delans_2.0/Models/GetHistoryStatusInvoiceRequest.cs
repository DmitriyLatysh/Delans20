using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delans.Models
{
    public class GetHistoryStatusInvoiceRequest
    {
        public string ICN;
        //public string[] ShipmentNumber;
        public List<string> ShipmentNumber = new List<string>();
        public string SessionID;
    }
}