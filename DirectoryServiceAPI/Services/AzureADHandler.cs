using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Services
{
    public class AzureADHandler : IADHandler //Concrete Product , similar such product classes can be added
    {
        public async Task<User> GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserResources> GetUsers(string filter, int? startIndex, int? count, string sortBy)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> GetGroup(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GroupResources> GetGroups(string filter, int? startIndex, int? count, string sortBy)
        {
            throw new NotImplementedException();
        }
    }
}
