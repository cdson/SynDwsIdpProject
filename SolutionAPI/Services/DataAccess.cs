/*
 * ©Copyright 2018 Dell, Inc., All Rights Reserved.
 * This material is confidential and a trade secret.  Permission to use this
 * work for any purpose must be obtained in writing from Dell, Inc.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using Serilog;
using SolutionAPI.Models;

namespace SolutionAPI.Services
{
    public class DataAccess : IDataAccess
    {
        private string ConnectionString { get; set; }
        private AppSettings AppSettings { get; set; }

        public DataAccess(DatabaseSettings dbSettings, AppSettings appSettings)
        {
            ConnectionString = dbSettings.ConnectionString;
            AppSettings = appSettings;
        }

        public Task<List<SolutionProvider>> GetSolutionProvidersForSKU(string sku)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsers(string filter, int? startIndex, int? count, string sortBy)
        {
            return Task.Run(() => MockedDataForUser.AllUsers);
        }

        public Task<User> GetUserById(int id)
        {
            var user = MockedDataForUser.AllUsers.SingleOrDefault(i => i.UserId == id);
            return Task.Run(() => user);
        }

        public Task<List<Group>> GetGroups(string filter, int? startIndex, int? count, string sortBy)
        {
            return Task.Run(() => MockedDataForGroup.AllGroups);
        }

        public Task<Group> GetGroupById(int id)
        {
            var group = MockedDataForGroup.AllGroups.SingleOrDefault(i => i.Id == id);
            return Task.Run(() => group);
        }
    }
}
