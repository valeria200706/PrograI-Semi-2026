using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class DireccionCliente
{
    public int IdDireccion { get; set; }

    public int IdCliente { get; set; }

    public string Direccion { get; set; } = null!;

    public string? Referencia { get; set; }

    public bool Activa { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
