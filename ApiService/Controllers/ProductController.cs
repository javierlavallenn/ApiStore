using Domain.Dtos.ProductDto;
using Domain.Dtos.UserDto;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service) => _service = service;

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<ReadProductDto> product = _service.GetAll();

                if (product == null)
                    return NotFound("Products not found");

                return Ok(product);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                ReadProductDto product = _service.GetById(id);

                if (product is null)
                    return NotFound($"Error, the product with the id: {id}, was nnot found");

                return Ok(product);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert(CreateProductDto req)
        {
            try
            {
                ReadProductDto newProduct = _service.Insert(req);

                return Ok(newProduct);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(UpdateProductDto req)
        {
            try
            {
                if (_service.GetById(req.Id) is null)
                    return NotFound($"Error, product whit id {req.Id}, not found");

                ReadProductDto updateProduct = _service.Update(req);

                return Ok(updateProduct);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var result = _service.Delete(id);

                if (!result)
                    return StatusCode(500);

                return Ok(true);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
