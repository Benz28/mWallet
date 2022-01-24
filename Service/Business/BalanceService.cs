using mWallet.Models;
using mWallet.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mWallet.Service.Business
{
    public class BalanceService
    {
        BalanceDAO dao = new BalanceDAO();

        public WalletModel GetBalance()
        {
            return dao.Balance();
        }
    }
}