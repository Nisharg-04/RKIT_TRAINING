using InventoryMgt.DAL.Interfaces;
using InventoryMgt.MAL;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace InventoryMgt.DAL
{
    public class ProductContext : IProductRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public ProductContext(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<Product> GetAll()
        {
            using var db = _dbFactory.Open();
            return db.Select<Product>();
        }

        public Product GetById(int id)
        {
            using var db = _dbFactory.Open();
            return db.SingleById<Product>(id);
        }

        public void Add(Product product)
        {
            using var db = _dbFactory.Open();
            db.Insert(product);
        }

        public void Update(Product product)
        {
            using var db = _dbFactory.Open();
            db.Update(product);
        }

        public void Delete(int id)
        {
            using var db = _dbFactory.Open();
            db.DeleteById<Product>(id);
        }
    }

}
