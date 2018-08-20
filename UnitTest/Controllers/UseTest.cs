using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using DirectoryServiceAPI.Services;
using DirectoryServiceAPI.Controllers;

namespace UnitTest.Controllers
{
    [TestFixture]
    public class DirectoryControllersTest
    {
        //[Test]
        //public async Task UserNotFound404()
        //{


        //    Mock<IADHandler> v = new Mock<IADHandler>();
        //    v.Setup(k => k.getUser(It.IsAny<string>())).ThrowsAsync(new UserNotFoundException("123abc"));

        //    Mock<IADFactory> mockFactory = new Mock<IADFactory>();
        //    mockFactory.Setup(k => k.GetIAM()).Returns(v.Object);

        //    DirectoryController cn = new DirectoryController(mockFactory.Object);
        //    cn.getUser("abc");
        //    expected.Should().BeEquivalentTo(result.Value);

        //}
    }
}
