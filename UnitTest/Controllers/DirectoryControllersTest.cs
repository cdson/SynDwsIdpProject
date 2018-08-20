using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace UnitTest.Controllers
{
    //[TestFixture]
    public class DirectoryControllersTest
    {
        //SolutionController controller = null;

        //[SetUp]
        //public void Initialize()
        //{
        //    List<SolutionProvider> objSolutionProvider = new List<SolutionProvider>()
        //    {
        //        new SolutionProvider()
        //        {
        //            SolutionPorviderId = "1",
        //            Name = "Name",
        //            Description ="Description",
        //            CreateAndSubscriptionURL="CreateAndSubscriptionURL",
        //            StatusURL = "StatusURL",
        //            SubscriptionURL = "SubscriptionURL"
        //        }
        //    };

        //    Mock<IRequestHandler> handler = new Mock<IRequestHandler>();
        //    handler.Setup(k => k.GetSolutionProvidersForSKU(It.IsAny<string>())).ReturnsAsync(objSolutionProvider);

        //    controller = new SolutionController(handler.Object);
        //}
        //[Test]
        //public async Task Valid_GetSolutionProvidersForSku()
        //{
        //    OkObjectResult result = await controller.GetSolutionProvidersForSku(sku: "1022097D-54EB") as OkObjectResult;

        //    var expected = new List<SolutionProvider>()
        //    {
        //        new SolutionProvider()
        //        {
        //            SolutionPorviderId = "1",
        //            Name = "Name",
        //            Description ="Description",
        //            CreateAndSubscriptionURL="CreateAndSubscriptionURL",
        //            StatusURL = "StatusURL",
        //            SubscriptionURL = "SubscriptionURL"
        //        }
        //    };
        //    //Todo : assert below correctly.
        //    //expected.Should().BeEquivalentTo(result.Value);
        //}
    }
}
