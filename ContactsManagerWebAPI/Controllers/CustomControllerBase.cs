using Microsoft.AspNetCore.Mvc;

namespace ContactsManagerWebAPI.Controllers
{
    /// <summary>
    /// Add CustomControllerBase
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
       
    }
 
}