using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mWallet.Models
{
    public class SummaryReport
    {
        public IEnumerable<SummaryReportModel> summary_by_year { get; set; }
        public IEnumerable<SummaryReportModel> summary_by_month { get; set; }
    }
}