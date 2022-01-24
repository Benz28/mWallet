using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mWallet.Models
{
    public class WalletModel
    {
        [Display(Name = "Current Balance")]
        public decimal current_balance { get; set; }
        [Display(Name = "Bank Balance")]
        public decimal bank_balance { get; set; }
    }
}