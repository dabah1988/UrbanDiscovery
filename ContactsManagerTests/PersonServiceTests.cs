using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCoreMock;
using Moq;
using AutoFixture;
using ContactsManager.Core.Services;
using Microsoft.EntityFrameworkCore;
using ContactsManager.Core.Entities;
using ContactsManager.Core.DTO;
using System.Globalization;
using FluentAssertions;
using ContactsManager.Core.RepositoryContract;
using ContactsManager.Core.ServicesContract;
using ContactsManager.Infrastructure.MyDbContext;
using Xunit;
using Xunit.Extensions;

namespace ServiceCountryPersonTests
{
    
    public  class PersonServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IPersonRepository> _personsRepositoryMock;
        private readonly Mock<ICountryRepository> _countryRepositoryMock;

        private readonly IPersonRepository _personsRepository;
        private readonly ICountryRepository _countryRepository;

        private readonly IPersonService _personService;
        private readonly IcountryService _countryService;
        private readonly ApplicationDbContext _dbcontext;
        private readonly List<Person> _personLists;
        private readonly List<Country> _countryLists;

        public PersonServiceTests()
        {
            var personInitialData = new List<Person>();
            var initialCountries = new List<Country>(); 
            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(
             new DbContextOptionsBuilder<ApplicationDbContext>().Options );
             _dbcontext = dbContextMock.Object;
            dbContextMock.CreateDbSetMock(p => p.Persons,personInitialData);
            dbContextMock.CreateDbSetMock(c => c.Countries, initialCountries);

            _personsRepositoryMock = new Mock<IPersonRepository>(); 
            _personsRepository = _personsRepositoryMock.Object; 

            _countryRepositoryMock = new Mock<ICountryRepository>();
            _countryRepository = _countryRepositoryMock.Object;

            _fixture = new Fixture();

            _personService =  new PersonsService(_personsRepository);
            _countryService = new CountryService(_countryRepository);

            _personLists = new List<Person>();
            _countryLists = new List<Country>();


        }

        /// <summary>
        /// Add person Null 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [Fact]
        public async Task AddPerson_ShouldReturnArgumentNullExceptionIfPersonIsNull()
        {           
            PersonAddRequest? personAddRequest = null;

            _personsRepositoryMock.Setup(c => c.AddPerson(It.IsAny<Person>()))
                .Callback<Person>((thePerson) => _personLists.Add(thePerson))
                .ReturnsAsync(new Person());

            _personsRepositoryMock.Setup(c => c.GetAllPersons())
            .ReturnsAsync(_personLists);

            await Assert.ThrowsAsync<ArgumentNullException>(
                async () =>
                {
                    await _personService.AddPerson(personAddRequest);
                });

        }

        [Fact]
        public async Task AddPerson_ShouldReturnArgumentNullExceptionIfPersonNameIsNull()
        {
            PersonAddRequest? personAddRequest = 
                _fixture.Build<PersonAddRequest>()
                .With(p => p.Name , null as string )
                .Create();  
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () =>
                {
                    await _personService.AddPerson(personAddRequest);
                });

        }


        [Fact]
        public async Task AddPerson_ShouldReturnArgumentExceptionWithDuplicateEmail()
        {
  
            PersonAddRequest? personAddRequest =
                _fixture.Build<PersonAddRequest>()
                .With(p => p.Email, "dabson2012@gmail.com")
                .Create();


            _personsRepositoryMock.Setup(p => p.AddPerson(It.Is<Person>(p => p.Email ==  "dabson2012@gmail.com")))
             .Callback<Person>(p => _personLists.Add(p))
            .ReturnsAsync(personAddRequest.ToPerson());

            _personsRepositoryMock.Setup(p => p.GetAllPersons())
            .ReturnsAsync(_personLists);


            await Assert.ThrowsAsync<ArgumentException>(
                async () =>
                {
                    await _personService.AddPerson(personAddRequest);
                    await _personService.AddPerson(personAddRequest);
                });
        }


        [Fact]
        public async Task AddPerson_ShouldReturnProperPersonIfAllPropertiesAreCorrect()
        {   
            CountryAddRequest? countryAddRequest = _fixture.Create<CountryAddRequest>();
       

            if (!DateTime.TryParseExact("01/11/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
            {
                throw new ArgumentException("Invalid date format");
            }

            _countryRepositoryMock.Setup(c => c.AddCountry(It.IsAny<Country>()))
            .Callback<Country>(c => _countryLists.Add(c))
            .ReturnsAsync(countryAddRequest.ToCountry());

            _countryRepositoryMock.Setup(c => c.GetAllCountries())
          .ReturnsAsync(_countryLists);

            CountryResponse? countryResponse = await _countryService.AddCountry(countryAddRequest);

            PersonAddRequest? personAddRequest =
                _fixture.Build<PersonAddRequest>()
                 .With(p => p.Name, "Dabah assandrè yves regis")
                    .With(p => p.PhoneNumber, "0605935300")
                .With(p => p.CountryId, countryResponse?.CountryId)
                .With(p => p.Country, countryResponse?.ToCountry())
                  .With(p => p.DateOfBirth, dateOfBirth)
                    .With(p => p.Email, "kabu@gmail.com")
                .Create();


            _personsRepositoryMock.Setup(p => p.AddPerson(It.IsAny<Person>()))
                .Callback<Person>(p => _personLists.Add(p)  )
                .ReturnsAsync(personAddRequest.ToPerson());

            _personsRepositoryMock.Setup(p => p.GetAllPersons())
            .ReturnsAsync(_personLists);

            PersonResponse? personResponse = await  _personService.AddPerson(personAddRequest);
            personResponse.Should().NotBeNull();    
            personResponse?.Name.Should().Be(personAddRequest.Name);


        }

    }
}
