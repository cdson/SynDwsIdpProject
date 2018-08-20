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
        public async Task<User> getUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserResources> getUsers(string filter, int? startIndex, int? count, string sortBy)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> getGroup(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GroupResources> getGroups(string filter, int? startIndex, int? count, string sortBy)
        {
            throw new NotImplementedException();
        }
    }
}
