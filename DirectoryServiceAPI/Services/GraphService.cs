using DirectoryServiceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Services
{
    public class GraphService : IGraphService
    {
        private readonly IGraphClient graphClient;
        public GraphService(IGraphClient graphClient)
        {
            this.graphClient = graphClient;
        }

        public async Task<Models.User> GetUser(string id)
        {
            Models.User objUser = new Models.User();

            // Initialize the GraphServiceClient.
            GraphServiceClient client = await graphClient.GetGraphServiceClient();
            // Load user profile.
            var user = await client.Users[id].Request().GetAsync();

            //TODO//write common code tp copy proeprties of Microsoft.Graph.User to dto object
            objUser.id = user.Id;
            objUser.givenName = user.GivenName;
            objUser.surname = user.Surname;
            objUser.userPrincipalName = user.UserPrincipalName;
            objUser.email = user.Mail;


            return objUser;
        }

        public async Task<UserResources> GetUsers(string filter, int? startIndex, int? count, string sortBy)
        {
            UserResources users = new UserResources();
            users.resources = new List<Models.User>();

            // Initialize the GraphServiceClient.
            GraphServiceClient client = await graphClient.GetGraphServiceClient();

            var userList = await client.Users.Request().Filter($"{filter}").GetAsync();

            //TODO//write common code tp copy proeprties of Microsoft.Graph.User to dto object
            foreach (var user in userList)
            {
                Models.User objUser = new Models.User();

                objUser.id = user.Id;
                objUser.givenName = user.GivenName;
                objUser.surname = user.Surname;
                objUser.userPrincipalName = user.UserPrincipalName;
                objUser.email = user.Mail;

                users.resources.Add(objUser);
            }


            users.totalResults = users.resources.Count;

            return users;
        }

        public async Task<Models.Group> GetGroup(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GroupResources> GetGroups(string filter, int? startIndex, int? count, string sortBy)
        {
            throw new NotImplementedException();
        }
    }
}
