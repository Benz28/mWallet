using mWallet.Models;
using mWallet.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mWallet.Service.Business
{
    public class IncomeService
    {
        IncomeDAO dao = new IncomeDAO();

        public List<IncomeModel> GetMonthlyIncome(int? month)
        {
            return dao.MonthlyIncome(month);
        }

        public bool AddIncome(ModifyIncomeModel data)
        {
            return dao.InsertIncome(data);
        }
    }
}