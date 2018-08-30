using DirectoryServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Services
{
    public interface IMicrosoftGraphService
    {
        Task<User> GetUser(string id);
        Task<UserResources> GetUsers(string filter, int? startIndex, int? count, string sortBy);
        Task<Group> GetGroup(string id);
        Task<GroupResources> GetGroups(string filter, int? startIndex, int? count, string sortBy);
    }
}
