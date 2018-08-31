using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using DirectoryServiceAPI.Services;
using DirectoryServiceAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DirectoryServiceAPI.Models;
using FluentAssertions;

namespace UnitTest.Controllers
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void UserNotFound404()
        {
            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetUser(It.IsAny<string>())).ThrowsAsync(new NotFoundException());

            DirectoryController cn = new DirectoryController(v.Object);
            IActionResult response = cn.GetUser("123abc").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status404NotFound, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void InternalServerError()
        {
            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetUser(It.IsAny<string>())).ThrowsAsync(new Exception());

            DirectoryController cn = new DirectoryController(v.Object);
            IActionResult response = cn.GetUser("123abc").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void ValidUser()
        {
            var expected = new User() { id = "123456abc", email = "abc@abc.com", givenName = "abc", surname = "abc", userPrincipalName = "abc" };

            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetUser(It.IsAny<string>())).ReturnsAsync(expected);

            DirectoryController cn = new DirectoryController(v.Object);
            OkObjectResult response = cn.GetUser("123456abc").Result as OkObjectResult;
            expected.Should().BeEquivalentTo(response.Value);
        }

        [Test]
        public void BadRequest()
        {
            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetUser(It.IsAny<string>())).ThrowsAsync(new BadRequestException());

            DirectoryController cn = new DirectoryController(v.Object);
            IActionResult response = cn.GetUser("").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status400BadRequest, ((StatusCodeResult)response).StatusCode);
        }

    }
}
