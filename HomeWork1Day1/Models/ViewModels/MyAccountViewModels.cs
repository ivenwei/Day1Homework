using HomeWork1Day1.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeWork1Day1.Models.ViewModels
{
    public class MyAccountViewModels
    {

        public Guid ID { get; set; }

        [Required]
        [Display(Name="類別")]
        public string category { get; set; }

        [Required]
        [Display(Name = "金額")]
        [RegularExpression(@"\d+$", ErrorMessage = "請輸入正確資料(非0整數)")]
        public decimal myMoney { get; set; }

        [Required]
        [Display(Name = "日期")]
        [DateRange(-1000,0,ErrorMessage ="填入日期不能超過今日")]
        public DateTime date { get; set; }

        [Required]
        [Display(Name = "備註")]
        [StringLength(100,ErrorMessage = "最多輸入100個字元")]
        public string memo { get; set; }
    }
}