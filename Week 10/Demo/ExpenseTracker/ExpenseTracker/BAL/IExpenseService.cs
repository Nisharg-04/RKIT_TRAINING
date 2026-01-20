using ExpenseTracker.Models.DTOs;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.BAL
{
    public interface IExpenseService
    {
        void AddExpense(int userId, ExpenseRequest request);
        List<Expense> GetExpenses(int userId);
        Expense GetExpense(int id, int userId);
        void UpdateExpense(int id, int userId, ExpenseRequest request);
        void DeleteExpense(int id, int userId);
    }

}