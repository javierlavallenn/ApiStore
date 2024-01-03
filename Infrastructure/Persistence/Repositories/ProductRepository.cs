using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) => _context = context;


        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(Guid id)
        {
            return _context.Products.Where(x => x.Id == id).FirstOrDefault()!;
        }

        public bool Insert(Product product)
        {
            EntityEntry result = _context.Products.Add(product);

            if (result.State != EntityState.Added)
            {
                return false;
            }

            _context.SaveChanges();

            return true;
        }

        public bool Update(Product product)
        {

            EntityEntry result = _context.Products.Update(product);

            if (result.State != EntityState.Modified)
            {
                return false;
            }

            _context.SaveChanges();

            return true;
        }

        public bool Delete(Guid id)
        {
            Product product = GetById(id);

            EntityEntry result = _context.Products.Remove(product);

            if (result.State != EntityState.Deleted)
            {
                return false;
            }

            _context.SaveChanges();

            return true;
        }
    }
}
