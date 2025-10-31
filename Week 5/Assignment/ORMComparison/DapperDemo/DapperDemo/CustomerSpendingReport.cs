using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo
{
    public class CustomerSpendingReport
    {
        public string CustomerName { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public override string ToString()
        {
            return $"[Name: {CustomerName}, Orders: {TotalOrders}, Spent: {TotalSpent:C}]";
        }
    }
}
