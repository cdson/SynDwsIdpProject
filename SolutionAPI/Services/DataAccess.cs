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
    }
}
