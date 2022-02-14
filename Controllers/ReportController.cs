using mWallet.Models;
using mWallet.Service.Business;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public ActionResult AnnualReport(int? year)
        {
            List<ReportModel> expenses = service.GetExpensesByYear(year);
            List<ReportModel> income = service.GetIncomeByYear(year);
            List<ReportModel> data = new List<ReportModel>();

            List<string> totalYear = service.GetTotalYear();

            ViewBag.ddlYear = totalYear.Where(y => !string.IsNullOrEmpty(y)).Select((yearDB, index) => new SelectListItem
            {
                Value = yearDB,
                Text = yearDB
            }); // get year for dropdownlist
            ViewBag.curYear = year == null ? DateTime.Now.Year.ToString() : year.ToString();  // get current year or selected year

            for (int i = 1; i < 13; i++)
            {
                var dt = new DateTime(2022, i, 1);
                ReportModel tempExp = expenses.Find(x => x.month.Equals(i.ToString()));
                ReportModel tempInc = income.Find(x => x.month == i.ToString());

                if (tempExp == null)
                {
                    data.Add(new ReportModel() { amount = 0, month = dt.ToString("MMM") });
                }
                else
                {
                    tempExp.month = dt.ToString("MMM");
                    data.Add(tempExp);
                }

                if (tempInc == null)
                {
                    data.Add(new ReportModel() { amount = 0, month = dt.ToString("MMM") });
                }
                else
                {
                    tempInc.month = dt.ToString("MMM");
                    data.Add(tempInc);
                }
            }
            return View(data);
        }

        #region dropdownlist variable
        public IEnumerable<SelectListItem> Years
        {
            get
            {
                return DateTimeFormatInfo
                       .InvariantInfo
                       .MonthNames
                       .Where(m => !String.IsNullOrEmpty(m))
                       .Select((monthName, index) => new SelectListItem
                       {
                           Value = (index + 1).ToString(),
                           Text = monthName
                       });
            }
        }
        #endregion

    }
}