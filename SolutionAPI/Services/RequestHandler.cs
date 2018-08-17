using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolutionAPI.Models;

namespace SolutionAPI.Services
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IDataAccess dataAccess;

        public RequestHandler(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<List<SolutionProvider>> GetSolutionProvidersForSKU(string sku)
        {
            return await dataAccess.GetSolutionProvidersForSKU(sku);
        }

        public async Task<List<User>> GetUsers(string filter, UserSearchModel option)
        {
            return await dataAccess.GetUsers(filter, option);
        }

        public async Task<User> GetUserById(int id)
        {
            return await dataAccess.GetUserById(id);
        }
        public async Task<List<Group>> GetGroups(string filter, GroupSearchModel option)
        {
            return await dataAccess.GetGroups(filter, option);
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await dataAccess.GetGroupById(id);
        }
    }
}
