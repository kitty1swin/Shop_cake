using Microsoft.AspNetCore.Mvc;
using Shop_cake.Data;
using Shop_cake.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace cakeStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDBContext _db;

        public ProductController(AppDBContext db)
        {
            _db = db;
        }

        // GET api/product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_db.Products.ToList());
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductCreateDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Category = productDto.Category,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            if (productDto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await productDto.Image.CopyToAsync(memoryStream);
                    product.Image = memoryStream.ToArray();
                }
            }

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return File(product.Image, "image/jpeg");
        }

        // PUT api/product/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, Product updatedProduct)
        {
            var existingProduct = _db.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Category = updatedProduct.Category;
            existingProduct.Image = updatedProduct.Image;
            existingProduct.UpdateDate = DateTime.Now;

            _db.SaveChanges();

            return NoContent();
        }
    }
    public class ProductCreateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ushort? Price { get; set; }
        public string? Category { get; set; }
        public IFormFile? Image { get; set; }
    }
}