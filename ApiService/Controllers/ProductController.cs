using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models.ProductDto;
using Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                IEnumerable<Product> products = _repository.GetAll();

                if (!products.Any())
                    return Ok(Array.Empty<Product>());

                return Ok(products);
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Product product = _repository.GetById(id);

                if (product is null)
                    return NotFound($"An error has occurred,the product with the id: {id} does not exist");

                return Ok(new Result<ProductResponse>()
                {
                    Failure = false,
                    Message = "success",
                    Data = new ProductResponse()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                    }
                });
            }
            catch (Exception error)
            {

                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateProductRequest req)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Price = req.Price,
                Description = req.Description,
                Code = req.Code,
                IsPublished = req.IsPublished,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            try
            {
                bool newProduct = _repository.Insert(product);

                if (!newProduct)
                    return BadRequest("An error occurred while creating a product, please try again!");

                return Ok(new Result<ProductResponse>()
                {
                    Failure = false,
                    Message = "success",
                    Data = new ProductResponse()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                    }
                });
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpPut]
        public IActionResult Update(UpdateProductRequest req)
        {
            try
            {
                Product product = _repository.GetById(req.Id);

                if (product is null)
                    return NotFound($"An error has occurred,the product with the id: {req.Id} does not exist");

                product.Name = req.Name;
                product.Price = req.Price;
                product.IsPublished = req.IsPublished;

                bool updatedProduct = _repository.Update(product);

                if (!updatedProduct)
                    return BadRequest("An error occurred when trying to update a product, try again");

                return StatusCode(201, new Result<ProductResponse>()
                {
                    Failure = false,
                    Message = "success",
                    Data = new ProductResponse()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                    }
                });
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                bool deletedProduct = _repository.Delete(id);

                if (!deletedProduct)
                    return BadRequest("");

                return Ok(true);
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }
    }
}
