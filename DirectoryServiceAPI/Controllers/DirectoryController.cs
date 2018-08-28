﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;
//using Microsoft.AspNetCore.Authorization;

namespace DirectoryServiceAPI.Controllers
{
    [Produces("application/json")]
    [Route("Directory")]
    //[Authorize]
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


        [HttpGet("users/{id}", Name = RouteNames.UserById)]
        public async Task<IActionResult> GetUser(string id)
        {
            User objUser = null;
            try
            {
                if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }

                IADHandler adHandler = factory.GetIAM();
                objUser = await adHandler.GetUser(id);
                return Ok(objUser);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUserById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //TODO//filter should be optional parameter. need a fix for swagger.
        //[HttpGet("users/{filter}/{startIndex?}/{count?}/{sortBy?}", Name = RouteNames.Users)]
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers(string filter, int? startIndex = null, int? count = null, string sortBy = null)
        {
            UserResources objUsers = null;
            try
            {
                IADHandler adHandler = factory.GetIAM();
                objUsers = await adHandler.GetUsers(filter, startIndex, count, sortBy);
                return Ok(objUsers);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (UserBadRequestException ex)
            {
                Log.Warning(ex, ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUsers message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("groups/{id}", Name = RouteNames.GroupById)]
        public async Task<IActionResult> GetGroup(string id)
        {
            Group objGroup = null;
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }

                IADHandler adHandler = factory.GetIAM();
                objGroup = await adHandler.GetGroup(id);
                return Ok(objGroup);
            }
            catch (GroupNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroupById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        //[HttpGet("groups/{filter?}/{startIndex?}/{count?}/{sortBy?}",Name = RouteNames.Groups)]
        [HttpGet]
        [Route("groups")]
        public async Task<IActionResult> GetGroups(string filter = null, int? startIndex = null, int? count = null, string sortBy = null)
        {
            GroupResources objGroups = null;
            try
            {
                IADHandler adHandler = factory.GetIAM();
                objGroups = await adHandler.GetGroups(filter, startIndex, count, sortBy);
                return Ok(objGroups);
            }
            catch (GroupNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (UserBadRequestException ex)
            {
                Log.Warning(ex, ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroups message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}