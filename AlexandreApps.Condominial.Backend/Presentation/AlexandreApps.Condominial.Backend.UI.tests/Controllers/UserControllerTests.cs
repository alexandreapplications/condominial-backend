using AlexandreApps.Condominial.Backend.Interfaces.AppService.Security;
using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using AlexandreApps.Condominial.Backend.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlexandreApps.Condominial.Backend.UI.tests.Controllers
{
    public class UserControllerTests
    {
        public UserControllerTests()
        {
            var mockAppService = new Mock<IUserAppService>();
            mockAppService.Setup(x => x.GetAll()).ReturnsAsync(MockData);
            mockAppService.Setup(x => x.Insert(MockData[0])).ReturnsAsync(new List<Guid> { Guid.Empty });
            mockAppService.Setup(x => x.Update(MockData[0])).ReturnsAsync(new List<Guid> { Guid.Empty });
            mockAppService.Setup(x => x.Delete()).ReturnsAsync(new List<Guid> { Guid.Empty });
            var mockPasswordAppService = new Mock<IPasswordAppService>();
            this._userController = new UserController(mockAppService.Object, mockPasswordAppService.Object);
        }
        private List<UserViewModel> MockData { get; set; } = new List<UserViewModel> {
                new UserViewModel {
                    Id = Guid.Empty,
                    Name = "João",
                    BirthDate = new DateTime(2010,1,1),
                    Country = 55,
                    Login = "joao@email.com",
                    PersonId = null,
                    SubscribeDate = DateTime.Today
                }
            };

        private readonly UserController _userController;

        [Fact(DisplayName="UserControllerTests -> GetAll")]
        public async void GetAll()
        {
            var result = await _userController.Get(null);
            Assert.NotNull(result);

            var okResult = result as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
            var value = okResult.Value as List<UserViewModel>;

            Assert.Equal(MockData.Count(), value.Count());
        }

        [Fact(DisplayName="UserControllerTests -> Insert")]
        public async void Insert()
        {
            var item = MockData.First();
            var result = await _userController.Insert(item);
            Assert.NotNull(result);

            var okResult = result as CreatedResult;
            Assert.Equal(201, okResult.StatusCode);
        }

        [Fact(DisplayName = "UserControllerTests -> Update")]
        public async void Update()
        {
            var item = MockData.First();
            var result = await _userController.Update(item);
            Assert.NotNull(result);

            var okResult = result as AcceptedResult;
            Assert.Equal(202, okResult.StatusCode);
        }

        [Fact(DisplayName = "UserControllerTests -> Deleted")]
        public async void Deleted()
        {
            var item = MockData.First();
            var result = await _userController.Delete(Guid.Empty);
            Assert.NotNull(result);

            var okResult = result as AcceptedResult;
            Assert.Equal(202, okResult.StatusCode);
        }
    }
}
