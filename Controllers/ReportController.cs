using mWallet.Models;
using mWallet.Service.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mWallet.Controllers
{
    public class ReportController : Controller
    {
        private ReportService service = new ReportService();

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult SummaryReport()
        {
            SummaryReport data = new SummaryReport();
            data.summary_by_year = service.GetSummaryByYear();
            data.summary_by_month = service.GetSummaryByMonth();
            return View(data);
        }
    }
}