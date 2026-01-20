using ValidationDemo.BAL.Interfaces;
using ValidationDemo.DAL.Interfaces;
using ValidationDemo.DTOs;
using ValidationDemo.MAL;

namespace ValidationDemo.BAL
{
    public class ProductHandler : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public void CreateProduct(CreateProductDto dto)
        {
         // BUSINESS VALIDATION
            if (dto.Price < 10)
            {
                throw new Exception("Product price must be at least 10");
            }

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Quantity = dto.Quantity,
                CreatedOn = DateTime.UtcNow
            };

            _repository.Add(product);
        }
    }

}
