using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class Categoria
{
    public short IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public short OrdenVisual { get; set; }

    public bool Activa { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
