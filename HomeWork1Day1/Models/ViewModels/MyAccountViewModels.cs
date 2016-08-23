using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeWork1Day1.Models.ViewModels
{
    public class MyAccountViewModels
    {

        [Required]
        [Display(Name="類別")]
        public string category { get; set; }

        [Required]
        [Display(Name = "金額")]
        public decimal myMoney { get; set; }

        [Required]
        [Display(Name = "日期")]
        public DateTime date { get; set; }

        [Required]
        [Display(Name = "備註")]
        [StringLength(500)]
        public string memo { get; set; }
    }
}