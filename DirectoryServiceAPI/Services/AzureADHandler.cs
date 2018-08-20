using DirectoryServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Models
{
    public class AzureADHandler : IRequestHandler //Concrete Product , similar such product classes can be added
    {
        public async Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<Group> GetGroupById(int id)
        {
            throw new NotImplementedException();
        }


        //remove me later
        public Task<List<SolutionProvider>> GetSolutionProvidersForSKU(string sku)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsers(string filter, int? startIndex, int? count, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> GetGroups(string filter, int? startIndex, int? count, string sortBy)
        {
            throw new NotImplementedException();
        }
    }
}
