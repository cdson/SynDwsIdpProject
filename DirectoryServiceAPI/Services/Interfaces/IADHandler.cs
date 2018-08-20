using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DirectoryServiceAPI.Models;
using System.Collections.Generic;

namespace DirectoryServiceAPI.Services
{
    public interface IADHandler
    {
        Task<User> getUser(string id);
        Task<UserResources> getUsers(string filter, int? startIndex, int? count, string sortBy);
        Task<Group> getGroup(string id);
        Task<GroupResources> getGroups(string filter, int? startIndex, int? count, string sortBy);
    }

    
}
