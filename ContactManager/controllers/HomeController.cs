
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContactsManager.Core.Services;
using ContactsManager.Core.ServicesContract;
using ContactsManager.Core.DTO;
using Microsoft.AspNetCore.Authorization;

namespace ContactManager.controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IPersonService? _personService;
        private readonly IcountryService?  _countryService;
        public HomeController(IPersonService personService , IcountryService countryService)
        {
            _personService = personService;
            _countryService = countryService;   
        }

        [Route("/")]
        
        public async Task<IActionResult> Index()
        {
            if (_personService == null) throw new ArgumentNullException($"{nameof(_personService)} is null ");
            if (_countryService == null) throw new ArgumentNullException($"{nameof(_countryService)} is null ");
            IEnumerable<PersonResponse>? PersonListModel = await _personService.GetAllPersons();
            // Placer ce dictionnaire dans ViewData
            ViewData["CountryDict"] = await _countryService.GetAllCountries();
            return View(PersonListModel);
        }

        /// <summary>
        /// Edit person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("Home/Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (_personService == null) throw new ArgumentNullException($"{nameof(_personService)} is null ");
            if (_countryService == null) throw new ArgumentNullException($"{nameof(_countryService)} is null ");
            PersonResponse? personFound = await _personService.GetPersonById(Guid.Parse(id));
            if (personFound == null) return NotFound();
            var countries = await _countryService.GetAllCountries();
            // Créer un SelectList pour la liste déroulante
            ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName", personFound.CountryId);
            return View(personFound);
        }

        /// <summary>
        /// Update Person
        /// </summary>
        /// <param name="personToUpdate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost("Home/Edit/{id}")]
        public async Task<IActionResult> Edit(PersonResponse personToUpdate)
        {
            if (_personService == null) throw new ArgumentNullException($"{nameof(_personService)} is null ");
            if (ModelState.IsValid) await  _personService.UpdatePerson(personToUpdate.ToPerson());
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("Home/Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (_personService == null) throw new ArgumentNullException($"{nameof(_personService)} is null ");
            PersonResponse? personFound = await _personService.GetPersonById(Guid.Parse(id));
            if (personFound == null) return NotFound();
           await  _personService.RemovePerson(personFound.ToPerson());
            return RedirectToAction("Index");
        }


        [HttpGet("Home/Create")]
        public async Task<IActionResult> Create()
        {
            if (_personService == null) throw new ArgumentNullException($"{nameof(_personService)} is null ");
            if (_countryService == null) throw new ArgumentNullException($"{nameof(_countryService)} is null ");
            var countries = await  _countryService.GetAllCountries();
            ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
            return View();
        }


        [HttpPost("Home/Create")]
        //[TypeFilter(typeof(PersonListActionFilter))]
        public async Task<IActionResult> Create(PersonAddRequest personAddRequest)
        {            
            if (personAddRequest == null) throw new ArgumentNullException($"{nameof(personAddRequest)} is null ");
            if (_personService == null) throw new ArgumentNullException($"{nameof(_personService)} is null ");
            if (ModelState.IsValid)
            {
                PersonResponse? personResponse = await _personService.AddPerson(personAddRequest);
                if (personResponse == null) return NotFound();
                return RedirectToAction("Index");
            }
            return View(personAddRequest);
        }
    }
}
