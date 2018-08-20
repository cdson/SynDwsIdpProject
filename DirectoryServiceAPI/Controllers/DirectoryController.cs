using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;


namespace DirectoryServiceAPI.Controllers
{
    [Produces("application/json")]
    [Route("Directory")]
    public class DirectoryController : Controller
    {
        internal static class RouteNames
        {
            public const string Users = nameof(Users);
            public const string UserById = nameof(UserById);
            public const string Groups = nameof(Groups);
            public const string GroupById = nameof(GroupById);
        }

        private readonly IADFactory factory;

        public DirectoryController(IADFactory factory)
        {
            this.factory = factory;
        }

        //directory/users/{id}
        [Route("users/{id}")]
        [HttpGet("{id}", Name = RouteNames.UserById)]
        public async Task<IActionResult> getUser(string id)
        {
            User objUser = null;
            try
            {
                IADHandler azureObj = factory.GetIAM();
                objUser = await azureObj.getUser(id);

                if (objUser == null)
                {
                    Log.Warning("No user found.");
                    throw new UserNotFoundException(id);
                }
                return Ok(objUser);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                throw new UserNotFoundException(id);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUserById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/users
        [Route("users/{filter?}/{startIndex?}/{count?}/{sortBy?}")]
        [HttpGet(Name = RouteNames.Users)]
        public async Task<IActionResult> getUsers(string filter = null, int? startIndex = null, int? count = null, string sortBy = null)
        {
            UserResources objUsers = null;
            try
            {
                IADHandler azureObj = factory.GetIAM();
                objUsers = await azureObj.getUsers(filter, startIndex, count, sortBy);

                if (objUsers == null)
                {
                    Log.Warning("No users found.");
                    throw new UserNotFoundException();
                }
                return Ok(objUsers);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUsers message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/groups/{id}
        [Route("groups/{id}")]
        [HttpGet("{id}", Name = RouteNames.GroupById)]
        public async Task<IActionResult> getGroup(string id)
        {
            Group objGroup = null;
            try
            {
                IADHandler azureObj = factory.GetIAM();
                objGroup = await azureObj.getGroup(id);

                if (objGroup == null)
                {
                    Log.Warning("No group found.");
                    throw new GroupNotFoundException(id);
                }
                return Ok(objGroup);
            }
            catch (GroupNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                throw new GroupNotFoundException(id);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroupById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/groups
        [Route("groups/{filter?}/{startIndex?}/{count?}/{sortBy?}")]
        [HttpGet(Name = RouteNames.Groups)]
        public async Task<IActionResult> getGroup(string filter = null, int? startIndex = null, int? count = null, string sortBy = null)
        {
            GroupResources objGroups = null;
            try
            {
                IADHandler azureObj = factory.GetIAM();
                objGroups = await azureObj.getGroups(filter, startIndex, count, sortBy);

                if (objGroups == null)
                {
                    Log.Warning("No groups found.");
                    throw new GroupNotFoundException();
                }
                return Ok(objGroups);
            }
            catch (GroupNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroups message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}