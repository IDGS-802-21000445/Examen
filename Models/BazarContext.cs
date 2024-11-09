using Microsoft.EntityFrameworkCore;

namespace Examen.Models
{
    public class BazarContext : DbContext
    {
        public BazarContext() { }

        public BazarContext(DbContextOptions<BazarContext> options) : base(options) { }

        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<ProductoImagen> ProductoImagen { get; set; }
        public virtual DbSet<Ventas> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProductoId).HasName("PK__Producto___451468AC4075AEB9");

                entity.ToTable("Producto");

                entity.Property(e => e.ProductoId).HasColumnName("ProductoId");
                entity.Property(e => e.Titulo).HasColumnName("Titulo");
                entity.Property(e => e.DesProducto).HasColumnName("DesProducto");
                entity.Property(e => e.Precio).HasColumnName("Precio");
                entity.Property(e => e.Descuento).HasColumnName("Descuento");
                entity.Property(e => e.Clasificacion).HasColumnName("Clasificacion");
                entity.Property(e => e.Existencias).HasColumnName("Existencias");
                entity.Property(e => e.Marca).HasColumnName("Marca");
                entity.Property(e => e.Categoria).HasColumnName("Categoria");
                entity.Property(e => e.Fondo).HasColumnName("Fondo");
            });

            modelBuilder.Entity<ProductoImagen>(entity =>
            {
                entity.HasKey(e => e.ImagenId).HasName("PK__ProductoImagen__ImagenId");

                entity.ToTable("ProductoImages");

                entity.Property(e => e.ImagenId).HasColumnName("ImagenId");
                entity.Property(e => e.ProductoId).HasColumnName("ProductoId");
                entity.Property(e => e.ImagenUrl)
                      .HasColumnName("ImagenUrl")
                      .HasMaxLength(200);

                entity.HasOne(d => d.Producto)
                      .WithMany(p => p.ProductoImages)
                      .HasForeignKey(d => d.ProductoId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_ProductoImagen_Producto");
            });

            modelBuilder.Entity<Ventas>(entity =>
            {
                entity.HasKey(e => e.VentaId).HasName("PK__Venta__123456");

                entity.ToTable("Ventas");

                entity.Property(e => e.VentaId).HasColumnName("VentaId");
                entity.Property(e => e.ProductoId).HasColumnName("ProductoId");
                entity.Property(e => e.Cantidad).HasColumnName("Cantidad");
                entity.Property(e => e.FechaVenta).HasColumnName("FechaVenta");
                entity.Property(e => e.PrecioVenta).HasColumnName("PrecioVenta");

                entity.HasOne(d => d.Producto)
                      .WithMany(p => p.Ventas)
                      .HasForeignKey(d => d.ProductoId)
                      .HasConstraintName("FK_Venta_Producto");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
