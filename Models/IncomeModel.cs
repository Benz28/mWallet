using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mWallet.Models
{
    public class IncomeModel
    {
        [Display(Name = "Date")]
        public int date { get; set; }
        [Display(Name = "Description")]
        public string desc { get; set; }
        [Display(Name = "Amount")]
        public decimal amount { get; set; }
        [Display(Name = "Income Type")]
        public string type { get; set; }
    }
}