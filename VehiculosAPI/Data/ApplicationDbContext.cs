using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehiculosAPI.Entities;
using VehiculosAPI.Entities.Catalogos;

namespace VehiculosAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<CatMarca> CatMarcas { get; set; }
        public DbSet<Cerveza> Cervezas { get; set; }

    }
}
