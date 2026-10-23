using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string NombreCliente { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual ICollection<DireccionCliente> DireccionClientes { get; set; } = new List<DireccionCliente>();

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
