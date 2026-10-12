using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class MetodosPago
{
    public short IdMetodoPago { get; set; }

    public string NombreMetodo { get; set; } = null!;

    public bool RequiereReferencia { get; set; }

    public virtual ICollection<PagosVenta> PagosVenta { get; set; } = new List<PagosVenta>();
}
