using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExpenseTracker.BAL;
using ExpenseTracker.Common;
using ExpenseTracker.Models.DTOs;
using System.Web.Http;

namespace ExpenseTracker.Controllers
{

    [JwtAuthorize] // JWT filter will protect this
    [RoutePrefix("api/v1/expenses")]
    public class ExpenseController : ApiController
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddExpense(ExpenseRequest request)
        {
            int userId = UserContext.GetUserId(this);
            _service.AddExpense(userId, request);
            return Ok(new { message = "Expense added" });
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetExpenses()
        {
            int userId = UserContext.GetUserId(this);
            return Ok(_service.GetExpenses(userId));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetExpense(int id)
        {
            int userId = UserContext.GetUserId(this);
            return Ok(_service.GetExpense(id, userId));
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateExpense(int id, ExpenseRequest request)
        {
            int userId = UserContext.GetUserId(this);
            _service.UpdateExpense(id, userId, request);
            return Ok(new { message = "Expense updated" });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteExpense(int id)
        {
            int userId = UserContext.GetUserId(this);
            _service.DeleteExpense(id, userId);
            return Ok(new { message = "Expense deleted" });
        }
    }

}