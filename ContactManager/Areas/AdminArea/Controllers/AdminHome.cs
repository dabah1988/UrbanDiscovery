using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Route("AdminArea/[controller]/[action]")]
    [Authorize(Roles ="Admin")]
    public class AdminHome : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
