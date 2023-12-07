using Domain.Dtos.ProductDto;
using Domain.Dtos.UserDto;
using Domain.Entities;
using Domain.Interfaces;

namespace ApiService.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ReadProductDto> GetAll()
        {
            IEnumerable<Product> products = _repository.GetAll();

            if (!products.Any())
            {
                return Array.Empty<ReadProductDto>();
            }

            List<ReadProductDto> results = new List<ReadProductDto>();

            foreach (Product product in products)
            {
                results.Add(new ReadProductDto
                {
                    Code = product.Code,
                    Name = product.Name,
                    IsPublished = product.IsPublished,
                });
            }

            return results;
        }

        public ReadProductDto GetByCode(string code)
        {
            Product product = _repository.GetByCode(code);

            if (product is null)
            {
                return null!;
            }

            ReadProductDto result = new()
            {
                Code = product.Code,
                Name = product.Name,
                IsPublished = product.IsPublished,
            };

            return result;
        }

        public ReadProductDto GetById(Guid id)
        {
            Product product = _repository.GetById(id);

            if (product is null)
            {
                return null!;
            }

            ReadProductDto result = new()
            {
                Code = product.Code,
                Name = product.Name,
                IsPublished = product.IsPublished,
            };

            return result;
        }

        public ReadProductDto Insert(CreateProductDto req)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Code = req.Code,
                Name = req.Name,
                IsPublished = req.IsPublished,
                Price = req.Price,
                Description = req.Description,
            };

            Product entity = _repository.Insert(product)!;

            if (entity is null)
            {
                return null!;
            }

            ReadProductDto result = new()
            {
                Code = entity.Code,
                Name = entity.Name,
                IsPublished = entity.IsPublished,
            };

            return result;
        }

        public ReadProductDto Update(UpdateProductDto req)
        {
            Product product = _repository.GetById(req.Id);

            if (product is null)
            {
                return null!;
            }

            product.IsPublished = req.IsPublished;
            product.Price = req.Price;
            product.Name = req.Name;

            Product productUpdated = _repository.Update(product)!;

            if (productUpdated is null)
            {
                return null!;
            }

            ReadProductDto result = new();

            result.Code = productUpdated.Code;
            result.Name = productUpdated.Name;
            result.IsPublished = productUpdated.IsPublished;

            return result;
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}
