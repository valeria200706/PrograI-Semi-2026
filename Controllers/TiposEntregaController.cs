using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    public class TiposEntregaController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public TiposEntregaController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // 1. LEER Y BUSCAR
        public async Task<IActionResult> Index(string buscar)
        {
            var query = _context.TiposEntregas.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(t => t.NombreTipo.Contains(buscar));
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
        public async Task<IActionResult> Crear([Bind("NombreTipo")] TiposEntrega tipoEntrega)
        {
            if (await _context.TiposEntregas.AnyAsync(t => t.NombreTipo.ToLower() == tipoEntrega.NombreTipo.ToLower()))
            {
                ModelState.AddModelError("NombreTipo", "Este tipo de entrega ya está registrado.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(tipoEntrega);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Tipo de entrega registrado correctamente!";
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEntrega);
        }

        // 3. EDITAR - GET
        public async Task<IActionResult> Editar(short? id)
        {
            if (id == null) return NotFound();

            var tipoEntrega = await _context.TiposEntregas.FindAsync(id);
            if (tipoEntrega == null) return NotFound();

            return View(tipoEntrega);
        }

        // 3. EDITAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(short id, [Bind("IdTipoEntrega,NombreTipo")] TiposEntrega tipoEntrega)
        {
            if (id != tipoEntrega.IdTipoEntrega) return NotFound();

            if (await _context.TiposEntregas.AnyAsync(t => t.NombreTipo.ToLower() == tipoEntrega.NombreTipo.ToLower() && t.IdTipoEntrega != id))
            {
                ModelState.AddModelError("NombreTipo", "Ya existe otro tipo de entrega con ese nombre.");
            }

            if (ModelState.IsValid)
            {
                _context.Update(tipoEntrega);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Tipo de entrega actualizado correctamente!";
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEntrega);
        }

        // 4. ELIMINAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(short id)
        {
            var tipoEntrega = await _context.TiposEntregas.FindAsync(id);
            if (tipoEntrega != null)
            {
                bool tieneOrdenes = await _context.Ordenes.AnyAsync(o => o.IdTipoEntrega == id);
                if (tieneOrdenes)
                {
                    TempData["Error"] = "No se puede eliminar porque existen órdenes asociadas a este tipo de entrega.";
                    return RedirectToAction(nameof(Index));
                }

                _context.TiposEntregas.Remove(tipoEntrega);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Tipo de entrega eliminado correctamente!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}