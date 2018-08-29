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
            User objUser = new User();
            objUser = await graphService.GetUser(id);
            return objUser;
        }

        public async Task<UserResources> GetUsers(string filter, int? startIndex, int? count, string sortBy)
        {
            UserResources users = new UserResources();
            users = await graphService.GetUsers(filter, startIndex, count, sortBy);
            return users;
        }

        public async Task<Group> GetGroup(string id)
        {
            Group objGroup = new Group();
            objGroup = await graphService.GetGroup(id);
            return objGroup;
        }

        public async Task<GroupResources> GetGroups(string filter, int? startIndex, int? count, string sortBy)
        {
            GroupResources groups = new GroupResources();
            groups = await graphService.GetGroups(filter, startIndex, count, sortBy);
            return groups;
        }
    }
}
