using HomeWork1Day1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork1Day1.Controllers
{
    public class MyAccountController : Controller
    {

        // GET: MyAccount
        public ActionResult myAccountBook()
        {
            List<SelectListItem> categoryContent = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "支出",
                    Value = "支出",
                    Selected = false,
                },
                new SelectListItem
                {
                    Text = "收入",
                    Value = "收入",
                    Selected = false,
                },
            };
            ViewData["myAccountCategory"] = categoryContent;

            return View();
        }

        [ChildActionOnly]
        public ActionResult myAccountBookChildAction()
        {
            var accountList = new List<MyAccountViewModels>
            {
                new MyAccountViewModels
                {
                    category = "支出",
                    date = DateTime.Now.Date.AddDays(1).Date,
                    memo = "",
                    myMoney = 300
                },
                new MyAccountViewModels
                {
                    category = "支出",
                    date = DateTime.Now.Date.AddDays(2).Date,
                    memo = "",
                    myMoney = 1000
                },
                new MyAccountViewModels
                {
                    category = "收入",
                    date = DateTime.Now.Date.AddDays(3).Date,
                    memo = "",
                    myMoney = 30000
                }
            };


            return View(accountList);
        }

    }
}