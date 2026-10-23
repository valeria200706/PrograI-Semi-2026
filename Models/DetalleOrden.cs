using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class DetalleOrden
{
    public long IdDetalle { get; set; }

    public long IdOrden { get; set; }

    public int IdProducto { get; set; }

    public short Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Ordene IdOrdenNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual ICollection<IndicacionesDetalle> IndicacionesDetalles { get; set; } = new List<IndicacionesDetalle>();
}
