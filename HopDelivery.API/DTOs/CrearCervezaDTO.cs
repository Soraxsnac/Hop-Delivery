using System.ComponentModel.DataAnnotations;

namespace HopDelivery.API.DTOs;

public class CrearCervezaDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required]
    public string Tipo { get; set; }

    public int IBU { get; set; }
    public double ABV { get; set; }
    public string Descripcion { get; set; }
    public string ImagenURL { get; set; }
}