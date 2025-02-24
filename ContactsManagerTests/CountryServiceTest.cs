using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using ContactsManager.Core.Services;
using ContactsManager.Core.ServicesContract;
using ContactsManager.Core.DTO;
using AutoFixture;
using Xunit;
using FluentAssertions;
using ContactsManager.Core.RepositoryContract;
using ContactsManager.Core.Entities;
using ContactsManager.Infrastructure.MyDbContext;

using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ServiceCountryTests
{
    public class CountryServiceTest
    {
        private readonly IcountryService? _countryService;
        private readonly Fixture _fixture;
        private readonly Mock<ICountryRepository> _countryRepositoryMock;
        private readonly ICountryRepository _countryRepository;
        private List<Country> _countries;
        public CountryServiceTest()
        {
            var countriesInitialData = new List<Country>() { };            

            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>
                (new  DbContextOptionsBuilder<ApplicationDbContext>().Options );  
                ApplicationDbContext dbContext = dbContextMock.Object;

            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);       
            _fixture = new Fixture();
            _countryRepositoryMock = new Mock<ICountryRepository>();
            _countryRepository = _countryRepositoryMock.Object;
            _countryService = new CountryService(_countryRepository);
            _countries = new List<Country>();   

        }
        /// <summary>
        /// Add New Country                       
        /// </summary>
        /// <returns>proper country</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Fact]
        public async Task AddCountry_ShouldReturnProperCountriesIfCountryAreNotNull()
        {
         
                if (_countryService == null) throw new ArgumentNullException($"{nameof(CountryService)} is null ");
           
                _countryRepositoryMock.Setup(rp => rp.GetAllCountries())
                  .ReturnsAsync(RandomListOfCountries());

                CountryAddRequest countryAddRequest = _fixture.Build<CountryAddRequest>()
                  .With(c => c.CountryName, "Côte d'voire")
                  .Create();
                Guid countryId = Guid.NewGuid();
                _countryRepositoryMock.Setup(c => c.AddCountry(It.Is<Country>(c => c.CountryName == "Côte d'voire")))
                 .ReturnsAsync((new Country() { CountryId = countryId, CountryName = countryAddRequest.CountryName }));

                CountryResponse? countryResponse = await _countryService.AddCountry(countryAddRequest);              
                
                if (countryResponse == null) throw new ArgumentNullException("Error");
                countryResponse!.CountryName.Should().Be(countryAddRequest.CountryName);
       
        }

        [Fact]
        public async Task AddCountry_ShouldReturnArgumentExceptionIfCountryNameIsDuplicated()
        {
            CountryAddRequest countryAddRequest =
                _fixture.Build<CountryAddRequest>()
                .With(c => c.CountryName, "Italie")
                .Create();

            CountryAddRequest countryAddRequest2 =
               _fixture.Build<CountryAddRequest>()
               .With(c => c.CountryName, "Italie")
               .Create();

              Guid countryId = Guid.NewGuid();

            _countryRepositoryMock.Setup(rp => rp.GetAllCountries()).ReturnsAsync(_countries);

            _countryRepositoryMock.Setup(c => c.AddCountry(It.Is<Country>(c => c.CountryName == "Italie")))
                .Callback<Country>(country => _countries.Add(country))
                .ReturnsAsync(new Country() {CountryId = countryId, CountryName = "Italie"}); 

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                   {
                       await _countryService.AddCountry(countryAddRequest);
                       await _countryService.AddCountry(countryAddRequest2);
                   });

        }

        public List<Country> RandomListOfCountries()
        {
            return new List<Country>()
            {
               //new Country { CountryId = Guid.Parse("{D869EE42-E316-44C9-9647-AC767497CD6B}") , CountryName = "Sénégal" },
               //new Country { CountryId = Guid.Parse("{3319468A-F1CD-4E5C-BCE5-367A5763A852}") ,CountryName = "Mali" },
               //new Country { CountryId =Guid.Parse("{20931B1B-E7F0-45E2-B2CB-6DD5AAC16FA8}") ,CountryName = "Congo" },
               //new Country { CountryId =Guid.Parse("{E7D9BC06-5981-465C-AC99-AF3407B95104}") ,CountryName = "Nigeria" },
               new Country { CountryId = Guid.Parse("{B3155CD1-E33C-4D47-A3AF-03858609F049}"), CountryName = "Italie" }
            };

            //return new List<Country>() {};
        }

    }
}