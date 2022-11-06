using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Configuration;
using TransportManagement.Helper;
using TransportManagement.Models;

namespace TransportManagement.Controllers
{
    [Authorize]
    public class TransportOrderController : Controller
    {
        private readonly IAppConfiguration _configuration;
        private readonly ServiceContext serviceContext;
        public TransportOrderController(IAppConfiguration configuration)
        {
            this._configuration = configuration;
            serviceContext = new ServiceContext(configuration);
        }

        public IActionResult Index()
        {
            return View("CreateInvoice");
        }

        public IActionResult CreateInvoice(TransportOrderModel model)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("CreateID", model.CreateID);
            data.Add("InvoiceDate", model.InvoiceDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            data.Add("Sender", model.Sender);
            data.Add("Receiver", model.Receiver);
            data.Add("ItemInfo", model.ItemInfo);
            data.Add("Quantity", model.Quantity);
            data.Add("UnitPrice", Convert.ToString(model.UnitPrice));
            data.Add("TotalPrice", Convert.ToString(model.TotalPrice));
            data.Add("PaidPrice", Convert.ToString(model.PaidPrice));
            //data.Add("ReceivedOn", model.ReceivedOn.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            data.Add("ReceivedBy", model.ReceivedBy);
            data.Add("SenderMobileNo", model.SenderMobileNo);
            data.Add("ReceiverMobileNo", model.ReceiverMobileNo);
            data.Add("InvoiceGeneratedBy", model.InvoiceGeneratedBy);
            data.Add("ETag", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            serviceContext.CreateInvoice(data);

            TransportOrderModel invoiceModel = serviceContext.GetInvoice("CreateID", model.CreateID);

            return View("ViewInvoice", invoiceModel);
        }

        public IActionResult ViewInvoice(string invoiceID)
        {
            TransportOrderModel invoiceModel = serviceContext.GetInvoice("SystemInvoiceId", invoiceID);
            
            return View("ViewInvoice", invoiceModel);
        }

        public IActionResult EditInvoice(string invoiceID)
        {
            TransportOrderModel invoiceModel = serviceContext.GetInvoice("SystemInvoiceId", invoiceID);

            return View("EditInvoice", invoiceModel);
        }

        public IActionResult UpdateInvoiceData(TransportOrderModel model)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("InvoiceDate", model.InvoiceDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            data.Add("Sender", model.Sender);
            data.Add("Receiver", model.Receiver);
            data.Add("ItemInfo", model.ItemInfo);
            data.Add("Quantity", model.Quantity);
            data.Add("UnitPrice", Convert.ToString(model.UnitPrice));
            data.Add("TotalPrice", Convert.ToString(model.TotalPrice));
            data.Add("PaidPrice", Convert.ToString(model.PaidPrice));
            data.Add("ReceivedOn", model.ReceivedOn.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            data.Add("ReceivedBy", model.ReceivedBy);
            data.Add("SenderMobileNo", model.SenderMobileNo);
            data.Add("ReceiverMobileNo", model.ReceiverMobileNo);
            data.Add("ETag", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            serviceContext.UpdateInvoiceById("SystemInvoiceId",model.SystemInvoiceId,data);

            TransportOrderModel invoiceModel = serviceContext.GetInvoice("SystemInvoiceId", model.SystemInvoiceId);

            return View("ViewInvoice", invoiceModel);
        }
    }
}
