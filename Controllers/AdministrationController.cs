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
    public class AdministrationController : Controller
    {
        private readonly IAppConfiguration _configuration;
        private readonly ServiceContext serviceContext;
        public AdministrationController(IAppConfiguration configuration)
        {
            this._configuration = configuration;
            serviceContext = new ServiceContext(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewAdministration()
        {
            return View("Administration");
        }

        public IActionResult GetOrderInvoices(string fromDate, string toDate)
        {
            List<TransportOrderModel> invoicess = serviceContext.GetInvoicesByDate(fromDate,toDate);

            return View("Administration",invoicess);
        }
    }
}
