using System;
using System.Text.Json.Serialization;

namespace Examen.Models
{
    public class Ventas
    {
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal PrecioVenta { get; set; }

        [JsonIgnore]
        public virtual Producto? Producto { get; set; }
    }
}
