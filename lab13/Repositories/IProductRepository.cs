using lab13.Models; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab13.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
    }
}