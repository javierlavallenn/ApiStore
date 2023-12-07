using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(Guid id);

        Product GetByCode(string code);

        Product? Insert(Product product);

        Product? Update(Product product);

        bool Delete(Guid id);
    }
}
