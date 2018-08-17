/*
 * ©Copyright 2018 Dell, Inc., All Rights Reserved.
 * This material is confidential and a trade secret.  Permission to use this
 * work for any purpose must be obtained in writing from Dell, Inc.
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using DirectoryServiceAPI.Models;

namespace DirectoryServiceAPI.Services
{
    public interface IDataAccess
    {
        /// <summary>
        /// Retrieves the list of Solution Providers for the given SKU.
        /// </summary>
        /// <param name="sku"></param>
        /// <returns>List<SolutionProvider></returns>
        Task<List<SolutionProvider>> GetSolutionProvidersForSKU(string sku);
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<List<Group>> GetGroups();
        Task<Group> GetGroupById(int id);

    }
}
