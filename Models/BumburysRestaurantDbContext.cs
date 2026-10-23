using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BumburysRestaurant.Models;

public partial class BumburysRestaurantDbContext : DbContext
{
    public BumburysRestaurantDbContext()
    {
    }

    public BumburysRestaurantDbContext(DbContextOptions<BumburysRestaurantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleOrden> DetalleOrdens { get; set; }

    public virtual DbSet<DireccionCliente> DireccionClientes { get; set; }

    public virtual DbSet<EstadosOrden> EstadosOrdens { get; set; }

    public virtual DbSet<IndicacionesDetalle> IndicacionesDetalles { get; set; }

    public virtual DbSet<MetodosPago> MetodosPagos { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<PagosVenta> PagosVentas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TiposEntrega> TiposEntregas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BumburysRestaurantDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__categori__CD54BC5A9558F63A");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.NombreCategoria, "UQ__categori__4EBF6259AD8AACD0").IsUnique();

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Activa)
                .HasDefaultValue(true)
                .HasColumnName("activa");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_categoria");
            entity.Property(e => e.OrdenVisual)
                .HasDefaultValue((short)1)
                .HasColumnName("orden_visual");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__clientes__677F38F58C8A0DEE");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.Telefono, "UQ__clientes__2A16D94556393BD1").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_cliente");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<DetalleOrden>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__detalle___4F1332DEB923DDD1");

            entity.ToTable("detalle_orden");

            entity.HasIndex(e => e.IdOrden, "IX_detalle_orden");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdOrden).HasColumnName("id_orden");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.IdOrden)
                .HasConstraintName("FK_detalle_ordenes");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detalle_productos");
        });

        modelBuilder.Entity<DireccionCliente>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__direccio__25C35D079AD96A15");

            entity.ToTable("direccion_cliente");

            entity.Property(e => e.IdDireccion).HasColumnName("id_direccion");
            entity.Property(e => e.Activa)
                .HasDefaultValue(true)
                .HasColumnName("activa");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Referencia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("referencia");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DireccionClientes)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_direccion_cliente");
        });

        modelBuilder.Entity<EstadosOrden>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__estados___86989FB2F46B90A8");

            entity.ToTable("estados_orden");

            entity.HasIndex(e => e.NombreEstado, "UQ__estados___2F8C637565944397").IsUnique();

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.EstadoFinal).HasColumnName("estado_final");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_estado");
            entity.Property(e => e.OrdenFlujo).HasColumnName("orden_flujo");
        });

        modelBuilder.Entity<IndicacionesDetalle>(entity =>
        {
            entity.HasKey(e => e.IdIndicacion).HasName("PK__indicaci__E58609B638A6C435");

            entity.ToTable("indicaciones_detalle");

            entity.Property(e => e.IdIndicacion).HasColumnName("id_indicacion");
            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Indicacion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("indicacion");

            entity.HasOne(d => d.IdDetalleNavigation).WithMany(p => p.IndicacionesDetalles)
                .HasForeignKey(d => d.IdDetalle)
                .HasConstraintName("FK_indicaciones_detalle");
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__metodos___85BE0EBC45C6215D");

            entity.ToTable("metodos_pago");

            entity.HasIndex(e => e.NombreMetodo, "UQ__metodos___E81160F892A29DC4").IsUnique();

            entity.Property(e => e.IdMetodoPago).HasColumnName("id_metodo_pago");
            entity.Property(e => e.NombreMetodo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_metodo");
            entity.Property(e => e.RequiereReferencia).HasColumnName("requiere_referencia");
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__ordenes__DD5B8F332D7EFB80");

            entity.ToTable("ordenes");

            entity.HasIndex(e => e.IdCliente, "IX_ordenes_cliente");

            entity.HasIndex(e => new { e.IdEstado, e.FechaHora }, "IX_ordenes_estado_fecha");

            entity.Property(e => e.IdOrden).HasColumnName("id_orden");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdDireccion).HasColumnName("id_direccion");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdTipoEntrega).HasColumnName("id_tipo_entrega");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.ObservacionGeneral)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("observacion_general");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_ordenes_clientes");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdDireccion)
                .HasConstraintName("FK_ordenes_direcciones");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ordenes_estados");

            entity.HasOne(d => d.IdTipoEntregaNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdTipoEntrega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ordenes_tipos_entrega");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ordenes_usuarios");
        });

        modelBuilder.Entity<PagosVenta>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__pagos_ve__0941B074018D4D57");

            entity.ToTable("pagos_ventas");

            entity.HasIndex(e => e.IdOrden, "UQ__pagos_ve__DD5B8F32F30F658C").IsUnique();

            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.FechaHoraPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_pago");
            entity.Property(e => e.IdMetodoPago).HasColumnName("id_metodo_pago");
            entity.Property(e => e.IdOrden).HasColumnName("id_orden");
            entity.Property(e => e.MontoAplicado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto_aplicado");
            entity.Property(e => e.MontoRecibido)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto_recibido");
            entity.Property(e => e.ReferenciaPago)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referencia_pago");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.PagosVenta)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pagos_metodos");

            entity.HasOne(d => d.IdOrdenNavigation).WithOne(p => p.PagosVenta)
                .HasForeignKey<PagosVenta>(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pagos_ordenes");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__producto__FF341C0D7E6723D9");

            entity.ToTable("productos");

            entity.HasIndex(e => new { e.IdCategoria, e.Activo }, "IX_productos_categoria");

            entity.HasIndex(e => e.NombreProducto, "UQ__producto__1424FB1D20470B84").IsUnique();

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Disponible)
                .HasDefaultValue(true)
                .HasColumnName("disponible");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.ImagenProducto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen_producto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
            entity.Property(e => e.PrecioActual)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_actual");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_productos_categorias");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__roles__6ABCB5E0F34FACDA");

            entity.ToTable("roles");

            entity.HasIndex(e => e.NombreRol, "UQ__roles__673CB435CC05A339").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<TiposEntrega>(entity =>
        {
            entity.HasKey(e => e.IdTipoEntrega).HasName("PK__tipos_en__B5546E4D92661A88");

            entity.ToTable("tipos_entrega");

            entity.HasIndex(e => e.NombreTipo, "UQ__tipos_en__0C60C00D08F6CA52").IsUnique();

            entity.Property(e => e.IdTipoEntrega).HasColumnName("id_tipo_entrega");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuarios__4E3E04ADA7CC9AF2");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.NombreUsuario, "UQ__usuarios__D4D22D7438050622").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuarios_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
