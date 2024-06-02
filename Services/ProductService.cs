using Shop_cake.Data.Models;
using Shop_cake.Data.Repositories;

namespace Shop_cake.Business.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            // Проверяем, что продукт не пустой
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            // Проверяем уникальность ID продукта перед добавлением
            var existingProduct = await _productRepository.GetProductByIdAsync(product.Id);
            if (existingProduct != null)
            {
                throw new InvalidOperationException($"Товар с номером {product.Id} уже существует.");
            }

            await _productRepository.AddProductAsync(product);
        }
    }
}