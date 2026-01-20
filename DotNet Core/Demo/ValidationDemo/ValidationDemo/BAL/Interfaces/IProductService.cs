using ValidationDemo.DTOs;

namespace ValidationDemo.BAL.Interfaces
{
    public interface IProductService
    {
        void CreateProduct(CreateProductDto dto);
    }

}
