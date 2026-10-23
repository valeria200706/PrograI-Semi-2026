using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    public class ProductosController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public ProductosController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // 1. LEER Y BUSCAR (Búsqueda interactiva en servidor o vista)
        public async Task<IActionResult> Index(string buscar)
        {
            var query = _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(p => p.NombreProducto.Contains(buscar) ||
                                         (p.Descripcion != null && p.Descripcion.Contains(buscar)) ||
                                         (p.IdCategoriaNavigation != null && p.IdCategoriaNavigation.NombreCategoria.Contains(buscar)));
            }

            ViewBag.BuscarActual = buscar;
            return View(await query.ToListAsync());
        }

        // 2. INSERTAR - GET
        public async Task<IActionResult> Crear()
        {
            ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "IdCategoria", "NombreCategoria");
            return View();
        }

        // 2. INSERTAR - POST (Con Validaciones)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("NombreProducto,Descripcion,PrecioActual,IdCategoria")] Producto producto)
        {
            // Validación personalizada de servidor: Precio debe ser positivo
            if (producto.PrecioActual <= 0)
            {
                ModelState.AddModelError("PrecioActual", "El precio del producto debe ser un monto mayor a $0.00.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Producto registrado correctamente!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "IdCategoria", "NombreCategoria", producto.IdCategoria);
            return View(producto);
        }

        // 3. ACTUALIZAR - GET
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "IdCategoria", "NombreCategoria", producto.IdCategoria);
            return View(producto);
        }

        // 3. ACTUALIZAR - POST (Con Validaciones)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdProducto,NombreProducto,Descripcion,PrecioActual,IdCategoria")] Producto producto)
        {
            if (id != producto.IdProducto) return NotFound();

            if (producto.PrecioActual <= 0)
            {
                ModelState.AddModelError("PrecioActual", "El precio del producto debe ser un monto mayor a $0.00.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    TempData["Exito"] = "¡Producto actualizado correctamente!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Productos.Any(e => e.IdProducto == producto.IdProducto))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "IdCategoria", "NombreCategoria", producto.IdCategoria);
            return View(producto);
        }

        // 4. ELIMINAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Producto eliminado correctamente!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}