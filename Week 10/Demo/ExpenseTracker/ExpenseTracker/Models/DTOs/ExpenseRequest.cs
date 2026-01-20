using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.Models.DTOs
{
    public class ExpenseRequest
    {
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
    }

}