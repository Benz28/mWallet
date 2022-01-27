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

        public List<SummaryReportModel> GetSummaryByMonth()
        {
            return dao.SummaryByMonth();
        }

        public List<SummaryReportModel> GetSummaryByYear()
        {
            return dao.SummaryByYear();
        }
    }
}