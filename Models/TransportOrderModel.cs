using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models
{
    public class TransportOrderModel
    {
        public string SystemInvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string ItemInfo { get; set; }
        public string Quantity { get; set; }
        public long TotalPrice { get; set; }
        public long UnitPrice { get; set; }
        public long PaidPrice { get; set; }
        public DateTime ReceivedOn { get; set; }
        public string ReceivedBy { get; set; }
        public string InvoiceGeneratedBy { get; set; }
        public string SenderMobileNo { get; set; }
        public string ReceiverMobileNo { get; set; }
        public string CreateID { get; set; }

    }
}
