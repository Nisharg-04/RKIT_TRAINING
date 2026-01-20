using ExpenseTracker.Common;
using ExpenseTracker.Models;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.DAL
{
    public class UserRepository : IUserRepository
    {
        public usrt01 GetByUsername(string username)
        {
            using (var db = Common.OrmLiteConfig.DbFactory.Open())
            {
                return db.Single<usrt01>(u => u.usrf02 == username);
            }
        }

        public void Insert(usrt01 user)
        {
            using (var db = Common.OrmLiteConfig.DbFactory.Open())
            {
                db.Insert(user);
            }
        }
    }
}