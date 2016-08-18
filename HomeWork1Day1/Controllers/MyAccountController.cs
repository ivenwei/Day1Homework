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
        private int pagesize = 5;

        // GET: MyAccount
        public ActionResult myAccountBook(int page = 1)
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
            ViewData["myAccountList"] = NewMethod(page); 
            return View();
        }

        [HttpPost]
        public ActionResult myAccountBook([Bind(Include = "category,myMoney,date,memo")] MyAccountViewModels accountObj)
        {
            if (ModelState.IsValid)
            {
                AccountBook newbookData = new AccountBook
                {
                    Id = Guid.NewGuid(),
                    Categoryyy = accountObj.category == "支出" ? 0 : 1,
                    Amounttt = decimal.ToInt32(accountObj.myMoney),
                    Dateee = accountObj.date,
                    Remarkkk = accountObj.memo
                };
                context.AccountBook.Add(newbookData);
                context.SaveChanges();
                return RedirectToAction("myAccountBook");
            }

            return View(accountObj);
        }


        [ChildActionOnly]
        public ActionResult myAccountBookChildAction(int page = 1)
        {
            return View(NewMethod(page));
        }


        /// <summary>
        /// ### 增加 AJAX 用的 Action
        /// </summary>
        /// <param name="accountObj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ajaxPostAccount([Bind(Include = "category,myMoney,date,memo")] MyAccountViewModels accountObj)
        {
            if (ModelState.IsValid)
            {
                AccountBook newbookData = new AccountBook
                {
                    Id = Guid.NewGuid(),
                    Categoryyy = accountObj.category == "支出" ? 0 : 1,
                    Amounttt = decimal.ToInt32(accountObj.myMoney),
                    Dateee = accountObj.date,
                    Remarkkk = accountObj.memo
                };
                context.AccountBook.Add(newbookData);
                context.SaveChanges();
            }
            return PartialView("_ajaxPostAccount", NewMethod(1));
        }


        /// <summary>
        /// 分頁用功能
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private IPagedList<MyAccountViewModels> NewMethod(int page)
        {
            var accountList = context.AccountBook
                            .ToList()
                            .Select(
                            d => new MyAccountViewModels
                            {
                                category = d.Categoryyy == 0 ? "支出" : "收入",
                                date = d.Dateee,
                                myMoney = d.Amounttt,
                                memo = d.Remarkkk
                            });
            var accountPageData = accountList.OrderByDescending(d => d.date);
            int currentPage = page < 1 ? 1 : page;
            var result = accountPageData.ToPagedList(page, pagesize);
            return result;
        }

    }
}