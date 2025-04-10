using crud_curotec.Domain.Entities;

namespace crud_curotec.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<List<Product>> CreateBatchAsync(List<Product> products);
    }
}
