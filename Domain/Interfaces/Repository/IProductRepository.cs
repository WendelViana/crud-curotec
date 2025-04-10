using crud_curotec.Domain.Entities;

namespace crud_curotec.Domain.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<List<Product>> AddRangeAsync(List<Product> products);
    }
}
