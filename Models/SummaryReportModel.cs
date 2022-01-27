using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mWallet.Models
{
    public class SummaryReportModel
    {
        [Display(Name = "Year")]
        public int year { get; set; }
        [Display(Name = "Month")]
        public string month { get; set; }
        [Display(Name = "Amount")]
        public decimal amount { get; set; }
    }
}