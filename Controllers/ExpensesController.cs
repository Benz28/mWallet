using mWallet.Models;
using mWallet.Service.Business;
using System;
using System.Collections.Generic;
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
            return View(service.GetMonthlyExpenses(month));
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ModifyExpensesModel data)
        {
            service.AddExpenses(data);
            TempData["success"] = "Successfully added into expenses";
            return RedirectToAction("Expenses");
        }
    }
}