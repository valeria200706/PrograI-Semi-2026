using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class TiposEntrega
{
    public short IdTipoEntrega { get; set; }

    public string NombreTipo { get; set; } = null!;

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
