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
        private readonly IGraphService graphService;
        public AzureADHandler(IGraphService graphService)
        {
            this.graphService = graphService;
        }

        public async Task<User> GetUser(string id)
        {
            try
            {
                User objUser = new User();

                objUser = await graphService.GetUser(id);

                return objUser;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<UserResources> GetUsers(string filter, int? startIndex, int? count, string sortBy)
        {
            try
            {
                UserResources users = new UserResources();

                users = await graphService.GetUsers(filter, startIndex, count, sortBy);

                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
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
