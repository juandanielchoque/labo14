// Repositories/ProductRepository.cs
using lab13.Models; 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab13.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly LinqexampleContext _context;

        public ProductRepository(LinqexampleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}