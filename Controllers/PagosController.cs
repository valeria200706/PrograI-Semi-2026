using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    public class PagosController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public PagosController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // 1. Mostrar órdenes Listas (Estado = 3) pendientes de cobro
        public async Task<IActionResult> Index()
        {
            var ordenesListas = await _context.Ordenes
                .Include(o => o.IdTipoEntregaNavigation)
                .Include(o => o.DetalleOrdens)
                .Where(o => o.IdEstado == 3)
                .OrderBy(o => o.FechaHora)
                .ToListAsync();

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
                .FirstOrDefaultAsync(o => o.IdOrden == id);

            if (orden == null) return NotFound();

            // Cargar los métodos de pago (Efectivo, Tarjeta, etc.)
            ViewBag.MetodosPago = new SelectList(await _context.MetodosPagos.ToListAsync(), "IdMetodoPago", "NombreMetodo");

            return View(orden);
        }

        // 3. Procesar el pago y finalizar la orden
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarPago(long IdOrden, short IdMetodoPago, decimal MontoRecibido, string? ReferenciaPago)
        {
            var orden = await _context.Ordenes
                .Include(o => o.DetalleOrdens)
                .FirstOrDefaultAsync(o => o.IdOrden == IdOrden);

            if (orden == null) return NotFound();

            decimal totalOrden = orden.DetalleOrdens.Sum(d => d.Cantidad * d.PrecioUnitario);

            if (MontoRecibido < totalOrden)
            {
                TempData["Error"] = "El monto recibido es menor al total a pagar.";
                return RedirectToAction(nameof(Cobrar), new { id = IdOrden });
            }

            // Registrar el pago
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

            // Cambiar estado a Entregado/Finalizado (4)
            orden.IdEstado = 4;
            _context.Update(orden);

            await _context.SaveChangesAsync();

            decimal cambio = MontoRecibido - totalOrden;
            TempData["Exito"] = $"¡Orden #{IdOrden} cobrada con éxito! Cambio a devolver: ${cambio.ToString("0.00")}";

            return RedirectToAction(nameof(Index));
        }
    }
}