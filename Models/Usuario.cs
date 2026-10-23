using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public short IdRol { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
