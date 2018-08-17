using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SolutionAPI.Models;
using SolutionAPI.Services;

namespace SolutionAPI.Controllers
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

        private readonly IRequestHandler requestHandler;

        public DirectoryController(IRequestHandler requestHandler)
        {
            this.requestHandler = requestHandler;
        }

        //directory/users/{id}
        [Route("users/{id}")]
        [HttpGet("{id}", Name = RouteNames.UserById)]
        public async Task<IActionResult> GetUserById(int id)
        {
            User objUser = null;
            try
            {
                objUser = await requestHandler.GetUserById(id);

                if (objUser == null)
                {
                    Log.Warning("No user found.");
                    throw new UserNotFoundException(id.ToString());
                }
                return Ok(objUser);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUserById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/users
        [Route("users/{filter?}/{option?}")]
        [HttpGet(Name = RouteNames.Users)]
        public async Task<IActionResult> GetUsers(string filter = null, UserSearchModel option = null)
        {
            List<User> objUsers = null;
            try
            {
                objUsers = await requestHandler.GetUsers(filter, option);

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
        public async Task<IActionResult> GetGroupById(int id)
        {
            Group objGroup = null;
            try
            {
                objGroup = await requestHandler.GetGroupById(id);

                if (objGroup == null)
                {
                    Log.Warning("No group found.");
                    throw new GroupNotFoundException(id.ToString());
                }
                return Ok(objGroup);
            }
            catch (GroupNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroupById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/groups
        [Route("groups/{filter?}/{option?}")]
        [HttpGet(Name = RouteNames.Groups)]
        public async Task<IActionResult> GetGroups(string filter = null, GroupSearchModel option = null)
        {
            List<Group> objGroups = null;
            try
            {
                objGroups = await requestHandler.GetGroups(filter, option);

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