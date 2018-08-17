using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SolutionAPI.Models;
using System.Collections.Generic;

namespace SolutionAPI.Services
{
    public interface IRequestHandler
    {
        Task<List<SolutionProvider>> GetSolutionProvidersForSKU(string sku);
        Task<List<User>> GetUsers(string filter, int? startIndex, int? count, string sortBy);
        Task<User> GetUserById(int id);
        Task<List<Group>> GetGroups(string filter, int? startIndex, int? count, string sortBy);
        Task<Group> GetGroupById(int id);
    }

    
}
