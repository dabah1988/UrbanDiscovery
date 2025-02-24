using Moq;
using ContactsManager.Core.Services;
using ContactsManager.Core.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactManager.controllers;
using Microsoft.Build.Framework;
using ContactsManager.Core.Entities;
using ContactsManager.Core.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Xunit;

namespace ServiceCountryPersonTests
{
    public  class HomeControllerTests
    {
        private readonly Mock<IPersonService> _IPersonServiceMock;
        private readonly Mock<IcountryService> _ICountryServiceMock;
        private readonly IcountryService _ICountryService;
        private readonly IPersonService _IPersonService;
        private List<PersonResponse> _personsResponse;
        private List<CountryResponse> _countriesResponse;
        private readonly Fixture _fixture;

        public HomeControllerTests()
        {
            _IPersonServiceMock = new Mock<IPersonService>();   
            _ICountryServiceMock = new Mock<IcountryService>();
            _IPersonService = _IPersonServiceMock.Object;
            _ICountryService = _ICountryServiceMock.Object;
            _personsResponse = new List<PersonResponse>();
            _countriesResponse = new List<CountryResponse>();
            _fixture = new Fixture();
        }
        /// <summary>
        /// Test Controller Index
        /// </summary>
        /// <returns>ViewResult</returns>
        [Fact]
        public async Task Index_shouldReturnIndexViewWithListOfPersons()
        {
            HomeController homeController = new HomeController(_IPersonService, _ICountryService);
            _IPersonServiceMock.Setup(p => p.GetAllPersons()) .ReturnsAsync(_personsResponse);
            _ICountryServiceMock.Setup(c => c.GetAllCountries()).ReturnsAsync(_countriesResponse);
           IActionResult homeResult =  await homeController.Index();
            ViewResult viewResult =  Assert.IsType<ViewResult>(homeResult);
            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<PersonResponse>>();          
        }


        /// <summary>
        /// Create_should Return PersonAddRequest Model If ModelState Is Invalid
        /// </summary>
        /// <returns>PersonAddRequest</returns>
        [Fact]
        public async Task Create_shouldReturnPersonAddRequestModelIfModelStateIsInvalid()
        {
            HomeController homeController = new HomeController(_IPersonService, _ICountryService);
            PersonResponse personResponse = _fixture.Create<PersonResponse>();
            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>()
                .With(p => p.Email,"dab@comm") //We gave invalid Email 
                .Create();

            _IPersonServiceMock.Setup(p => p.AddPerson(It.IsAny<PersonAddRequest>()))
            .ReturnsAsync(personResponse);

            homeController.ModelState.AddModelError("Email","Email is invalid");
            IActionResult homeResult = await homeController.Create(personAddRequest);
            ViewResult viewResult = Assert.IsType<ViewResult>(homeResult);
            viewResult.ViewData.Model.Should().BeOfType<PersonAddRequest>();
        }

        [Fact]
        public async Task Create_shouldRedirectToActionWhenPersonAddRequestModelIfModelStatevalid()
        {
            HomeController homeController = new HomeController(_IPersonService, _ICountryService);
            PersonResponse personResponse = _fixture.Create<PersonResponse>();
            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>()
                .With(p => p.Email, "dab@comm") //We gave invalid Email 
                .Create();

            _IPersonServiceMock.Setup(p => p.AddPerson(It.IsAny<PersonAddRequest>()))
            .ReturnsAsync(personResponse);

            IActionResult homeResult = await homeController.Create(personAddRequest);
            RedirectToActionResult viewResult = Assert.IsType<RedirectToActionResult>(homeResult);
            viewResult.ActionName.Should().Be("Index");
        }
    }
}
