/* ============================================================================
   CREDENCIALES DE ACCESO AL SISTEMA - BUMBURY'S RESTAURANT
   ============================================================================
   Rol             | Usuario       | Contraseña | Vista / Panel Principal
   ----------------|---------------|------------|-----------------------------
   Administrador   | admin         | 123456     | /Home/Index (Dashboard)
   Recepción       | recepcion     | 123456     | /Recepcion/PanelRecepcion
   Mesero          | mesero        | 123456     | /Recepcion/PanelMesero
   Cajero          | cajero        | 123456     | /Pagos/PanelCaja
   Cocina          | cocina        | 123456     | /Cocina/Index
   ============================================================================ */

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public AccountController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var rolActual = User.FindFirstValue(ClaimTypes.Role);
                return RedirigirSegunRol(rolActual);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string? nombreUsuario, string? contrasena, string? username, string? password, string? returnUrl = null)
        {
            string userFinal = (nombreUsuario ?? username ?? "").Trim();
            string passFinal = (contrasena ?? password ?? "").Trim();

            if (string.IsNullOrWhiteSpace(userFinal) || string.IsNullOrWhiteSpace(passFinal))
            {
                ViewBag.Error = "Por favor, ingrese su usuario y contraseña.";
                return View();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.NombreUsuario.ToLower() == userFinal.ToLower() && u.Contrasena == passFinal);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

            if (!usuario.Activo)
            {
                ViewBag.Error = "Esta cuenta se encuentra desactivada. Contacte al administrador.";
                return View();
            }

            string nombreRol = usuario.IdRolNavigation?.NombreRol ?? "Recepcion";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim("Username", usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, nombreRol)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirigirSegunRol(nombreRol);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        // GET: Account/AccesoDenegado
        [AllowAnonymous]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        private IActionResult RedirigirSegunRol(string? rol)
        {
            return rol?.ToLower() switch
            {
                "cocina" or "cocinero" => RedirectToAction("Index", "Cocina"),
                "cajero" => RedirectToAction("PanelCaja", "Pagos"),
                "mesero" => RedirectToAction("PanelMesero", "Recepcion"),
                "recepcion" => RedirectToAction("PanelRecepcion", "Recepcion"),
                _ => RedirectToAction("Index", "Home")
            };
        }
    }
}