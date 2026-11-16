using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public HomeController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Métricas rápidas para el Dashboard
            ViewBag.TotalProductos = await _context.Productos.CountAsync(p => p.Activo);
            ViewBag.TotalCategorias = await _context.Categorias.CountAsync(c => c.Activa);
            ViewBag.OrdenesPendientes = await _context.Ordenes.CountAsync(o => o.IdEstado == 1 || o.IdEstado == 2);
            ViewBag.VentasTotalesDia = await _context.PagosVentas.SumAsync(p => (decimal?)p.MontoAplicado) ?? 0;

            return View();
        }
    }
}