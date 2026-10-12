using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class PagosVenta
{
    public long IdPago { get; set; }

    public long IdOrden { get; set; }

    public short IdMetodoPago { get; set; }

    public DateTime FechaHoraPago { get; set; }

    public decimal MontoAplicado { get; set; }

    public decimal MontoRecibido { get; set; }

    public string? ReferenciaPago { get; set; }

    public virtual MetodosPago IdMetodoPagoNavigation { get; set; } = null!;

    public virtual Ordene IdOrdenNavigation { get; set; } = null!;
}
