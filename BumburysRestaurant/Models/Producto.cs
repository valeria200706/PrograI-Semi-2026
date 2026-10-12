using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public short IdCategoria { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? ImagenProducto { get; set; }

    public decimal PrecioActual { get; set; }

    public bool Disponible { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; } = new List<DetalleOrden>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}
