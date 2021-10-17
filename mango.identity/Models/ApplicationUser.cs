using Microsoft.AspNetCore.Identity;

namespace mango.identity.Models
{
    public class ApplicationUser : IdentityUser
    {

        //Estos campos son añadidos a los campos por defecto que crea IdentityServer y
        ////al estar referenciada la clase en el contexto se crearán con la migración a bbdd
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
