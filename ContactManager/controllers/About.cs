using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.controllers
{
    [Route("[controller]/[action]")]
    public class About : Controller
    {
        // GET: About
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return View();
        }


    }
}
