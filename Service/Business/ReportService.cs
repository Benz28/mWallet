using mWallet.Models;
using mWallet.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mWallet.Service.Business
{
    public class ReportService
    {
        ReportDAO dao = new ReportDAO();

        #region Summary Report
        public List<ReportModel> GetSummaryByMonth()
        {
            return dao.SummaryByMonth();
        }

        public List<ReportModel> GetSummaryByYear()
        {
            return dao.SummaryByYear();
        }
        #endregion

        #region Annual Report
        public List<ReportModel> GetExpensesByYear(int? year)
        {
            return dao.ExpensesByYear(year);
        }
        
        public List<ReportModel> GetIncomeByYear(int? year)
        {
            return dao.IncomeByYear(year);
        }

        public List<string> GetTotalYear()
        {
            return dao.TotalYear();
        }
        #endregion
    }
}