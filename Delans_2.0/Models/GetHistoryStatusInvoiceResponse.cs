using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delans.Models
{
    public class GetHistoryStatusInvoiceResponse : Response
    {
        public string ICN;
        public struct nHistory
        {
            public DateTime? Date;
            public string Is_Delivered;
            public string Is_deliveredComents;
        }

        public struct nList {
            public string ShipmentNumber;
            public string ClientShipmentNumber;
            public string FromCity;
            public string ToCity;
            public nHistory[] History;
        }

        public nList[] List;
        
    }
}