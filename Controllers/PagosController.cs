using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    [Authorize(Roles = "Administrador,Cajero,Mesero,Recepcion")]
    public class PagosController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public PagosController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // Dashboard inicial para el rol Cajero
        public async Task<IActionResult> PanelCaja()
        {
            ViewBag.PendientesCobro = await _context.Ordenes.CountAsync(o => o.IdEstado == 3);
            ViewBag.CobradasHoy = await _context.PagosVentas.CountAsync(p => p.FechaHoraPago.Date == DateTime.Today);
            return View();
        }

        // 1. Mostrar órdenes Listas (Estado = 3) pendientes de cobro
        public async Task<IActionResult> Index()
        {
            var query = _context.Ordenes
                .Include(o => o.IdTipoEntregaNavigation)
                .Include(o => o.IdUsuarioNavigation)
                .Include(o => o.IdMesaNavigation)
                .Include(o => o.DetalleOrdens)
                .Where(o => o.IdEstado == 3)
                .AsQueryable();

            // Si es Mesero, SOLO puede ver y cobrar las órdenes que él mismo generó
            if (User.IsInRole("Mesero"))
            {
                var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(claimId, out int idMesero))
                {
                    query = query.Where(o => o.IdUsuario == idMesero);
                }
            }

            var ordenesListas = await query.OrderBy(o => o.FechaHora).ToListAsync();
            return View(ordenesListas);
        }

        // 2. Pantalla de facturación para una orden específica
        public async Task<IActionResult> Cobrar(long id)
        {
            var orden = await _context.Ordenes
                .Include(o => o.DetalleOrdens)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Include(o => o.IdClienteNavigation)
                .Include(o => o.IdTipoEntregaNavigation)
                .Include(o => o.IdMesaNavigation)
                .Include(o => o.IdUsuarioNavigation)
                .FirstOrDefaultAsync(o => o.IdOrden == id);

            if (orden == null) return NotFound();

            // Seguridad extra: si es Mesero, verificar que la orden sea suya
            if (User.IsInRole("Mesero"))
            {
                var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(claimId, out int idMesero) && orden.IdUsuario != idMesero)
                {
                    return RedirectToAction("AccesoDenegado", "Account");
                }
            }

            ViewBag.MetodosPago = new SelectList(await _context.MetodosPagos.ToListAsync(), "IdMetodoPago", "NombreMetodo");
            return View(orden);
        }

        // 3. Procesar el pago, finalizar la orden y marcar mesa como Sucia si aplica
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarPago(long IdOrden, short IdMetodoPago, decimal MontoRecibido, string? ReferenciaPago)
        {
            var orden = await _context.Ordenes
                .Include(o => o.DetalleOrdens)
                .Include(o => o.IdMesaNavigation)
                .FirstOrDefaultAsync(o => o.IdOrden == IdOrden);

            if (orden == null) return NotFound();

            decimal totalOrden = orden.DetalleOrdens.Sum(d => d.Cantidad * d.PrecioUnitario);

            if (MontoRecibido < totalOrden)
            {
                TempData["Error"] = "El monto recibido es menor al total a pagar.";
                return RedirectToAction(nameof(Cobrar), new { id = IdOrden });
            }

            var pago = new PagosVenta
            {
                IdOrden = IdOrden,
                IdMetodoPago = IdMetodoPago,
                FechaHoraPago = DateTime.Now,
                MontoAplicado = totalOrden,
                MontoRecibido = MontoRecibido,
                ReferenciaPago = ReferenciaPago
            };
            _context.PagosVentas.Add(pago);

            orden.IdEstado = 4; // Entregado / Pagado
            _context.Update(orden);

            // Si la orden tenía una mesa asignada, pasar la mesa a "Sucia" para limpieza
            if (orden.IdMesaNavigation != null)
            {
                orden.IdMesaNavigation.EstadoMesa = "Sucia";
                _context.Update(orden.IdMesaNavigation);
            }

            await _context.SaveChangesAsync();

            decimal cambio = MontoRecibido - totalOrden;
            string avisoMesa = orden.IdMesaNavigation != null ? $" ({orden.IdMesaNavigation.NumeroMesa} marcada como Sucia para limpieza)" : "";
            TempData["Exito"] = $"¡Orden #{IdOrden} cobrada con éxito! Cambio a devolver: ${cambio.ToString("0.00")}{avisoMesa}";

            return RedirectToAction(nameof(Index));
        }
    }
}