using Domain.Dtos.ProductDto;
using Domain.Dtos.UserDto;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ReadProductDto> GetAll();

        ReadProductDto GetById(Guid id);

        ReadProductDto GetByCode(string code);

        ReadProductDto Insert(CreateProductDto req);

        ReadProductDto Update(UpdateProductDto req);

        bool Delete(Guid id);
    }
}
