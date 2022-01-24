using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mWallet.Models
{
    public class ModifyExpensesModel
    {
        [Display(Name = "Date")]
        public string date { get; set; }
        [Display(Name = "Description")]
        public string desc { get; set; }
        [Display(Name = "Amount")]
        public decimal amount { get; set; }
        [Display(Name = "Payment Type")]
        public PaymentType type { get; set; }
    }

    public enum PaymentType
    {
        Cash,
        Bank
    }
}