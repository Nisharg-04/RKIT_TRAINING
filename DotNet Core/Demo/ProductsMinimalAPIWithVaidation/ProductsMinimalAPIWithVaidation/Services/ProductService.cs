using ProductsMinimalAPIWithVaidation.Models;

namespace ProductsMinimalAPIWithVaidation.Services
{
    public static class ProductService
    {
        private static List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 100,
                Quantity = 12
            }
        };
        public static List<Product> GetAllProducts()
        {
            return products;
        }

        public static Product? GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public static Product AddProduct(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            return product;
        }

        public static bool UpdateProduct(int id, Product updatedProduct)
        {
            var existingProduct = GetProductById(id);

            if (existingProduct == null)
                return false;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Quantity = updatedProduct.Quantity;

            return true;
        }


        public static bool DeleteProduct(int id)
        {
            var product = GetProductById(id);

            if (product == null)
                return false;

            products.Remove(product);
            return true;
        }
    }
}
