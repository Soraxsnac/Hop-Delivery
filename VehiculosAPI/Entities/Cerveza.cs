using System.ComponentModel.DataAnnotations;

namespace VehiculosAPI.Entities
{
    public class Cerveza
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la cerveza es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } 

        public int IBU { get; set; } 

        public double ABV { get; set; } 

        [MaxLength(500)]
        public string Descripcion { get; set; }

        public string ImagenURL { get; set; } 
    }
}