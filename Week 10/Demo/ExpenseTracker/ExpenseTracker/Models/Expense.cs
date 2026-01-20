using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;

namespace ExpenseTracker.Models
{
    

    [Alias("exmt01")]
    public class Expense
    {
        [PrimaryKey]
        [AutoIncrement]
        [Alias("exmf01")]
        public int Id { get; set; }

        [Alias("exmf02")]
        public int UserId { get; set; }

        [Alias("exmf03")]
        public decimal Amount { get; set; }

        [Alias("exmf04")]
        public string Category { get; set; }

        [Alias("exmf05")]
        public string Description { get; set; }

        [Alias("exmf06")]
        public DateTime ExpenseDate { get; set; }

        [Alias("exmf07")]
        public DateTime CreatedAt { get; set; }
    }

}