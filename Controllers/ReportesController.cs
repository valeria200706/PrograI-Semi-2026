using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    [Authorize(Roles = "Administrador,Recepcion,Cajero")]
    public class ReportesController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public ReportesController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // 1. HISTORIAL DE VENTAS (Administrador y Cajero en modo consulta)
        [Authorize(Roles = "Administrador,Cajero")]
        public async Task<IActionResult> HistorialVentas(string buscar)
        {
            var query = _context.PagosVentas
                .Include(p => p.IdMetodoPagoNavigation)
                .Include(p => p.IdOrdenNavigation)
                    .ThenInclude(o => o!.IdTipoEntregaNavigation)
                .Include(p => p.IdOrdenNavigation)
                    .ThenInclude(o => o!.IdUsuarioNavigation)
                .Include(p => p.IdOrdenNavigation)
                    .ThenInclude(o => o!.DetalleOrdens)
                        .ThenInclude(d => d.IdProductoNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(p => p.IdOrden.ToString().Contains(buscar) ||
                                        (p.ReferenciaPago != null && p.ReferenciaPago.Contains(buscar)) ||
                                        (p.IdOrdenNavigation != null && p.IdOrdenNavigation.IdUsuarioNavigation != null &&
                                         p.IdOrdenNavigation.IdUsuarioNavigation.NombreCompleto.Contains(buscar)));
            }

            var ventas = await query.OrderByDescending(p => p.FechaHoraPago).ToListAsync();

            ViewBag.TotalIngresos = ventas.Sum(v => v.MontoAplicado);
            ViewBag.TotalEfectivo = ventas.Where(v => v.IdMetodoPago == 1).Sum(v => v.MontoAplicado);
            ViewBag.TotalTarjeta = ventas.Where(v => v.IdMetodoPago == 2).Sum(v => v.MontoAplicado);
            ViewBag.BuscarActual = buscar;

            return View(ventas);
        }

        // 2. HISTORIAL DE PEDIDOS (Administrador y Recepción)
        [Authorize(Roles = "Administrador,Recepcion")]
        public async Task<IActionResult> HistorialOrdenes(string buscar, short? idEstado)
        {
            var query = _context.Ordenes
                .Include(o => o.IdTipoEntregaNavigation)
                .Include(o => o.IdEstadoNavigation)
                .Include(o => o.IdUsuarioNavigation)
                .Include(o => o.DetalleOrdens)
                    .ThenInclude(d => d.IdProductoNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(o => o.IdOrden.ToString().Contains(buscar) ||
                                        (o.ObservacionGeneral != null && o.ObservacionGeneral.Contains(buscar)) ||
                                        (o.IdUsuarioNavigation != null && o.IdUsuarioNavigation.NombreCompleto.Contains(buscar)));
            }

            if (idEstado.HasValue && idEstado.Value > 0)
            {
                query = query.Where(o => o.IdEstado == idEstado.Value);
            }

            var ordenes = await query.OrderByDescending(o => o.FechaHora).ToListAsync();

            ViewBag.BuscarActual = buscar;
            ViewBag.EstadoActual = idEstado;
            ViewBag.TotalOrdenes = ordenes.Count;
            ViewBag.Pendientes = ordenes.Count(o => o.IdEstado == 1 || o.IdEstado == 2);
            ViewBag.Completadas = ordenes.Count(o => o.IdEstado == 4);

            return View(ordenes);
        }

        // 3. CANCELAR ORDEN (Administrador y Recepción)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Recepcion")]
        public async Task<IActionResult> CancelarOrden(long idOrden)
        {
            var orden = await _context.Ordenes.FindAsync(idOrden);
            if (orden != null && orden.IdEstado != 4)
            {
                orden.IdEstado = 5;
                await _context.SaveChangesAsync();
                TempData["Exito"] = $"La Orden #{idOrden} ha sido cancelada.";
            }
            return RedirectToAction(nameof(HistorialOrdenes));
        }
    }
}