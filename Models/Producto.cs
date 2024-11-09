using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Examen.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string? Titulo { get; set; }
        public string? DesProducto { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public int Clasificacion { get; set; }
        public int Existencias { get; set; }
        public string? Marca { get; set; }
        public string? Categoria { get; set; }
        public string? Fondo { get; set; }

        [JsonIgnore]
        public virtual List<ProductoImagen>? ProductoImages { get; set; }

        [JsonIgnore]
        public virtual List<Ventas>? Ventas { get; set; }
    }
}
