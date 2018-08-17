using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DirectoryServiceAPI.Models;
using System.Collections.Generic;

namespace DirectoryServiceAPI.Services
{
    public interface IRequestHandler
    {
        Task<List<SolutionProvider>> GetSolutionProvidersForSKU(string sku);
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<List<Group>> GetGroups();
        Task<Group> GetGroupById(int id);
    }

    
}
