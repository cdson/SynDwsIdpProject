using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
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
            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetGroup(It.IsAny<string>())).ThrowsAsync(new NotFoundException());
            
            DirectoryController cn = new DirectoryController(v.Object);
            IActionResult response = cn.GetGroup("123abc").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status404NotFound, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void InternalServerError()
        {
            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetGroup(It.IsAny<string>())).ThrowsAsync(new Exception());
            
            DirectoryController cn = new DirectoryController(v.Object);
            IActionResult response = cn.GetGroup("123abc").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void ValidGroup()
        {
            var expected = new Group() { id = "123abc", displayName = "group 1" };

            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetGroup(It.IsAny<string>())).ReturnsAsync(expected);
            
            DirectoryController cn = new DirectoryController(v.Object);
            OkObjectResult response = cn.GetGroup("123456abc").Result as OkObjectResult;
            expected.Should().BeEquivalentTo(response.Value);
        }

        [Test]
        public void BadRequest()
        {
            Mock<IGraphService> v = new Mock<IGraphService>();
            v.Setup(k => k.GetGroup(It.IsAny<string>())).ThrowsAsync(new BadRequestException());
            
            DirectoryController cn = new DirectoryController(v.Object);
            IActionResult response = cn.GetGroup("").Result;
            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(StatusCodes.Status400BadRequest, ((StatusCodeResult)response).StatusCode);
        }

    }
}
