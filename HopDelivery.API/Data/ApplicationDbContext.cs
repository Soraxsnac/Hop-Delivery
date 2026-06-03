using Microsoft.EntityFrameworkCore;
using HopDelivery.API.Entities;
using HopDelivery.API.Entities.Catalogos;

namespace HopDelivery.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cerveza> Cervezas { get; set; }
        public DbSet<CatMarca> CatMarcas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cerveza>().HasData(
                new Cerveza { Id = 1, Nombre = "Corona Extra", Tipo = "Lager", ABV = 4.5, Calificacion = 4, Descripcion = "Lager clara muy refrescante.", ImagenURL = "" },
                new Cerveza { Id = 2, Nombre = "Guinness Draught", Tipo = "Stout", ABV = 4.2, Calificacion = 5, Descripcion = "Stout oscura con espuma cremosa.", ImagenURL = "" },
                new Cerveza { Id = 3, Nombre = "Punk IPA", Tipo = "IPA", ABV = 5.4, Calificacion = 4, Descripcion = "IPA escocesa con notas frutales.", ImagenURL = "" }
            );
        }
    }
}