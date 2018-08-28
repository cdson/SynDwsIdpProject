using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Net.Http;
using DirectoryServiceAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DirectoryServiceAPI.Services;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using DirectoryServiceAPI.Models;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace ServiceTest.DirectoryManagement
{
    [TestFixture]
    public class DirectoryManagementTest
    {

        #region GetUser

        [Test]
        public async Task ValidUser()
        {
            User expected = Helper.CreateUser();
            HttpResponseMessage response = await Startup.Client.GetAsync("directory/users/"+expected.id);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(res);
            Assert.AreEqual(expected.givenName, user.givenName);
        }

        [Test]
        public async Task UserNotFound404()
        {
            HttpResponseMessage Res = await Startup.Client.GetAsync("directory/users/1");
            Assert.AreEqual(HttpStatusCode.NotFound, Res.StatusCode);
        }

        #endregion

        #region GetUsers

        [Test]
        public async Task ValidUsers()
        {
            UserResources expected = Helper.CreateUserResources();
            User expectedUser = expected.resources.Where(x => x.id == "8c09a9d1-f1a2-463b-bd0f-b10c51c437d0").SingleOrDefault();

            HttpResponseMessage response = await Startup.Client.GetAsync("directory/users");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var userRes = JsonConvert.DeserializeObject<UserResources>(res);
            User resultUser = userRes.resources.Where(x => x.id == "8c09a9d1-f1a2-463b-bd0f-b10c51c437d0").SingleOrDefault();

            resultUser.Should().BeEquivalentTo(expectedUser);
        }


        [Test]
        public async Task Filter_NameEquals_Users()
        {
            UserResources expected = Helper.CreateUserResources();
            User expectedUser = expected.resources.Where(x => x.id == "8c09a9d1-f1a2-463b-bd0f-b10c51c437d0").SingleOrDefault();

            HttpResponseMessage response = await Startup.Client.GetAsync("directory/users?filter=givenName eq 'saket'");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var userRes = JsonConvert.DeserializeObject<UserResources>(res);
            User resultUser = userRes.resources.Where(x => x.id == "8c09a9d1-f1a2-463b-bd0f-b10c51c437d0").SingleOrDefault();

            resultUser.Should().BeEquivalentTo(expectedUser);
        }

        [Test]
        public async Task Filter_NameStartsWith_Users()
        {
            UserResources expected = Helper.CreateUserResources();
            User expectedUser = expected.resources.Where(x => x.id == "8c09a9d1-f1a2-463b-bd0f-b10c51c437d0").SingleOrDefault();

            HttpResponseMessage response = await Startup.Client.GetAsync("directory/users?filter=startswith(givenName,'S')");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var userRes = JsonConvert.DeserializeObject<UserResources>(res);
            User resultUser = userRes.resources.Where(x => x.id == "8c09a9d1-f1a2-463b-bd0f-b10c51c437d0").SingleOrDefault();

            resultUser.Should().BeEquivalentTo(expectedUser);
        }

        [Test]
        public async Task BadRequest_Users()
        {
            HttpResponseMessage response = await Startup.Client.GetAsync("directory/users?filter=startswith(test,'S')");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task NoContent_Users()
        {
            HttpResponseMessage response = await Startup.Client.GetAsync("directory/users?filter=givenName eq 'test'");
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

    }
}
