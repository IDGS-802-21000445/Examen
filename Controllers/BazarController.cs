using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen.Models;

namespace Examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BazarController : ControllerBase
    {
        private readonly BazarContext _context;

        public BazarController(BazarContext context)
        {
            _context = context;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItems([FromQuery] string? q)
        {
            var items = string.IsNullOrEmpty(q)
                ? await _context.Producto
                    .Select(p => new
                    {
                        p.ProductoId,
                        p.Titulo,
                        p.DesProducto,
                        p.Precio,
                        p.Descuento,
                        p.Existencias,
                        p.Marca,
                        p.Categoria,
                        p.Fondo
                    }).ToListAsync()
                : await _context.Producto
                    .Where(p => p.Titulo.Contains(q) || p.DesProducto.Contains(q))
                    .Select(p => new
                    {
                        p.ProductoId,
                        p.Titulo,
                        p.DesProducto,
                        p.Precio,
                        p.Descuento,
                        p.Existencias,
                        p.Marca,
                        p.Categoria,
                        p.Fondo
                    }).ToListAsync();

            return Ok(items);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _context.Producto
                .Where(p => p.ProductoId == id)
                .Select(p => new
                {
                    p.ProductoId,
                    p.Titulo,
                    p.DesProducto,
                    p.Precio,
                    p.Descuento,
                    p.Existencias,
                    p.Marca,
                    p.Categoria,
                    p.Fondo
                })
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost("addSale")]
        public async Task<IActionResult> AddSale([FromBody] Ventas venta)
        {
            var producto = await _context.Producto.FindAsync(venta.ProductoId);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado." });
            }

            if (producto.Existencias < venta.Cantidad)
            {
                return BadRequest(new { message = "No hay suficiente stock para realizar la venta." });
            }

            producto.Existencias -= venta.Cantidad;
            _context.Entry(producto).State = EntityState.Modified;

            venta.FechaVenta = DateTime.Now;
            venta.PrecioVenta = (decimal)producto.Precio * venta.Cantidad;


            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }


        [HttpGet("sales")]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _context.Ventas
                .Select(v => new
                {
                    v.VentaId,
                    v.ProductoId,
                    v.Cantidad,
                    v.FechaVenta,
                    Producto = new
                    {
                        v.Producto.Titulo,
                        v.Producto.Precio
                    }
                })
                .ToListAsync();

            return Ok(sales);
        }

        [HttpGet("items/{id}/images")]
        public async Task<IActionResult> GetProductImages(int id)
        {
            var images = await _context.ProductoImagen
                .Where(img => img.ProductoId == id)
                .Select(img => new
                {
                    img.ImagenId,
                    img.ImagenUrl
                })
                .ToListAsync();

            if (images == null || !images.Any())
            {
                return NotFound(new { message = "No se encontraron imágenes para el producto especificado." });
            }

            return Ok(images);
        }

    }
}
