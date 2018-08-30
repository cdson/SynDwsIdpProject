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
    public class GroupResourcesTest
    {
        [Test]
        public void GroupsNotFound204()
        {
            Mock<IADHandler> v = new Mock<IADHandler>();
            v.Setup(k => k.GetGroups(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>())).ThrowsAsync(new NotFoundException());

            Mock<IADFactory> mockFactory = new Mock<IADFactory>();
            mockFactory.Setup(k => k.GetIAM()).Returns(v.Object);

            DirectoryController cn = new DirectoryController(mockFactory.Object);
            IActionResult response = cn.GetGroups().Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status204NoContent, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void InternalServerError()
        {
            Mock<IADHandler> v = new Mock<IADHandler>();
            v.Setup(k => k.GetGroups(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            Mock<IADFactory> mockFactory = new Mock<IADFactory>();
            mockFactory.Setup(k => k.GetIAM()).Returns(v.Object);

            DirectoryController cn = new DirectoryController(mockFactory.Object);
            IActionResult response = cn.GetGroups().Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void ValidGroups()
        {
            var expected = new GroupResources();
            expected.resources = new List<Group>() { new Group() { id = "1", displayName = "Test group 1" }, new Group() { id = "2", displayName = "Test group 2" } };

            Mock<IADHandler> v = new Mock<IADHandler>();
            v.Setup(k => k.GetGroups(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>())).ReturnsAsync(expected);

            Mock<IADFactory> mockFactory = new Mock<IADFactory>();
            mockFactory.Setup(k => k.GetIAM()).Returns(v.Object);

            DirectoryController cn = new DirectoryController(mockFactory.Object);
            OkObjectResult response = cn.GetGroups().Result as OkObjectResult;
            expected.Should().BeEquivalentTo(response.Value);
        }

        [Test]
        public void BadRequest()
        {
            Mock<IADHandler> v = new Mock<IADHandler>();
            v.Setup(k => k.GetGroups(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>())).ThrowsAsync(new BadRequestException());

            Mock<IADFactory> mockFactory = new Mock<IADFactory>();
            mockFactory.Setup(k => k.GetIAM()).Returns(v.Object);

            DirectoryController cn = new DirectoryController(mockFactory.Object);
            IActionResult response = cn.GetGroups("test eq group1").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status400BadRequest, ((StatusCodeResult)response).StatusCode);
        }
    }
}
