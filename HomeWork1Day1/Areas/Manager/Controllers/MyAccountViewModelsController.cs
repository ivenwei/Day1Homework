using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeWork1Day1.Models;
using HomeWork1Day1.Models.ViewModels;
using HomeWork1Day1.Models.Services;
using PagedList;

namespace HomeWork1Day1.Areas.Manager.Controllers
{
    public class MyAccountViewModelsController : Controller
    {
        private AccountService _accountService;
        private int pagesize = 5;
        public MyAccountViewModelsController()
        {
            _accountService = new AccountService();
        }

        /// <summary>
        /// 分頁用功能
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private IPagedList<MyAccountViewModels> paging(int page)
        {
            var allAccountData = _accountService.getAllAccountData();

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


        // GET: Manager/MyAccountViewModels
        public ActionResult Index(int page = 1)
        {
            return View(paging(page));
        }

        // GET: Manager/MyAccountViewModels/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountBook = _accountService.getSingalAccountData(id.Value);
            if (accountBook == null)
            {
                return HttpNotFound();
            }

            MyAccountViewModels myAccountViewModels = new MyAccountViewModels
            {
                ID = accountBook.Id,
                category = accountBook.Categoryyy == 0 ? "支出" : "收入",
                date = accountBook.Dateee,
                myMoney = accountBook.Amounttt,
                memo = accountBook.Remarkkk
            };
            
            return View(myAccountViewModels);
        }

        // GET: Manager/MyAccountViewModels/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Manager/MyAccountViewModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category,myMoney,date,memo")] MyAccountViewModels myAccountViewModels)
        {
            if (ModelState.IsValid)
            {
                AccountBook account = new AccountBook
                {
                    Id = Guid.NewGuid(),
                    Categoryyy = myAccountViewModels.category == "支出" ? 0 : 1,
                    Amounttt = decimal.ToInt32(myAccountViewModels.myMoney),
                    Dateee = myAccountViewModels.date,
                    Remarkkk = myAccountViewModels.memo
                };
                myAccountViewModels.ID = Guid.NewGuid();
                _accountService.addAccountData(account);
                _accountService.save();
                return RedirectToAction("Index");
            }

            return View(myAccountViewModels);
        }

        // GET: Manager/MyAccountViewModels/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountBook = _accountService.getSingalAccountData(id.Value);
            if (accountBook == null)
            {
                return HttpNotFound();
            }

            MyAccountViewModels myAccountViewModels = new MyAccountViewModels
            {
                ID = accountBook.Id,
                category = accountBook.Categoryyy == 0 ? "支出" : "收入",
                date = accountBook.Dateee,
                myMoney = accountBook.Amounttt,
                memo = accountBook.Remarkkk
            };

            return View(myAccountViewModels);
        }

        // POST: Manager/MyAccountViewModels/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,category,myMoney,date,memo")] MyAccountViewModels myAccountViewModels)
        {

            var editAccount = _accountService.getSingalAccountData(myAccountViewModels.ID);
            if (editAccount!=null && ModelState.IsValid)
            {
                _accountService.editAccountData(myAccountViewModels, editAccount);
                _accountService.save();
                return RedirectToAction("Index");
            }
            return View(myAccountViewModels);
        }

        // GET: Manager/MyAccountViewModels/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deleteAccount = _accountService.getSingalAccountData(id.Value);
            if (deleteAccount == null)
            {
                return HttpNotFound();
            }

            MyAccountViewModels myAccountViewModels = new MyAccountViewModels
            {
                ID = deleteAccount.Id,
                category = deleteAccount.Categoryyy == 0 ? "支出" : "收入",
                date = deleteAccount.Dateee,
                myMoney = deleteAccount.Amounttt,
                memo = deleteAccount.Remarkkk
            };
            return View(myAccountViewModels);
        }

        // POST: Manager/MyAccountViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var deleteAccount = _accountService.getSingalAccountData(id);
            if (deleteAccount != null)
            {
                _accountService.deleteAccountData(deleteAccount);
            }
            _accountService.save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _accountService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
