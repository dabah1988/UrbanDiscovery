using Microsoft.AspNetCore.Mvc.Filters;
using ContactsManager.Core.DTO;
using System.Drawing.Printing;

namespace ContactManager.Filter.Actions
{
    public class PersonListActionFilter : IActionFilter
    {
        private readonly ILogger<PersonListActionFilter> _logger;
        public PersonListActionFilter(ILogger<PersonListActionFilter> logger)
        { 
             _logger = logger;              
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("OnActionExecuted Method");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("ActionExecuting Method");
            // Vérifie si l'argument "personAddRequest" est présent
            if (context.ActionArguments.ContainsKey("personAddRequest"))
            {
                if (context.ActionArguments["personAddRequest"] is PersonAddRequest personAddRequest)
                {
                    // 🔹 Modification des valeurs de `personAddRequest`
                    personAddRequest.Email = "newemail@example.com";
                    personAddRequest.Name = "UpdatedFirstName";

                    _logger.LogInformation("personAddRequest modifié : {Name} {Name}",
                        personAddRequest.Name, personAddRequest.Email);
                }
            }
        }
    }
}
