using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HomeWork1Day1.Models.ViewModels
{
    public class MyAccountViewModels
    {


        [DisplayName("類別")]
        public string category { get; set; }

        [DisplayName("金額")]
        public decimal myMoney { get; set; }

        [DisplayName("日期")]
        public DateTime date { get; set; }

        [DisplayName("備註")]
        public string memo { get; set; }
    }
}