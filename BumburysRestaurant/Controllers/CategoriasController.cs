using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public CategoriasController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // 1. LEER Y BUSCAR
        public async Task<IActionResult> Index(string buscar)
        {
            var query = _context.Categorias.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(c => c.NombreCategoria.Contains(buscar) ||
                                         (c.Descripcion != null && c.Descripcion.Contains(buscar)));
            }

            ViewBag.BuscarActual = buscar;
            return View(await query.ToListAsync());
        }

        // 2. INSERTAR - GET
        public IActionResult Crear()
        {
            return View();
        }

        // 2. INSERTAR - POST (Con Validación de Duplicados)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("NombreCategoria,Descripcion")] Categoria categoria)
        {
            // Validación personalizada: evitar nombres de categoría duplicados
            if (await _context.Categorias.AnyAsync(c => c.NombreCategoria.ToLower() == categoria.NombreCategoria.ToLower()))
            {
                ModelState.AddModelError("NombreCategoria", "Ya existe una categoría registrada con este nombre.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Categoría registrada correctamente!";
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // 3. ACTUALIZAR - GET
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();

            return View(categoria);
        }

        // 3. ACTUALIZAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdCategoria,NombreCategoria,Descripcion")] Categoria categoria)
        {
            if (id != categoria.IdCategoria) return NotFound();

            if (await _context.Categorias.AnyAsync(c => c.NombreCategoria.ToLower() == categoria.NombreCategoria.ToLower() && c.IdCategoria != id))
            {
                ModelState.AddModelError("NombreCategoria", "Ya existe otra categoría con este mismo nombre.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    TempData["Exito"] = "¡Categoría actualizada correctamente!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Categorias.Any(e => e.IdCategoria == categoria.IdCategoria))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // 4. ELIMINAR - POST (Con protección de integridad referencial)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoria = await _context.Categorias
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (categoria != null)
            {
                // Validación de integridad: no borrar si tiene productos asignados
                if (categoria.Productos.Any())
                {
                    TempData["Error"] = "No se puede eliminar la categoría porque existen productos asociados a ella.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Categoría eliminada correctamente!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}