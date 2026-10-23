using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public UsuariosController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // 1. LEER Y BUSCAR
        public async Task<IActionResult> Index(string buscar)
        {
            var query = _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(u => u.NombreCompleto.Contains(buscar) ||
                                         u.NombreUsuario.Contains(buscar) ||
                                         (u.IdRolNavigation != null && u.IdRolNavigation.NombreRol.Contains(buscar)));
            }

            ViewBag.BuscarActual = buscar;
            return View(await query.ToListAsync());
        }

        // 2. INSERTAR - GET
        public async Task<IActionResult> Crear()
        {
            ViewBag.Roles = new SelectList(await _context.Roles.ToListAsync(), "IdRol", "NombreRol");
            return View();
        }

        // 2. INSERTAR - POST (Con Validaciones)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("NombreCompleto,NombreUsuario,Contrasena,IdRol")] Usuario usuario)
        {
            // Validar que el nombre de usuario no esté duplicado
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario.ToLower() == usuario.NombreUsuario.ToLower()))
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya está en uso por otra persona.");
            }

            if (ModelState.IsValid)
            {
                usuario.Activo = true;
                usuario.FechaCreacion = DateTime.Now;

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Usuario registrado correctamente!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = new SelectList(await _context.Roles.ToListAsync(), "IdRol", "NombreRol", usuario.IdRol);
            return View(usuario);
        }

        // 3. ACTUALIZAR - GET
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            ViewBag.Roles = new SelectList(await _context.Roles.ToListAsync(), "IdRol", "NombreRol", usuario.IdRol);
            return View(usuario);
        }

        // 3. ACTUALIZAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdUsuario,NombreCompleto,NombreUsuario,Contrasena,IdRol,Activo,FechaCreacion")] Usuario usuario)
        {
            if (id != usuario.IdUsuario) return NotFound();

            // Validar usuario duplicado exceptuando el actual
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario.ToLower() == usuario.NombreUsuario.ToLower() && u.IdUsuario != id))
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya está en uso.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    TempData["Exito"] = "¡Usuario actualizado correctamente!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Usuarios.Any(e => e.IdUsuario == usuario.IdUsuario))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = new SelectList(await _context.Roles.ToListAsync(), "IdRol", "NombreRol", usuario.IdRol);
            return View(usuario);
        }

        // 4. ELIMINAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Usuario eliminado correctamente!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}