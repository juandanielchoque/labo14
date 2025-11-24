using lab13.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab13.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly LinqexampleContext _context;

        public ClientRepository(LinqexampleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetClientsWithOrdersAsync()
        {
            return await _context.Clients
                .Include(client => client.Orders) 
                .ToListAsync();
        }
    }
}