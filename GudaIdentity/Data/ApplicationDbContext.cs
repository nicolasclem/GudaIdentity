using GudaIdentity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GudaIdentity.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        //Agregar  modelos 

        public DbSet <AppUsuario> AppUsuarios { get; set; } 
    }
}
