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
    public class GroupTest
    {
        [Test]
        public void GroupNotFound404()
        {
            Mock<IADHandler> v = new Mock<IADHandler>();
            v.Setup(k => k.GetGroup(It.IsAny<string>())).ThrowsAsync(new GroupNotFoundException("123abc"));

            Mock<IADFactory> mockFactory = new Mock<IADFactory>();
            mockFactory.Setup(k => k.GetIAM()).Returns(v.Object);

            DirectoryController cn = new DirectoryController(mockFactory.Object);
            IActionResult response = cn.GetGroup("123abc").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status404NotFound, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void InternalServerError()
        {
            Mock<IADHandler> v = new Mock<IADHandler>();
            v.Setup(k => k.GetGroup(It.IsAny<string>())).ThrowsAsync(new Exception());

            Mock<IADFactory> mockFactory = new Mock<IADFactory>();
            mockFactory.Setup(k => k.GetIAM()).Returns(v.Object);

            DirectoryController cn = new DirectoryController(mockFactory.Object);
            IActionResult response = cn.GetGroup("123abc").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void ValidGroup()
        {
            var expected = new Group() { id = "123abc", displayName = "group 1" };

            Mock<IADHandler> v = new Mock<IADHandler>();
            v.Setup(k => k.GetGroup(It.IsAny<string>())).ReturnsAsync(expected);

            Mock <IADFactory> mockFactory = new Mock<IADFactory>();
            mockFactory.Setup(k => k.GetIAM()).Returns(v.Object);

            DirectoryController cn = new DirectoryController(mockFactory.Object);
            OkObjectResult response = cn.GetGroup("123456abc").Result as OkObjectResult;
            expected.Should().BeEquivalentTo(response.Value);
        }

    }
}
