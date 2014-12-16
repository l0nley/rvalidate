using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using Moq;
using NUnit.Framework;
using RValidate;

namespace RValidationUnit
{
    [TestFixture]
    public class RValidateAttributeTests
    {
        [Test]
        public void TestValidationPerformedPositive()
        {
            var attr = new RValidateAttribute();
            var actionContext = CreateContext();
            actionContext.ActionArguments.Add(
                "model",
                new SimpleModel
                {
                    Key = 1234,
                    FirstName = "Uladzimir Harabtsou"
                });
            
            attr.OnActionExecuting(actionContext);
            Assert.IsTrue(actionContext.ModelState.IsValid);
            Assert.AreEqual(actionContext.ModelState.Count, 0);
            Assert.IsNull(actionContext.Response);
        }

        [Test]
        public void TestValidationPerformedNegative()
        {
            var attr = new RValidateAttribute();
            var actionContext = CreateContext();
            actionContext.ActionArguments.Add(
                "model",
                new SimpleModel
                {
                    Key = 0,
                    FirstName = "Uladzimir Harabtsou"
                });

            attr.OnActionExecuting(actionContext);
            Assert.IsFalse(actionContext.ModelState.IsValid);
            Assert.AreEqual(actionContext.ModelState.Count, 1);
            Assert.IsNotNull(actionContext.Response);
            Assert.AreEqual(actionContext.Response.StatusCode, HttpStatusCode.BadRequest);
        }

        private static HttpActionContext CreateContext()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/");
            var requestContext = new Mock<HttpRequestContext>();
            var controllerDescriptor = new Mock<HttpControllerDescriptor>();
            var controller = new Mock<IHttpController>();
            var controllerContext = new HttpControllerContext(requestContext.Object, request, controllerDescriptor.Object, controller.Object);
            var actionDescriptor = new ReflectedHttpActionDescriptor();
            var actionContext = new HttpActionContext(controllerContext, actionDescriptor);

            return actionContext;
        }

        private class SimpleModel
        {
            [RRequired]
            public int Key { get; set; }

            [RMaxLength(30)]
            public string FirstName { get; set; }
        }
    }
}
