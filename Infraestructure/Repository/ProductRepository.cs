using crud_curotec.Domain.Entities;
using crud_curotec.Domain.Interfaces.Repository;
using crud_curotec.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace crud_curotec.Infraestructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await 
                _context.Products
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await 
                _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(Product updatedProduct)
        {
            var existing = await 
                _context.Products
                .FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);

            if (existing == null)
                return false;

            existing.Update(updatedProduct.Name, updatedProduct.Price);
            _context.Products.Update(existing);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existing == null)
                return false;

            _context.Products.Remove(existing);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Product>> AddRangeAsync(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
            return products;
        }

    }
}
