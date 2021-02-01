using Library.Domain.Core;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Infrastructure.Data
{
    public class AdminRepos : DisposeRelease, IAdminRepos
    {
        

        public AdminRepos(LibraryContext context)
        {
            db = context;
        }


        public IEnumerable<Account> Users 
        { 
            get 
            {
                return db.Accounts.
                    Where(a => a.Rights == "user");
            } 
        }

        public Account FindByPassword(string password, string name)
        {
            var account = db.Accounts.Where(a => a.Password == password & a.Name == name).FirstOrDefault();
            if (account != null)
                return account;
            return new Account();
        }

        public Account GetAccount(int id)
        {
            return db.Accounts.Find(id);
        }

        public IEnumerable<Account> SuperUsers 
        { 
            get 
            { 
                return db.Accounts.
                    Where(a=>a.Rights == "admin" | a.Rights == "librarian"); 
            } 
        }

        public void AddAccount(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public void ChangePassword(int id, string newPassword)
        {
                var acc = db.Accounts.Find(id);
                if (acc != null)
                {
                    acc.Password = newPassword;
                }
            
            db.SaveChanges();
        }

        public void RemoveAccount(int idAccount)
        {
            db.Accounts.Remove(db.Accounts.Find(idAccount));
            db.SaveChanges();   
        }

        
    }
}
