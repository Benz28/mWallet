using mWallet.Service.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mWallet.Controllers
{
    public class HomeController : Controller
    {
        private BalanceService service = new BalanceService();
        public ActionResult Balance()
        {
            return View(service.GetBalance());
        }
    }
}