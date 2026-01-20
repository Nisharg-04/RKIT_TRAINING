using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.DAL
{
    public interface IExpenseRepository
    {
        void Add(Expense expense);
        Expense GetById(int id, int userId);
        List<Expense> GetAll(int userId);
        void Update(Expense expense);
        void Delete(int id, int userId);
    }

}