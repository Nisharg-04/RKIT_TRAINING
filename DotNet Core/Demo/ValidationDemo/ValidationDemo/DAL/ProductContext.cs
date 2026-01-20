using ValidationDemo.DAL.Interfaces;
using ValidationDemo.MAL;

namespace ValidationDemo.DAL
{
    public class ProductContext : IProductRepository
    {
        private static readonly List<Product> _products = new();

        public void Add(Product product)
        {
            product.Id = _products.Count + 1;
            _products.Add(product);
        }
    }

}
