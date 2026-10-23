using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class Ordene
{
    public long IdOrden { get; set; }

    public short IdEstado { get; set; }

    public short IdTipoEntrega { get; set; }

    public int IdUsuario { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDireccion { get; set; }

    public DateTime FechaHora { get; set; }

    public string? ObservacionGeneral { get; set; }

    public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; } = new List<DetalleOrden>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual DireccionCliente? IdDireccionNavigation { get; set; }

    public virtual EstadosOrden IdEstadoNavigation { get; set; } = null!;

    public virtual TiposEntrega IdTipoEntregaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual PagosVenta? PagosVenta { get; set; }
}
