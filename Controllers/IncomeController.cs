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
    public class IncomeController : Controller
    {
        private IncomeService service = new IncomeService();
        public ActionResult Income(int? month)
        {
            ViewBag.ddlMonth = Months; // get month for dropdownlist
            ViewBag.curMonth = month == null ? DateTime.Now.Month.ToString() : month.ToString();  // get current month or selected month
            return View(service.GetMonthlyIncome(month));
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ModifyIncomeModel data)
        {
            if (service.AddIncome(data))
            {
                TempData["success"] = "Successfully added into income";
            }
            else
            {
                TempData["fail"] = "Fail to insert into income";
            }
            return RedirectToAction("Income");
        }

        public ActionResult DeleteIncome(string desc = "", decimal amt = 0)
        {
            service.PostRemoveIncome(desc, amt);
            return RedirectToAction("Income");
        }

        #region dropdownlist variable
        public IEnumerable<SelectListItem> Months
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