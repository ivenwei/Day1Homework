using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork1Day1.Models.Services
{
    public class AccountService
    {
        private AccountModel context;

        public AccountService()
        {
            context = new AccountModel();
        }

        public List<AccountBook> getAllAccountData()
        {
            return context.AccountBook.ToList();
        }

        public AccountBook getSingalAccountData(Guid id)
        {
            return context.AccountBook.Where(d=>d.Id == id).FirstOrDefault();
        }

        public void deleteAccountData(AccountBook deleData)
        {
            context.AccountBook.Remove(deleData);
        }

        public void addAccountData(AccountBook accountData)
        {
            accountData.Id = Guid.NewGuid();
            context.AccountBook.Add(accountData);
        }

        public void editAccountData(AccountBook modifyAccountData,AccountBook accountData)
        {
            accountData.Amounttt = accountData.Amounttt;
            accountData.Categoryyy = accountData.Categoryyy;
            accountData.Dateee = accountData.Dateee;
            accountData.Remarkkk = accountData.Remarkkk;
        }

        public void save()
        {
            context.SaveChanges();
        }
    }
}