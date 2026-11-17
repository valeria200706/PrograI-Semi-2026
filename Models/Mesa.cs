using System;
using System.Collections.Generic;

namespace BumburysRestaurant.Models;

public partial class Mesa
{
    public int IdMesa { get; set; }
    public string NumeroMesa { get; set; } = null!;
    public int Capacidad { get; set; }
    public string EstadoMesa { get; set; } = "Disponible"; // Disponible, Ocupada, Sucia

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}