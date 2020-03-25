using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delans.Models
{
    public class Shipments
    {
        public string Number;
        public string ShipmentNumber;
        public string ClientShipmentNumber;
        public DateTime? Date;
        public DateTime? DesiredDate;
        public string Is_delivered;
        public string Is_deliveredComents;
        public string Recipient;
        public string Recipient_Name;
        public string Recipient_Phone;
        public string Recipient_City;
        public string Recipient_FullAddress;
        public double? Weight;
        public double? VolumeWeight;
        public int? Places;
        public int? CashOfDelivery;
        public int? DeclaredValue;
        public int? Total;
        public int? DeliveryPrice;
        public string Sender_City;
        public string DeliveryType;
        public string add_Is_delivered;
        public string add_detail;
    }
}