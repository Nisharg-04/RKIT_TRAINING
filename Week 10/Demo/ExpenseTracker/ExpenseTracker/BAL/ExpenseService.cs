using ExpenseTracker.DAL;
using ExpenseTracker.Models.DTOs;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseTracker.Models.Logging;

namespace ExpenseTracker.BAL
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repo;
        private readonly INLogLogger _logger;

        public ExpenseService(IExpenseRepository repo, INLogLogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public void AddExpense(int userId, ExpenseRequest request)
        {
            var expense = new Expense
            {
                UserId = userId,
                Amount = request.Amount,
                Category = request.Category,
                Description = request.Description,
                ExpenseDate = request.ExpenseDate,
                CreatedAt = DateTime.UtcNow
            };

            _repo.Add(expense);
            _logger.Info($"Expense added | User:{userId} | Amount:{request.Amount}");
        }

        public List<Expense> GetExpenses(int userId)
        {
            return _repo.GetAll(userId);
        }

        public Expense GetExpense(int id, int userId)
        {
            var expense = _repo.GetById(id, userId);
            if (expense == null)
                throw new Exception("Expense not found");

            return expense;
        }

        public void UpdateExpense(int id, int userId, ExpenseRequest request)
        {
            var expense = GetExpense(id, userId);

            expense.Amount = request.Amount;
            expense.Category = request.Category;
            expense.Description = request.Description;
            expense.ExpenseDate = request.ExpenseDate;

            _repo.Update(expense);
            _logger.Info($"Expense updated | Id:{id} | User:{userId}");
        }

        public void DeleteExpense(int id, int userId)
        {
            _repo.Delete(id, userId);
            _logger.Info($"Expense deleted | Id:{id} | User:{userId}");
        }
    }

}