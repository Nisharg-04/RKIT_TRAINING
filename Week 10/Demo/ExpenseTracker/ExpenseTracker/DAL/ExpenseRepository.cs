using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseTracker.Models;
using ServiceStack.OrmLite;


namespace ExpenseTracker.DAL
{

    public class ExpenseRepository : IExpenseRepository
    {
        public void Add(Expense expense)
        {
            using (var db = Common.OrmLiteConfig.DbFactory.Open())
            {
                db.Insert(expense);
            }
        }

        public Expense GetById(int id, int userId)
        {
            using (var db = Common.OrmLiteConfig.DbFactory.Open())
            {
                return db.Single<Expense>(e => e.Id == id && e.UserId == userId);
            }
        }

        public List<Expense> GetAll(int userId)
        {
            using (var db = Common.OrmLiteConfig.DbFactory.Open())
            {
                return db.Select<Expense>(e => e.UserId == userId);
            }
        }

        public void Update(Expense expense)
        {
            using (var db = Common.OrmLiteConfig.DbFactory.Open())
            {
                db.Update(expense);
            }
        }

        public void Delete(int id, int userId)
        {
            using (var db = Common.OrmLiteConfig.DbFactory.Open())
            {
                db.Delete<Expense>(e => e.Id == id && e.UserId == userId);
            }
        }
    }

}