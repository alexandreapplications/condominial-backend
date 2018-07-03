using AlexandreApps.Condominial.Backend.Model.Domain;
using AlexandreApps.Condominial.Backend.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlexandreApps.Condominial.Backend.UI.tests.Controllers
{
    public class ValuesControllerTests
    {
        [Fact(DisplayName ="Ctrl - Values - Get")]
        public async Task Get()
        {
            ValuesController controller = new ValuesController();

            // Act
            var result = await controller.Get();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, actionResult.StatusCode);

            var model = Assert.IsAssignableFrom<RequiredStringValueModel[]>(actionResult.Value);

            Assert.Equal(2, model.Length);
        }
        [Fact(DisplayName = "Ctrl - Values - Post Valid Model")]
        public async Task Post_OkModel()
        {
            var testModel = new RequiredStringValueModel() { value = "value10" };

            ValuesController controller = new ValuesController();

            var result = await controller.Post(testModel);

            var actionResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, actionResult.StatusCode);

            var model = Assert.IsAssignableFrom<RequiredStringValueModel>(actionResult.Value);

            Assert.Equal(testModel, model);
        }

        [Fact(DisplayName = "Ctrl - Values - Post Invalid Model")]
        public async Task Post_InvalidModel()
        {
            var testModel = new RequiredStringValueModel();

            ValuesController controller = new ValuesController();

            var result = await controller.Post(testModel);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, actionResult.StatusCode);

            var model = Assert.IsAssignableFrom<SerializableError>(actionResult.Value);

            Assert.Single(model);
        }

        [Fact(DisplayName = "Ctrl - Values - Post Empty Model")]
        public async Task Post_EmptyModel()
        {
            var testModel = new RequiredStringValueModel() { value = string.Empty };

            ValuesController controller = new ValuesController();

            var result = await controller.Post(testModel);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, actionResult.StatusCode);

            var model = Assert.IsAssignableFrom<SerializableError>(actionResult.Value);

            Assert.Single(model);
        }

        [Fact(DisplayName = "Ctrl - Values - Put Valid Model")]
        public async Task Put_OkModel()
        {
            var testModel = new RequiredStringValueModel() { value = "value10" };

            ValuesController controller = new ValuesController();

            var result = await controller.Put(10, testModel);

            var actionResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, actionResult.StatusCode);

            var model = Assert.IsAssignableFrom<RequiredStringValueModel>(actionResult.Value);

            Assert.Equal(testModel, model);
        }

        [Fact(DisplayName = "Ctrl - Values - Put Invalid Model")]
        public async Task Put_InvalidModel()
        {
            var testModel = new RequiredStringValueModel();

            ValuesController controller = new ValuesController();

            var result = await controller.Put(10, testModel);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, actionResult.StatusCode);

            var model = Assert.IsAssignableFrom<SerializableError>(actionResult.Value);

            Assert.Single(model);
        }

        [Fact(DisplayName = "Ctrl - Values - Put Empty Model")]
        public async Task Put_EmptyModel()
        {
            var testModel = new RequiredStringValueModel() { value = string.Empty };

            ValuesController controller = new ValuesController();

            var result = await controller.Put(10, testModel);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, actionResult.StatusCode);

            var model = Assert.IsAssignableFrom<SerializableError>(actionResult.Value);

            Assert.Single(model);
        }

        [Fact(DisplayName = "Ctrl - Values - Delete valid")]
        public async Task Delete_Valid()
        {
            ValuesController controller = new ValuesController();

            var result = await controller.Delete(10);

            var actionResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, actionResult.StatusCode);

            var model = Assert.IsAssignableFrom<int>(actionResult.Value);

            Assert.Equal(10, model);
        }

        [Fact(DisplayName = "Ctrl - Values - Delete invalid")]
        public async Task Delete_Invalid()
        {
            ValuesController controller = new ValuesController();

            var result = await controller.Delete(100);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, actionResult.StatusCode);

            Assert.IsType<ApplicationException>(actionResult.Value);
        }
    }
}
