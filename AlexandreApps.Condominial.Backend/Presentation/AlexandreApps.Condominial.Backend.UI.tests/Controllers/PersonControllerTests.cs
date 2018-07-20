using AlexandreApps.Condominial.Backend.Interfaces.AppService.Global;
using AlexandreApps.Condominial.Backend.Model.Global.ViewModel;
using AlexandreApps.Condominial.Backend.Model.Security;
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
    public class PersonControllerTests
    {
        public PersonControllerTests()
        {
            var userData = new UserModel
            {
                Id = Guid.Empty,
                Name = "João",
                BirthDate = new DateTime(2010, 1, 1),
                Country = 55,
                Login = "joao@email.com",
                PersonId = null,
                SubscribeDate = DateTime.Today
            };

            _personModel = new PersonViewModel[]
            {
                new PersonViewModel
                {
                    Id = Guid.Empty,
                    Name = "José da Silva",
                    DocumentCode = "111",
                    DocumentType = "11",
                    PreferedLanguage = "pt-br",
                    IsActive = true,
                    Addresses = new List<AddressViewModel> {
                        new AddressViewModel
                        {
                            Id = Guid.Empty,
                            Street = "Street",
                            Number = "1",
                            ComplementaryInformation = "Complementary",
                            ZipCode = "11111-111",
                            City = "City",
                            Province = "CC",
                            Country = "55",
                            Latitude = 0,
                            Longitude = 0
                        }
                    },
                    Contacts = new List<ContactViewModel>
                    {
                        new ContactViewModel
                        {
                            Id = Guid.Empty,
                            Comment = "Comment",
                            Type = "Tel",
                            Value = "(11)2222-3333"
                        }
                    },
                    Notes = new List<NoteViewModel>
                    {
                        new NoteViewModel
                        {
                            Id = Guid.Empty,
                            Text = "This is a note",
                            LastUpdate = DateTime.Today
                        }
                    },
                    LastUpdate = DateTime.Today
                }
            };

            var mockAppService = new Mock<IPersonAppService>();
            mockAppService.Setup(x => x.Insert(userData, _personModel)).ReturnsAsync(new List<Guid> { Guid.NewGuid() });
            mockAppService.Setup(x => x.Update(userData, _personModel)).ReturnsAsync(new List<Guid> { Guid.Empty });
            mockAppService.Setup(x => x.Delete(userData, Guid.Empty)).ReturnsAsync(new List<Guid> { Guid.Empty });
            mockAppService.Setup(x => x.Get(Guid.Empty)).ReturnsAsync(_personModel);

            this._personController = new PersonController(mockAppService.Object);
        }

        private readonly PersonController _personController;
        private readonly PersonViewModel[] _personModel;

        //[Fact]
        //public async void Get()
        //{
        //    var result = await _personController.Get(Guid.Empty);
        //    Assert.NotNull(result);

        //    var okResult = result as OkObjectResult;
        //    Assert.Equal(200, okResult.StatusCode);
        //    var value = okResult.Value as List<UserViewModel>;

        //    Assert.Equal(_personModel.Count(), value.Count());
        //}

        //[Fact]
        //public async void Create()
        //{
        //    var item = _personModel.First();
        //    var result = await _personController.Create(item);
        //    Assert.NotNull(result);

        //    var okResult = result as CreatedResult;
        //    Assert.Equal(201, okResult.StatusCode);
        //}

        //[Fact]
        //public async void Update()
        //{
        //    var item = _personModel.First();
        //    var result = await _personController.Update(item);
        //    Assert.NotNull(result);

        //    var okResult = result as AcceptedResult;
        //    Assert.Equal(202, okResult.StatusCode);
        //}

        //[Fact]
        //public async void Delete()
        //{
        //    var item = _personModel.First();
        //    var result = await _personController.Delete(Guid.Empty);
        //    Assert.NotNull(result);

        //    var okResult = result as AcceptedResult;
        //    Assert.Equal(202, okResult.StatusCode);
        //}
    }
}
