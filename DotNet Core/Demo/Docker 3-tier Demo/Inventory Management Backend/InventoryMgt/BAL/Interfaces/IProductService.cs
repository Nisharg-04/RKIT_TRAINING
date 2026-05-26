using InventoryMgt.MAL;

namespace InventoryMgt.BAL.Interfaces
{
    public interface IProductService
    {
        public List<Product> GetAll();
        Product Get(int id);
        public void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
