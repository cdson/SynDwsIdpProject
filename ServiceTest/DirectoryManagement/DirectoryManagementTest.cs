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
        public async Task FilterNameEqualsUsers()
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
        public async Task FilterNameStartsWithUsers()
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
        public async Task BadRequestUsers()
        {
            HttpResponseMessage response = await Startup.Client.GetAsync("directory/users?filter=startswith(test,'S')");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task NoContentUsers()
        {
            HttpResponseMessage response = await Startup.Client.GetAsync("directory/users?filter=givenName eq 'test'");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var userRes = JsonConvert.DeserializeObject<UserResources>(res);
            Assert.AreEqual(0, userRes.totalResults);
        }

        #endregion

        #region GetGroup

        [Test]
        public async Task ValidGroup()
        {
            Group expected = Helper.CreateGroup();
            HttpResponseMessage response = await Startup.Client.GetAsync("directory/groups/" + expected.id);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var group = JsonConvert.DeserializeObject<Group>(res);
            Assert.AreEqual(expected.displayName, group.displayName);
        }

        [Test]
        public async Task GroupNotFound404()
        {
            HttpResponseMessage Res = await Startup.Client.GetAsync("directory/groups/1");
            Assert.AreEqual(HttpStatusCode.NotFound, Res.StatusCode);
        }

        #endregion

        #region GetGroups

        [Test]
        public async Task ValidGroups()
        {
            GroupResources expected = Helper.CreateGroupResources();
            Group expectedGroup= expected.resources.Where(x => x.id == "722fd891-fe44-4bd1-b529-963f573ec969").SingleOrDefault();

            HttpResponseMessage response = await Startup.Client.GetAsync("directory/groups");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var grpRes = JsonConvert.DeserializeObject<GroupResources>(res);
            Group resultGroup = grpRes.resources.Where(x => x.id == "722fd891-fe44-4bd1-b529-963f573ec969").SingleOrDefault();

            resultGroup.Should().BeEquivalentTo(expectedGroup);
        }


        [Test]
        public async Task FilterNameEqualsGroups()
        {
            GroupResources expected = Helper.CreateGroupResources();
            Group expectedGroup = expected.resources.Where(x => x.id == "722fd891-fe44-4bd1-b529-963f573ec969").SingleOrDefault();

            HttpResponseMessage response = await Startup.Client.GetAsync("directory/groups?filter=displayName eq 'TestGroup1'");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var grpRes = JsonConvert.DeserializeObject<GroupResources>(res);
            Group resultGroup = grpRes.resources.Where(x => x.id == "722fd891-fe44-4bd1-b529-963f573ec969").SingleOrDefault();

            resultGroup.Should().BeEquivalentTo(expectedGroup);
        }

        [Test]
        public async Task FilterNameStartsWithGroups()
        {
            GroupResources expected = Helper.CreateGroupResources();
            Group expectedGroup = expected.resources.Where(x => x.id == "722fd891-fe44-4bd1-b529-963f573ec969").SingleOrDefault();

            HttpResponseMessage response = await Startup.Client.GetAsync("directory/groups?filter=startswith(displayName,'T')");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var grpRes = JsonConvert.DeserializeObject<GroupResources>(res);
            Group resultGroup = grpRes.resources.Where(x => x.id == "722fd891-fe44-4bd1-b529-963f573ec969").SingleOrDefault();

            resultGroup.Should().BeEquivalentTo(expectedGroup);
        }

        [Test]
        public async Task BadRequestGroups()
        {
            HttpResponseMessage response = await Startup.Client.GetAsync("directory/groups?filter=startswith(test,'S')");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task NoContentGroups()
        {
            HttpResponseMessage response = await Startup.Client.GetAsync("directory/groups?filter=displayName eq 'test'");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            var grpRes = JsonConvert.DeserializeObject<GroupResources>(res);
            Assert.AreEqual(0, grpRes.totalResults);
        }

        #endregion

    }
}
