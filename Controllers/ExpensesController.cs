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
    public class ExpensesController : Controller
    {
        private ExpensesService service = new ExpensesService();

        public ActionResult Expenses(int? month)
        {
            ViewBag.ddlMonth = Months; // get month for dropdownlist
            ViewBag.curMonth = month == null ? DateTime.Now.Month.ToString() : month.ToString();  // get current month or selected month
            return View(service.GetMonthlyExpenses(month));
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ModifyExpensesModel data)
        {
            if (service.AddExpenses(data))
            {
                TempData["success"] = "Successfully added into expenses";
            }
            else
            {
                TempData["fail"] = "Fail to insert into expenses";
            }
            return RedirectToAction("Expenses");
        }

        public ActionResult DeleteExpenses(string desc = "", decimal amt = 0)
        {
            service.PostRemoveExpenses(desc, amt);
            return RedirectToAction("Expenses");
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