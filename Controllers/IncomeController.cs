using mWallet.Models;
using mWallet.Service.Business;
using System;
using System.Collections.Generic;
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
            return View(service.GetMonthlyIncome(month));
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ModifyIncomeModel data)
        {
            service.AddIncome(data);
            TempData["success"] = "Successfully added into income";
            return RedirectToAction("Income");
        }

    }
}