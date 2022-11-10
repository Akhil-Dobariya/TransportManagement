using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IActionResult GetInvoicesByDatenPage(string pageToGet)
        {
            int rowsPerPage = 10;
            int pageNo = 0;

            HttpContext.Session.TryGetValue("fromDate", out byte[] fromDateArr);
            HttpContext.Session.TryGetValue("toDate", out byte[] toDateArr);
            HttpContext.Session.TryGetValue("currentPage", out byte[] currentPage);

            string fromDateStr = Encoding.Default.GetString(fromDateArr);
            string toDateStr = Encoding.Default.GetString(toDateArr);

            string currentPageStr = string.Empty;

            if (currentPage == null || currentPage.Length < 1)
            {
                currentPageStr = "1";
            }
            else
            {
                currentPageStr = Encoding.Default.GetString(currentPage);
            }

            if (pageToGet == "Next")
            {
                pageNo = Convert.ToInt16(currentPageStr) + 1;
            }
            else
            {
                pageNo = Convert.ToInt16(currentPageStr) - 1;   
            }

            if (currentPageStr == "0")
            {
                pageNo = 1;
            }

            List<TransportOrderModel> invoicess = serviceContext.GetInvoicesByDatenPage(fromDateStr,toDateStr,pageNo,rowsPerPage);

            byte[] pageNoArr = Encoding.ASCII.GetBytes(Convert.ToString(pageNo));
            HttpContext.Session.Set("currentPage", pageNoArr);

            return View("Administration", invoicess);
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
            byte[] fromDateArr = Encoding.Default.GetBytes(fromDate);
            HttpContext.Session.Set("fromDate", fromDateArr);

            byte[] toDateArr = Encoding.Default.GetBytes(toDate);
            HttpContext.Session.Set("toDate", toDateArr);

            byte[] pageNoArr = Encoding.Default.GetBytes("1");
            HttpContext.Session.Set("currentPage", pageNoArr);

            //List<TransportOrderModel> invoicess = serviceContext.GetInvoicesByDate(fromDate,toDate);

            List<TransportOrderModel> invoicess = serviceContext.GetInvoicesByDatenPage(fromDate, toDate, 1, 10);

            return View("Administration",invoicess);
        }
    }
}
