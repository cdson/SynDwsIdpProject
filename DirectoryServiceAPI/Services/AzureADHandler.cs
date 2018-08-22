using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Services
{
    public class AzureADHandler : IADHandler //Concrete Product , similar such product classes can be added
    {
        private readonly IGraphSdkHelper graphSdkHelper;

        public AzureADHandler(IGraphSdkHelper graphSdkHelper)
        {
            this.graphSdkHelper = graphSdkHelper;
        }

        public async Task<User> GetUser(string id)
        {
            User objUser = new User();

            // Initialize the GraphServiceClient.
            var graphClient = this.graphSdkHelper.GetAuthenticatedClient(id);
            var result = await GraphService.GetUserJsonById(graphClient, id);
            objUser = JsonConvert.DeserializeObject<User>(result);

            return objUser;
            //throw new NotImplementedException();
        }

        public async Task<UserResources> GetUsers(string filter, int? startIndex, int? count, string sortBy, string userId)
        {
            UserResources users = new UserResources();
            users.resources = new List<User>();

            // Initialize the GraphServiceClient.
            var graphClient = this.graphSdkHelper.GetAuthenticatedClient(userId);
            var result = await GraphService.GetAllUsersJson(graphClient, filter, startIndex, count, sortBy);
            users.resources = JsonConvert.DeserializeObject<List<User>>(result);

            return users;
            //throw new NotImplementedException();
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
