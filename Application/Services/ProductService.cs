using crud_curotec.Domain.Entities;
using crud_curotec.Domain.Interfaces.Repository;
using crud_curotec.Domain.Interfaces.Services;
using FluentValidation;
using System.Collections.Concurrent;


namespace crud_curotec.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<Product> _validator;

        public ProductService(IProductRepository productRepository, IValidator<Product> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();

        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product?> CreateAsync(Product product)
        {
            var validationResult = await _validator.ValidateAsync(product);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var validationResult = await _validator.ValidateAsync(product);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<List<Product>> CreateBatchAsync(List<Product> products)
        {
            var validProducts = new ConcurrentBag<Product>();

            await Task.Run(() =>
            {
                Parallel.ForEach(products, product =>
                {
                    var validationResult = _validator.Validate(product);

                    if (validationResult.IsValid)
                        validProducts.Add(product);
                });
            });

            if (!validProducts.Any())
                return new List<Product>();

            return await _productRepository.AddRangeAsync(validProducts.ToList());
        }
    }
}
