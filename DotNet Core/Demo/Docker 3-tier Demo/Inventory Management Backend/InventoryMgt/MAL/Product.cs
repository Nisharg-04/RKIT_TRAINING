using ServiceStack.DataAnnotations;

namespace InventoryMgt.MAL
{
    [Alias("prdtb01")]
    public class Product
    {
        [Alias("prdf01")]
        [AutoIncrement]
        public int Id { get; set; }

        [Alias("prdf02")]
        public string Name { get; set; }

        [Alias("prdf03")]
        public decimal Price { get; set; }

        [Alias("prdf04")]
        public int Quantity { get; set; }
    }

}
