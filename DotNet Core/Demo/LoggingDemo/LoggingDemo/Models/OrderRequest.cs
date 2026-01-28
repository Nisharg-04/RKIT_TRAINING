namespace LoggingDemo.Models
{
    public class OrderRequest
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
    }

}
