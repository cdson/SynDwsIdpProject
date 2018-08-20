using SolutionAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionAPI.Models
{
    public class AzureADHandler : IRequestHandler //Concrete Product , similar such product classes can be added
    {
        public async Task<List<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Group>> GetGroups()
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
    }
}
