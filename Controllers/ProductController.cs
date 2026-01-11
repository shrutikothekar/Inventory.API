using Inventory.API.Models;
using Inventory.API.Helpers;
using Inventory.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Inventory.API.Services;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        public readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
           _repo = repo;
        }

        // POST: api/products
        [HttpPost("bulk-upload")]
        public async Task<IActionResult> BulkUpload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required");

            var dataTable = CsvParser.ParseProductsCsv(file.OpenReadStream()); //here we use static method os static class directly withougt injecting just use namespace for that

            await _repo.BulkInsertProductsAsync(dataTable);

            return Ok("Bulk upload successful");
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var data = await _repo.GetProductsAsync();
            return Ok(data);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await _repo.AddProductAsync(product);
            return Ok("Product added successfully");
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(long id, Product product)
        {
            product.ProductId = id;
            await _repo.UpdateProductAsync(product);
            return Ok("Product updated successfully");
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            await _repo.DeleteProductAsync(id);
            return Ok("Product deleted successfully");
        }
    }
}
