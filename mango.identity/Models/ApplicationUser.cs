using Microsoft.AspNetCore.Identity;

namespace mango.identity.Models
{
    public class ApplicationUser : IdentityUser
    {

        //Estos campos son añadidos a los campos por defecto que crea IdentityServer
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
