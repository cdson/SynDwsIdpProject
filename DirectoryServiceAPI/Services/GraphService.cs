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

                // Copy Microsoft-Graph User to DTO User
                objUser = CopyHandler.UserProperty(user);

                return objUser;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode.ToString().ToLower().Equals("notfound")) ////"error":{"code": "Request_ResourceNotFound"}////
                {
                    Log.Warning(ex.Message);
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
                    var objUser = CopyHandler.UserProperty(user);
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
                if (ex.StatusCode.ToString().ToLower().Equals("badrequest"))
                {
                    Log.Warning(ex.Message);
                    throw new UserBadRequestException();
                }
                else if (ex.StatusCode.ToString().ToLower().Equals("notfound"))
                {
                    Log.Warning(ex.Message);
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
            try
            {
                Models.Group objGroup = new Models.Group();

                // Initialize the GraphServiceClient.
                GraphServiceClient client = await graphClient.GetGraphServiceClient();

                // Load group profile.
                var group = await client.Groups[id].Request().GetAsync();

                // Copy Microsoft-Graph Group to DTO Group
                objGroup = CopyHandler.GroupProperty(group);

                return objGroup;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode.ToString().ToLower().Equals("badrequest")) ////"error":{"code": "Request_BadRequest"}////
                {
                    Log.Warning(ex.Message);
                    throw new GroupNotFoundException(id);
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task<GroupResources> GetGroups(string filter, int? startIndex, int? count, string sortBy)
        {
            try
            {
                GroupResources groups = new GroupResources();
                groups.resources = new List<Models.Group>();

                // Initialize the GraphServiceClient.
                GraphServiceClient client = await graphClient.GetGraphServiceClient();

                // Load groups profiles.
                var groupList = await client.Groups.Request().Filter($"{filter}").GetAsync();

                // Copy Microsoft-Graph Group to DTO Group
                foreach (var group in groupList)
                {
                    var objGroup = CopyHandler.GroupProperty(group);
                    groups.resources.Add(objGroup);
                }
                groups.totalResults = groups.resources.Count;


                if (groups.totalResults == 0)
                {
                    Log.Warning("No group found.");
                    throw new GroupNotFoundException();
                }

                return groups;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode.ToString().ToLower().Equals("badrequest"))
                {
                    Log.Warning(ex.Message);
                    throw new GroupBadRequestException();
                }
                else if (ex.StatusCode.ToString().ToLower().Equals("notfound"))
                {
                    Log.Warning(ex.Message);
                    throw new GroupNotFoundException();
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
