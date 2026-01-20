using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.DAL
{
    public interface IUserRepository
    {
        usrt01 GetByUsername(string username);
        void Insert(usrt01 user);
    }
}