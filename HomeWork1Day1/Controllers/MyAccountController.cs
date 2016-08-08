using HomeWork1Day1.Models;
using HomeWork1Day1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HomeWork1Day1.Controllers
{
    public class MyAccountController : Controller
    {

        AccountModel context = new AccountModel();

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

        private int pagesize = 5;
        [ChildActionOnly]
        public ActionResult myAccountBookChildAction(int page = 1)
        {
            var accountList = context.AccountBook.ToList().Select(
                d => new MyAccountViewModels
                {
                    category = d.Categoryyy==0?"收入":"支出",
                    date = d.Dateee,
                    myMoney = d.Amounttt,
                    memo = d.Remarkkk
                });

            int currentPage = page < 1 ? 1 : page;
            var accountPageData = accountList.OrderBy(d => d.date);
            var result = accountPageData.ToPagedList(currentPage, pagesize);
            return View(result);
        }

    }
}