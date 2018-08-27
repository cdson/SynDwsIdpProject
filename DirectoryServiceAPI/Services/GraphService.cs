using DirectoryServiceAPI.Helpers;
using DirectoryServiceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using Serilog;
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
            try
            {
                Models.User objUser = new Models.User();

                // Initialize the GraphServiceClient.
                GraphServiceClient client = await graphClient.GetGraphServiceClient();

                // Load user profile.
                var user = await client.Users[id].Request().GetAsync();

                // Copy Microsoft User to DTO User
                objUser = CopyHandler.PropertyCopy(user);

                return objUser;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode.ToString().ToLower() == "notfound")
                {
                    Log.Warning("No user found.");
                    throw new UserNotFoundException(id);
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task<UserResources> GetUsers(string filter, int? startIndex, int? count, string sortBy)
        {
            try
            {
                UserResources users = new UserResources();
                users.resources = new List<Models.User>();

                // Initialize the GraphServiceClient.
                GraphServiceClient client = await graphClient.GetGraphServiceClient();

                // Load users profiles.
                var userList = await client.Users.Request().Filter($"{filter}").GetAsync();

                // Copy Microsoft User to DTO User
                foreach (var user in userList)
                {
                    var objUser = CopyHandler.PropertyCopy(user);
                    users.resources.Add(objUser);
                }
                users.totalResults = users.resources.Count;


                if (users.totalResults == 0)
                {
                    Log.Warning("No user found.");
                    throw new UserNotFoundException();
                }

                return users;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode.ToString().ToLower() == "badrequest")
                {
                    Log.Warning("bad request.");
                    throw new UserBadRequestException();
                }
                else if (ex.StatusCode.ToString().ToLower() == "notfound")
                {
                    Log.Warning("No user found.");
                    throw new UserNotFoundException();
                }
                else
                {
                    throw new Exception();
                }
            }
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
