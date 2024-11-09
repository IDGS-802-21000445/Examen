namespace Examen.Models
{
    public class ProductoImagen
    {
        public int ImagenId { get; set; }
        public int ProductoId { get; set; }
        public string? ImagenUrl { get; set; }

        public virtual Producto? Producto { get; set; }
    }
}
