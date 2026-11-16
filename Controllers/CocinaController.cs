using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
	public class CocinaController : Controller
	{
		private readonly BumburysRestaurantDbContext _context;

		public CocinaController(BumburysRestaurantDbContext context)
		{
			_context = context;
		}

		// GET: Monitor de Cocina
		public async Task<IActionResult> Index()
		{
			// Cargar solo órdenes Pendientes (1) y En Preparación (2)
			var ordenes = await _context.Ordenes
				.Include(o => o.IdTipoEntregaNavigation)
				.Include(o => o.DetalleOrdens)
					.ThenInclude(d => d.IdProductoNavigation) // Traer el nombre del producto
				.Where(o => o.IdEstado == 1 || o.IdEstado == 2)
				.OrderBy(o => o.FechaHora) // Las más antiguas primero
				.ToListAsync();

			return View(ordenes);
		}

		// POST: Avanzar el estado de la orden
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ActualizarEstado(long idOrden, short nuevoEstado)
		{
			var orden = await _context.Ordenes.FindAsync(idOrden);
			if (orden != null)
			{
				orden.IdEstado = nuevoEstado;
				await _context.SaveChangesAsync();
			}

			// Recargar la pantalla de cocina
			return RedirectToAction(nameof(Index));
		}
	}
}