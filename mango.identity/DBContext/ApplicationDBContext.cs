using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace mango.identity.DBContext
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }


        public bool IsOpen()
        {
            return this.Database.GetDbConnection().State == System.Data.ConnectionState.Open;
        }


    }
}
