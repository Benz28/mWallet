using mWallet.Models;
using mWallet.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mWallet.Service.Business
{
    public class ExpensesService
    {
        ExpensesDAO dao = new ExpensesDAO();

        public List<ExpensesModel> GetMonthlyExpenses(int? month)
        {
            return dao.MonthlyExpenses(month);
        }

        public bool AddExpenses(ModifyExpensesModel data)
        {
            return dao.InsertExpenses(data);
        }

        public bool PostRemoveExpenses(string desc, decimal amt)
        {
            return dao.RemoveExpenses(desc, amt);
        }
    }
}