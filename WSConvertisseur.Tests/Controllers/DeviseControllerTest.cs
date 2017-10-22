using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System.Web.Http;
using WSConvertisseur.Models;
using System.Linq;
using System.Web.Http.Results;

namespace WSConvertisseur.Tests.Controllers
{
    /// <summary>
    /// Summary description for DeviseController
    /// </summary>
    [TestClass]
    public class DeviseControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            DeviseController controller = new DeviseController();

            // Act
            IEnumerable<Devise> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), result.ElementAt(0));
            Assert.AreEqual(new Devise(2, "Franc Suisse", 1.07), result.ElementAt(1));
            Assert.AreEqual(new Devise(3, "Yen", 120), result.ElementAt(2));
        }


        [TestMethod]
        public void Get(int id)
        {
            // Arrange
            DeviseController controller = new DeviseController();

            // Act
            var result = controller.Get(1) as OkNegotiatedContentResult<Devise>;
            IHttpActionResult result1 = controller.Get(1);
            IHttpActionResult result2 = controller.Get(15);

            // Assert
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult));
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(OkNegotiatedContentResult<Devise>));
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), result.Content);

        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            DeviseController controller = new DeviseController();
            Devise d = new Devise();

            // Act
            var result = controller.Post(d) as CreatedAtRouteNegotiatedContentResult<Devise>;
            IHttpActionResult result1 = controller.Post(new Devise());

            // Assert
            Assert.IsInstanceOfType(result1, typeof(CreatedAtRouteNegotiatedContentResult<Devise>));
            Assert.AreEqual(d, result.Content);

            controller.ModelState.AddModelError("fakeError", "fakeError");
            var response = controller.Post(d);
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));

        }


        [TestMethod]
        public void Put()
        {
            // Arrange
            DeviseController controller = new DeviseController();
            Devise dFalse = new Devise(7,"test", 2);
            Devise dUpdated = new Devise(1, "testYes", 2);

            // Act
            IHttpActionResult result1 = controller.Put(7, dUpdated);
            var result = controller.Put(7, dUpdated) as StatusCodeResult;
            IHttpActionResult result2 = controller.Put(2, dFalse);
            IHttpActionResult result3 = controller.Put(7, dFalse);


            // Assert
            Assert.AreEqual(1, dUpdated.Id);
            Assert.AreEqual(controller.Get(1), result);
            Assert.IsInstanceOfType(result1, typeof(StatusCodeResult));
            Assert.IsInstanceOfType(result2, typeof(BadRequestResult)); // devise.id is not the same as the id parameter
            Assert.IsInstanceOfType(result3, typeof(NotFoundResult)); // Id is not found


            controller.ModelState.AddModelError("fakeError", "fakeError");
            var response = controller.Put(7, dFalse);
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));
        }


        [TestMethod]
        public void Delete()
        {
            // Arrange
            DeviseController controller = new DeviseController();

            // Act
            IHttpActionResult result1 = controller.Delete(2);
            IHttpActionResult result2 = controller.Delete(15);

            // Assert
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult));
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(OkNegotiatedContentResult<Devise>));

            var result = controller.Delete(1) as OkNegotiatedContentResult<Devise>;
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), result.Content);            
        }
    }
}
