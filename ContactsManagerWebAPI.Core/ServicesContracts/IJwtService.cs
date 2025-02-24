using ContactsManagerWebAPI.Core.DTO;
using ContactsManagerWebAPI.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManagerWebAPI.Core.ServicesContracts
{
    public  interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
        ClaimsPrincipal? GetUserDetailsFromToken(string? jwtToken);
    }
}
