using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moq;
using DirectoryServiceAPI.Services;
using DirectoryServiceAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DirectoryServiceAPI.Models;

namespace UnitTest.Controllers
{
    [TestFixture]
    public class UserResourcesTest
    {
        [Test]
        public void UsersNotFound204()
        {
            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetUsers(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>())).ThrowsAsync(new NotFoundException());
            
            DirectoryController cn = new DirectoryController(v.Object);
            IActionResult response = cn.GetUsers(null).Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status204NoContent, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void InternalServerError()
        {
            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetUsers(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            DirectoryController cn = new DirectoryController(v.Object);
            IActionResult response = cn.GetUsers(null).Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void ValidUsers()
        {
            var expected = new UserResources();
            expected.resources = new List<User>() { new User() { id = "1", email = "abc@abc.com", givenName = "abc", surname = "abc", userPrincipalName = "abc" }, new User() { id = "2", email = "abc@abc.com", givenName = "abc", surname = "abc", userPrincipalName = "abc" } };

            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetUsers(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>())).ReturnsAsync(expected);

            DirectoryController cn = new DirectoryController(v.Object);
            OkObjectResult response = cn.GetUsers(null).Result as OkObjectResult;
            expected.Should().BeEquivalentTo(response.Value);
        }

        [Test]
        public void BadRequest()
        {
            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetUsers(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>())).ThrowsAsync(new BadRequestException());
            
            DirectoryController cn = new DirectoryController(v.Object);
            IActionResult response = cn.GetUsers("test eq saket").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status400BadRequest, ((StatusCodeResult)response).StatusCode);
        }
    }
}
