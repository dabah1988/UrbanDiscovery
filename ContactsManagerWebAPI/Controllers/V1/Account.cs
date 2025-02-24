using ContactsManagerWebAPI.Core.DTO;
using ContactsManagerWebAPI.Core.Identity;
using ContactsManagerWebAPI.Core.Services;
using ContactsManagerWebAPI.Core.ServicesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactsManagerWebAPI.Controllers.V1
{
    /// <summary>
    /// Account Controller 
    /// </summary>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class AccountController : CustomControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="jwtService"></param>
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager ,
            IJwtService jwtService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        /// <summary>
        /// For Registration of user
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join("|",
                    ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return Problem( errorMessage);
            }
            if (registerDTO == null || string.IsNullOrWhiteSpace(registerDTO.Password))
            {
                return Problem( "Invalid request or password is missing." );
            }

            //Create User
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                 var authenticationResponse =  _jwtService.CreateJwtToken(user);
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDatetime;
                await _userManager.UpdateAsync(user);
                return Ok(authenticationResponse);
            }
            else
            {
                String errorMessage = string.Join("|", result.Errors.Select(e => e.Description));
                return Problem(errorMessage);

            }
        }

   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("IsEmailAlreadyExists")]
        public async Task<IActionResult> IsEmailAlreadyExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(new { Error = "Email is required." });
            try
            {
                if (_userManager == null) return Problem("Service not available");
                ApplicationUser? user = await _userManager.FindByEmailAsync(email);
                return Ok(user == null);
                 
            }
            catch (Exception ex)
            {
                return Problem($"An error occurred: {ex.Message}", statusCode: 500);

            }
        }

        /// <summary>
        /// Search for User
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> PosLogin(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join("|",
                    ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return Problem( 
                    detail: errorMessage,
                    title:"Erreur survenu dans la validation des données");
            }
            if (loginDTO == null || string.IsNullOrWhiteSpace(loginDTO.Password) ||
                string.IsNullOrWhiteSpace(loginDTO.Email))
            {
               return Problem(detail:$"Objet {nameof(LoginDTO)} is null or {nameof(LoginDTO.Email)} is null or {nameof(LoginDTO.Password)} is null " );
            }
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
                isPersistent:false, lockoutOnFailure:false);
            if (result.Succeeded)
            {
                ApplicationUser? user =await  _userManager.FindByEmailAsync(loginDTO.Email);
                if (user == null) return Problem(title:"User Error", detail: $"user with login {nameof(loginDTO.Email)} not found",statusCode:204);
                var authenticationResponse = _jwtService.CreateJwtToken(user);
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDatetime;
                await _userManager.UpdateAsync(user);
                return Ok( authenticationResponse);
            }

            return Problem("Invalid Email or Password");
        }


        /// <summary>
        /// LogOut Session
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpGet("logout")]
        public async Task<ActionResult> GetLoGout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        /// <summary>
        /// For Generate Now Token
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [HttpPost("generateNewJwtToken")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenModel tokenModel)
        {
            if(tokenModel == null || string.IsNullOrWhiteSpace(tokenModel.Token)
                || string.IsNullOrWhiteSpace(tokenModel.RefreshToken)  )
                return BadRequest("Invalid Token");
          
            ClaimsPrincipal? principals = _jwtService.GetUserDetailsFromToken(tokenModel.Token);
            if (principals == null) return BadRequest("Invalid Token");
            string? email = principals.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email)) return BadRequest("User Name not found");
            ApplicationUser? user = await _userManager.FindByNameAsync(email);
            if(user == null || user.RefreshToken != tokenModel.RefreshToken
                || user.RefreshTokenExpirationDateTime <= DateTime.Now)
                return BadRequest("Invalid refresh Token ");
           AuthenticationResponse authenticationResponse =  _jwtService.CreateJwtToken(user);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDatetime;
           await  _userManager.UpdateAsync(user);
            return Ok( authenticationResponse);            
        }

    }
}
