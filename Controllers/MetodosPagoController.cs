using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    public class MetodosPagoController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public MetodosPagoController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // 1. LEER Y BUSCAR
        public async Task<IActionResult> Index(string buscar)
        {
            var query = _context.MetodosPagos.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(m => m.NombreMetodo.Contains(buscar));
            }

            ViewBag.BuscarActual = buscar;
            return View(await query.ToListAsync());
        }

        // 2. CREAR - GET
        public IActionResult Crear()
        {
            return View();
        }

        // 2. CREAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("NombreMetodo,RequiereReferencia")] MetodosPago metodo)
        {
            if (await _context.MetodosPagos.AnyAsync(m => m.NombreMetodo.ToLower() == metodo.NombreMetodo.ToLower()))
            {
                ModelState.AddModelError("NombreMetodo", "Este método de pago ya existe.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(metodo);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Método de pago registrado correctamente!";
                return RedirectToAction(nameof(Index));
            }
            return View(metodo);
        }

        // 3. EDITAR - GET
        public async Task<IActionResult> Editar(short? id)
        {
            if (id == null) return NotFound();

            var metodo = await _context.MetodosPagos.FindAsync(id);
            if (metodo == null) return NotFound();

            return View(metodo);
        }

        // 3. EDITAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(short id, [Bind("IdMetodoPago,NombreMetodo,RequiereReferencia")] MetodosPago metodo)
        {
            if (id != metodo.IdMetodoPago) return NotFound();

            if (await _context.MetodosPagos.AnyAsync(m => m.NombreMetodo.ToLower() == metodo.NombreMetodo.ToLower() && m.IdMetodoPago != id))
            {
                ModelState.AddModelError("NombreMetodo", "Ya existe otro método con ese nombre.");
            }

            if (ModelState.IsValid)
            {
                _context.Update(metodo);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Método de pago actualizado correctamente!";
                return RedirectToAction(nameof(Index));
            }
            return View(metodo);
        }

        // 4. ELIMINAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(short id)
        {
            var metodo = await _context.MetodosPagos.FindAsync(id);
            if (metodo != null)
            {
                bool tienePagos = await _context.PagosVentas.AnyAsync(p => p.IdMetodoPago == id);
                if (tienePagos)
                {
                    TempData["Error"] = "No se puede eliminar porque ya existen ventas registradas con este método.";
                    return RedirectToAction(nameof(Index));
                }

                _context.MetodosPagos.Remove(metodo);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Método de pago eliminado correctamente!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}