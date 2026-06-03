namespace HopDelivery.API.DTOs
{
    public class CervezaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public double ABV { get; set; }
        public int IBU { get; set; }
        public string Descripcion { get; set; } 
        public string ImagenURL { get; set; }
        public int IdMarca { get; set; }
        public int Calificacion { get; set; }
    }
}