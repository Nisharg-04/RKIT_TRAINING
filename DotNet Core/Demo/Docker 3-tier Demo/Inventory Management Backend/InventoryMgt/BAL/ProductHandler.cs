using InventoryMgt.BAL.Interfaces;
using InventoryMgt.DAL.Interfaces;
using InventoryMgt.MAL;
using ServiceStack;

namespace InventoryMgt.BAL
{
    public class ProductHandler : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ILogger<ProductHandler> _logger;

        public ProductHandler(
            IProductRepository repo,
            ILogger<ProductHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public List<Product> GetAll()
        {
            _logger.LogInformation("Fetching all products");
            return _repo.GetAll();
        }

        public Product Get(int id)
        {
            return _repo.GetById(id);
        }

        public void Add(Product product)
        {
           
            _repo.Add(product);
        }

        public void Update(Product product)
        {
            _repo.Update(product);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
