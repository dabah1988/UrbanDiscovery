using ContactsManagerWebAPI.Core.DTO;
using ContactsManagerWebAPI.Core.Identity;
using ContactsManagerWebAPI.Core.ServicesContracts;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManagerWebAPI.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        /// <summary>
        /// Fonction  for  create Jwt Token for exchnages between server and client
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public AuthenticationResponse CreateJwtToken(ApplicationUser user)
        {
            try {
                if (user == null) throw new ArgumentNullException($"{nameof(user)} is null ");
                if (_configuration == null) throw new ArgumentNullException($"{nameof(_configuration)} is null ");

                //DateTime expirationDate = DateTime.UtcNow.AddMinutes(
                //        Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

                DateTime expirationDate = DateTime.UtcNow.AddMinutes(
    Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]))
    .ToUniversalTime(); //


                Claim[] claims = new Claim[]
                {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()), //Subjet , User Id
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), //Jwt Unique Id
              new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString()),

              new Claim(ClaimTypes.Email,user.Email), //Unique email
                new Claim(ClaimTypes.NameIdentifier,user.Email), //Unique Name Identifier
                 new Claim(ClaimTypes.Name,user.PersonName), //  Name of User
              };
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

                SigningCredentials signingCredentials = new SigningCredentials(
                    securityKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                     _configuration["Jwt:Audience"],
                     claims,
                     expires: expirationDate,
                     signingCredentials: signingCredentials
                    );

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                string token = tokenHandler.WriteToken(tokenGenerator);
                return new AuthenticationResponse()
                {
                    Token = token,
                    Email = user.Email,
                    PersonName = user.PersonName,
                    Expiration = expirationDate,
                    RefreshToken = GenerateRefreshToken(),
                    RefreshTokenExpirationDatetime = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["RefreshToken:EXPIRATION_MINUTES"])),
                };
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);   
            }
            return null;
         }

        public ClaimsPrincipal? GetUserDetailsFromToken(string? jwtToken)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
               IssuerSigningKey  = new SymmetricSecurityKey( 
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"])),
               ValidateLifetime = false,
            };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal? userDetails = jwtSecurityTokenHandler.ValidateToken(
                jwtToken,
                tokenValidationParameters,
                out SecurityToken securityToken
                );
            if(securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.
                Equals(SecurityAlgorithms.HmacSha256,StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityTokenException("Token invalide");
            }
            return userDetails;
        }

        private string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var  randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
