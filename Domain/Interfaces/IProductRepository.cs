using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(Guid id);

        bool Insert(Product product);

        bool Update(Product product);

        bool Delete(Guid id);
    }
}
