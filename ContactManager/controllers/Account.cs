using ContactManager.controllers;
using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.Core.DTO;
using ContactsManager.Core.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.controllers
{
    [Route("[controller]/[action]")]
    public class Account : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager; 
        public Account(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager; 
        }

        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize("NotAuthorized")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if( !ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(err => err.ErrorMessage);
                return View(registerDTO);
            }
            var roleName = registerDTO.UserType == UserTypeOptions.Admin ? UserTypeOptions.Admin.ToString() :   
                UserTypeOptions.User.ToString();       
            ApplicationUser userApplication = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName,               
            };
            IdentityResult resultCreationOfUser = await _userManager.CreateAsync(userApplication,registerDTO?.Password);
            if (resultCreationOfUser.Succeeded)
            {
                ApplicationRole applicationRole = new ApplicationRole() { Name = roleName };
               var resultCreationOfRole =  await _roleManager.CreateAsync(applicationRole);
                await _signInManager.SignInAsync(userApplication, false);
                if (resultCreationOfRole.Succeeded)
                {
                    var resultAffectRoleToUser = await _userManager.AddToRoleAsync(userApplication,roleName);
                    if (!resultAffectRoleToUser.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Erreur lors de l'attribution du rôle à l'utilisateur.");
                        ViewBag.Errors = resultAffectRoleToUser.Errors.Select(e => e.Description).Distinct();
                    }
                }

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            // Si la création de l'utilisateur a échoué, ajouter les erreurs au ModelState
            foreach (var error in resultCreationOfUser.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            // Optionnel : Ajouter les erreurs à ViewBag pour les afficher dans la vue
            ViewBag.Errors = resultCreationOfUser.Errors.Select(e => e.Description);
            return View(registerDTO);
        }


        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Login(LoginDTO loginDTO,string? ReturnUrl)
        {
            if(loginDTO == null ) throw new ArgumentNullException(nameof(loginDTO));    
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(err => err.ErrorMessage);
                return View(loginDTO);
            }
           var  result =  await _signInManager.PasswordSignInAsync(loginDTO?.Email, loginDTO?.Password,
               isPersistent:false,lockoutOnFailure:false);
            if (result.Succeeded)
            {
                 ApplicationUser? userApplication = await _userManager.FindByEmailAsync(loginDTO.Email);
                if(userApplication != null)
                {
                    if(await _userManager.IsInRoleAsync(userApplication,UserTypeOptions.Admin.ToString()))
                    {
                        return RedirectToAction("Index", "AdminHome", new {area ="AdminArea"});
                    }
                }
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }

            }
            ModelState.AddModelError("Login", "Invalid email or password");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
         return RedirectToAction(nameof(HomeController.Index), "Home");
           
        }

        [AllowAnonymous]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Json(true);
            return Json(false);

        }
    }
}
