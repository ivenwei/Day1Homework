using HomeWork1Day1.Models;
using HomeWork1Day1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HomeWork1Day1.Models.Services;
using System.Net;

namespace HomeWork1Day1.Controllers
{
    public class MyAccountController : Controller
    {

        AccountModel context = new AccountModel();
        private AccountService _accountSvc;
        private int pagesize = 5;

        public MyAccountController()
        {
            _accountSvc = new AccountService();
        }

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
                _accountSvc.addAccountData(newbookData);
                _accountSvc.save();
                //context.AccountBook.Add(newbookData);
                //context.SaveChanges();
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
                _accountSvc.addAccountData(newbookData);
                _accountSvc.save();
                //context.AccountBook.Add(newbookData);
                //context.SaveChanges();
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
            var allAccountData = _accountSvc.getAllAccountData();

            var accountList = allAccountData
                            .Select(
                            d => new MyAccountViewModels
                            {
                                ID = d.Id,
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

        //Get
        public ActionResult Edit(Guid? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var account = _accountSvc.getSingalAccountData(Id.Value);
            if (account == null){
                return HttpNotFound();
            }

            MyAccountViewModels accountObj = null;
            if (account != null)
            {
                accountObj = new MyAccountViewModels
                {
                    category = account.Categoryyy == 0 ? "支出" : "收入",
                    date = account.Dateee,
                    memo = account.Remarkkk,
                    myMoney = account.Amounttt
                };
            }
            return View(accountObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,category,myMoney,date,memo")] MyAccountViewModels accountObj)
        {
            var editAccount = _accountSvc.getSingalAccountData(accountObj.ID);
            if (editAccount != null && ModelState.IsValid)
            {
                _accountSvc.editAccountData(accountObj, editAccount);
                _accountSvc.save();
                return RedirectToAction("myAccountBook");
            }
            return View(accountObj);
        }


        //Get
        public ActionResult Delete(Guid? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var account = _accountSvc.getSingalAccountData(Id.Value);
            if (account == null)
            {
                return HttpNotFound();
            }

            MyAccountViewModels accountObj = new MyAccountViewModels
            {
                category = account.Categoryyy == 0 ? "支出" : "收入",
                date = account.Dateee,
                memo = account.Remarkkk,
                myMoney = account.Amounttt
            };

            return View(accountObj);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(Guid id)
        {
            var account = _accountSvc.getSingalAccountData(id);
            if (account != null)
            {
                _accountSvc.deleteAccountData(account);
                _accountSvc.save();
            }

            return RedirectToAction("myAccountBook");
        }

        public ActionResult Detail(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountData = _accountSvc.getSingalAccountData(id.Value);
            if (accountData == null)
            {
                return HttpNotFound();
            };

            MyAccountViewModels accountObj = new MyAccountViewModels
            {
                ID = accountData.Id,
                category = accountData.Categoryyy == 0 ? "支出" : "收入",
                date = accountData.Dateee,
                memo = accountData.Remarkkk,
                myMoney = accountData.Amounttt
            };
            return View(accountObj);
        }
    }
}