using lab13.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab13.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClientsWithOrdersAsync();
    }
}