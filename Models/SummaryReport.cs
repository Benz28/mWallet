using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mWallet.Models
{
    public class SummaryReport
    {
        public IEnumerable<ReportModel> summary_by_year { get; set; }
        public IEnumerable<ReportModel> summary_by_month { get; set; }
    }
}