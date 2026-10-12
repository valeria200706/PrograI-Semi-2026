using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class EstadosOrden
{
    public short IdEstado { get; set; }

    public string NombreEstado { get; set; } = null!;

    public short OrdenFlujo { get; set; }

    public bool EstadoFinal { get; set; }

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
