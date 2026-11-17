using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    [Authorize(Roles = "Administrador,Cajero,Mesero,Recepcion")]
    public class RecepcionController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public RecepcionController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // DASHBOARD DEL MESERO
        [Authorize(Roles = "Mesero,Administrador")]
        public async Task<IActionResult> PanelMesero()
        {
            var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idMesero = int.TryParse(claimId, out int idParsed) ? idParsed : 0;

            ViewBag.Mesas = await _context.Mesas.OrderBy(m => m.IdMesa).ToListAsync();

            var misOrdenesActivas = await _context.Ordenes
                .Include(o => o.IdEstadoNavigation)
                .Include(o => o.IdMesaNavigation)
                .Include(o => o.IdTipoEntregaNavigation)
                .Include(o => o.DetalleOrdens)
                .Where(o => o.IdUsuario == idMesero && (o.IdEstado == 1 || o.IdEstado == 2 || o.IdEstado == 3))
                .OrderByDescending(o => o.FechaHora)
                .ToListAsync();

            ViewBag.MisOrdenesCobradasHoy = await _context.Ordenes
                .CountAsync(o => o.IdUsuario == idMesero && o.IdEstado == 4 && o.FechaHora.Date == DateTime.Today);

            return View(misOrdenesActivas);
        }

        // DASHBOARD DE RECEPCIÓN (SUBGERENCIA / SALA / WEB)
        [Authorize(Roles = "Recepcion,Administrador")]
        public async Task<IActionResult> PanelRecepcion()
        {
            ViewBag.Mesas = await _context.Mesas.OrderBy(m => m.IdMesa).ToListAsync();

            ViewBag.PedidosWeb = await _context.Ordenes
                .Include(o => o.IdClienteNavigation)
                .Include(o => o.DetalleOrdens)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Where(o => o.IdEstado == 6)
                .OrderBy(o => o.FechaHora)
                .ToListAsync();

            ViewBag.OrdenesEnCurso = await _context.Ordenes
                .Include(o => o.IdEstadoNavigation)
                .Include(o => o.IdUsuarioNavigation)
                .Include(o => o.IdMesaNavigation)
                .Include(o => o.IdTipoEntregaNavigation)
                .Where(o => o.IdEstado == 1 || o.IdEstado == 2 || o.IdEstado == 3)
                .OrderByDescending(o => o.FechaHora)
                .ToListAsync();

            return View();
        }

        // PANTALLA DE TOMA DE PEDIDOS (COMANDA Y MENÚ)
        public async Task<IActionResult> Index(int? idMesaPreseleccionada)
        {
            ViewBag.Categorias = await _context.Categorias
                .Where(c => c.Activa)
                .OrderBy(c => c.OrdenVisual)
                .ToListAsync();

            var productos = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.Activo && p.Disponible)
                .ToListAsync();

            ViewBag.TiposEntrega = await _context.TiposEntregas.ToListAsync();
            ViewBag.Mesas = await _context.Mesas.OrderBy(m => m.IdMesa).ToListAsync();
            ViewBag.MesaPreseleccionada = idMesaPreseleccionada;

            ViewBag.OrdenesEnCurso = await _context.Ordenes
                .Include(o => o.IdEstadoNavigation)
                .Include(o => o.IdTipoEntregaNavigation)
                .Include(o => o.IdUsuarioNavigation)
                .Include(o => o.IdMesaNavigation)
                .Where(o => o.IdEstado == 1 || o.IdEstado == 2 || o.IdEstado == 3)
                .OrderByDescending(o => o.FechaHora)
                .ToListAsync();

            ViewBag.PedidosWeb = await _context.Ordenes
                .Include(o => o.IdClienteNavigation)
                .Include(o => o.DetalleOrdens)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Where(o => o.IdEstado == 6)
                .OrderBy(o => o.FechaHora)
                .ToListAsync();

            return View(productos);
        }

        // POST: Recepcion/GuardarOrden
        [HttpPost]
        public async Task<IActionResult> GuardarOrden([FromBody] CrearOrdenViewModel modelo)
        {
            if (modelo == null || modelo.Detalles == null || !modelo.Detalles.Any())
            {
                return BadRequest(new { mensaje = "El pedido no contiene productos." });
            }

            var claimIdUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idUsuarioLogueado = int.TryParse(claimIdUsuario, out int idParsed) ? idParsed : 1;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int? idCliente = null;
                int? idDireccion = null;

                if (!string.IsNullOrWhiteSpace(modelo.NombreCliente) && !string.IsNullOrWhiteSpace(modelo.TelefonoCliente))
                {
                    var cliente = await _context.Clientes
                        .FirstOrDefaultAsync(c => c.Telefono == modelo.TelefonoCliente);

                    if (cliente == null)
                    {
                        cliente = new Cliente
                        {
                            NombreCliente = modelo.NombreCliente,
                            Telefono = modelo.TelefonoCliente
                        };
                        _context.Clientes.Add(cliente);
                        await _context.SaveChangesAsync();
                    }
                    idCliente = cliente.IdCliente;

                    if (!string.IsNullOrWhiteSpace(modelo.DireccionCliente))
                    {
                        var direccion = new DireccionCliente
                        {
                            IdCliente = cliente.IdCliente,
                            Direccion = modelo.DireccionCliente,
                            Referencia = modelo.ReferenciaDireccion,
                            Activa = true
                        };
                        _context.DireccionClientes.Add(direccion);
                        await _context.SaveChangesAsync();
                        idDireccion = direccion.IdDireccion;
                    }
                }

                if (modelo.IdMesa.HasValue && modelo.IdMesa.Value > 0)
                {
                    var mesa = await _context.Mesas.FindAsync(modelo.IdMesa.Value);
                    if (mesa != null)
                    {
                        mesa.EstadoMesa = "Ocupada";
                    }
                }

                var orden = new Ordene
                {
                    IdEstado = 1,
                    IdTipoEntrega = modelo.IdTipoEntrega,
                    IdUsuario = idUsuarioLogueado,
                    IdMesa = (modelo.IdMesa.HasValue && modelo.IdMesa.Value > 0) ? modelo.IdMesa.Value : null,
                    IdCliente = idCliente,
                    IdDireccion = idDireccion,
                    FechaHora = DateTime.Now,
                    ObservacionGeneral = modelo.ObservacionGeneral
                };

                _context.Ordenes.Add(orden);
                await _context.SaveChangesAsync();

                foreach (var item in modelo.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(item.IdProducto);
                    if (producto == null) continue;

                    var detalle = new DetalleOrden
                    {
                        IdOrden = orden.IdOrden,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = producto.PrecioActual
                    };

                    _context.DetalleOrdens.Add(detalle);
                    await _context.SaveChangesAsync();

                    if (!string.IsNullOrWhiteSpace(item.Indicacion))
                    {
                        _context.IndicacionesDetalles.Add(new IndicacionesDetalle
                        {
                            IdDetalle = detalle.IdDetalle,
                            Indicacion = item.Indicacion
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { exito = true, idOrden = orden.IdOrden });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { mensaje = "Error al guardar el pedido: " + ex.Message });
            }
        }

        // POST: Cambiar Estado de Mesa (Recepción y Admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Recepcion")]
        public async Task<IActionResult> CambiarEstadoMesa(int idMesa, string nuevoEstado, string? returnUrl)
        {
            var mesa = await _context.Mesas.FindAsync(idMesa);
            if (mesa != null && (nuevoEstado == "Disponible" || nuevoEstado == "Ocupada" || nuevoEstado == "Sucia"))
            {
                mesa.EstadoMesa = nuevoEstado;
                await _context.SaveChangesAsync();
                TempData["Exito"] = $"{mesa.NumeroMesa} actualizada a: {nuevoEstado}.";
            }
            if (!string.IsNullOrEmpty(returnUrl)) return LocalRedirect(returnUrl);
            return RedirectToAction(nameof(PanelRecepcion));
        }

        // POST: Marcar Servida en Mesa (Acción directa sin vista extra, solo Mesero)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mesero")]
        public async Task<IActionResult> MarcarServida(long idOrden, string? returnUrl)
        {
            var orden = await _context.Ordenes.FindAsync(idOrden);
            if (orden != null && orden.IdEstado == 3)
            {
                string obs = orden.ObservacionGeneral ?? "";
                if (!obs.Contains("[SERVIDA EN MESA]"))
                {
                    orden.ObservacionGeneral = string.IsNullOrWhiteSpace(obs)
                        ? "[SERVIDA EN MESA]"
                        : $"{obs} | [SERVIDA EN MESA]";
                }
                await _context.SaveChangesAsync();
                TempData["Exito"] = $"Orden #{idOrden} marcada como servida en mesa. Ya aparece registrada para Recepción y Caja.";
            }
            if (!string.IsNullOrEmpty(returnUrl)) return LocalRedirect(returnUrl);
            return RedirectToAction(nameof(PanelMesero));
        }

        // POST: Aprobar Pedido Web (Recepción)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Recepcion")]
        public async Task<IActionResult> AprobarPedidoWeb(long idOrden, string? returnUrl)
        {
            var orden = await _context.Ordenes.FindAsync(idOrden);
            if (orden != null && orden.IdEstado == 6)
            {
                orden.IdEstado = 1;
                await _context.SaveChangesAsync();
                TempData["Exito"] = $"Pedido Web #{idOrden} aprobado y enviado a Cocina.";
            }
            if (!string.IsNullOrEmpty(returnUrl)) return LocalRedirect(returnUrl);
            return RedirectToAction(nameof(PanelRecepcion));
        }
    }

    public class CrearOrdenViewModel
    {
        public short IdTipoEntrega { get; set; }
        public int? IdMesa { get; set; }
        public string? NombreCliente { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public string? ReferenciaDireccion { get; set; }
        public string? ObservacionGeneral { get; set; }
        public List<DetalleOrdenViewModel> Detalles { get; set; } = new();
    }

    public class DetalleOrdenViewModel
    {
        public int IdProducto { get; set; }
        public short Cantidad { get; set; }
        public string? Indicacion { get; set; }
    }
}