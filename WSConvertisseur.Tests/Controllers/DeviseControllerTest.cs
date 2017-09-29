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
            IHttpActionResult result1 = controller.Get(15);

            // Assert
            Assert.IsInstanceOfType(result1, typeof(NotFoundResult));
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Devise>));
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), result.Content);

        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            DeviseController controller = new DeviseController();

            // Act

            // Assert
        }


        [TestMethod]
        public void Put()
        {
            // Arrange
            DeviseController controller = new DeviseController();

            // Act
          //  controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            DeviseController controller = new DeviseController();

            // Act
            IHttpActionResult result = controller.Delete(5);
            var result1 = controller.Delete(1) as OkNegotiatedContentResult<Devise>;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(OkNegotiatedContentResult<Devise>));
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), result1.Content);
            
        }
    }
}
