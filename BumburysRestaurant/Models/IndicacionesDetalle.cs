using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class IndicacionesDetalle
{
    public long IdIndicacion { get; set; }

    public long IdDetalle { get; set; }

    public string Indicacion { get; set; } = null!;

    public virtual DetalleOrden IdDetalleNavigation { get; set; } = null!;
}
