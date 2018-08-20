using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DirectoryServiceAPI.Models;
using System.Collections.Generic;

namespace DirectoryServiceAPI.Services
{
    public interface IADHandler
    {
        Task<User> GetUser(string id);
        Task<UserResources> GetUsers(string filter, int? startIndex, int? count, string sortBy);
        Task<Group> GetGroup(string id);
        Task<GroupResources> GetGroups(string filter, int? startIndex, int? count, string sortBy);
    }

    
}
