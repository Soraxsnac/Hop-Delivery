using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HopDelivery.API.Entities;
using HopDelivery.API.Entities.Catalogos;

namespace HopDelivery.API.Data
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
