using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Models
{

    // It derives from the IdentityDbContext class and provides the database structures required for user authentication. 
    // The IdentityUser type is the default ASP.NET Core Identity user model.
   
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
     // Constructor takes DbContextOptions class and passes it to IdentityDbContext's constructor.
// DbContextOptions provides information about database connection and configuration.
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)  // DbContextOptions contains the configuration needed to interact with the database.
        {

        }
    }
}