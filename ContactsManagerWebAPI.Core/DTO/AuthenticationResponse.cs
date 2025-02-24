using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManagerWebAPI.Core.DTO
{
    public class AuthenticationResponse
    {
        public string PersonName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public DateTime RefreshTokenExpirationDatetime {  get; set; }

        public DateTime Expiration { get; set; }  
    }
}
