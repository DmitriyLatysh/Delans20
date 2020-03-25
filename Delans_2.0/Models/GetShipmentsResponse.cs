using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delans.Models
{
    public class GetShipmentsResponse : Response
    {
        public string ICN;
        public Shipments[] Shipments;
        public string selectedNumber;
    }
}