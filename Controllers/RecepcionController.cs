using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BumburysRestaurant.Models;

namespace BumburysRestaurant.Controllers
{
    public class RecepcionController : Controller
    {
        private readonly BumburysRestaurantDbContext _context;

        public RecepcionController(BumburysRestaurantDbContext context)
        {
            _context = context;
        }

        // GET: Recepcion/Index (Pantalla principal de toma de pedidos)
        public async Task<IActionResult> Index()
        {
            // Cargar categorías y productos disponibles
            var categorias = await _context.Categorias
                .Where(c => c.Activa)
                .OrderBy(c => c.OrdenVisual)
                .ToListAsync();

            var productos = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.Activo && p.Disponible)
                .ToListAsync();

            ViewBag.Categorias = categorias;
            ViewBag.TiposEntrega = await _context.TiposEntregas.ToListAsync();

            return View(productos);
        }

        // POST: Recepcion/GuardarOrden
        [HttpPost]
        public async Task<IActionResult> GuardarOrden([FromBody] CrearOrdenViewModel modelo)
        {
            if (modelo == null || modelo.Detalles == null || !modelo.Detalles.Any())
            {
                return BadRequest(new { mensaje = "El pedido no contiene productos." });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Registrar o buscar cliente si aplica
                int? idCliente = null;
                int? idDireccion = null;

                if (!string.IsNullOrWhiteSpace(modelo.NombreCliente) && !string.IsNullOrWhiteSpace(modelo.TelefonoCliente))
                {
                    var cliente = await _context.Clientes
                        .FirstOrDefaultAsync(c => c.Telefono == modelo.TelefonoCliente);

                    if (cliente == null)
                    {
                        cliente = new Cliente
                        {
                            NombreCliente = modelo.NombreCliente,
                            Telefono = modelo.TelefonoCliente
                        };
                        _context.Clientes.Add(cliente);
                        await _context.SaveChangesAsync();
                    }
                    idCliente = cliente.IdCliente;

                    if (!string.IsNullOrWhiteSpace(modelo.DireccionCliente))
                    {
                        var direccion = new DireccionCliente
                        {
                            IdCliente = cliente.IdCliente,
                            Direccion = modelo.DireccionCliente,
                            Referencia = modelo.ReferenciaDireccion,
                            Activa = true
                        };
                        _context.DireccionClientes.Add(direccion);
                        await _context.SaveChangesAsync();
                        idDireccion = direccion.IdDireccion;
                    }
                }

                // 2. Crear la orden (Clase modelo generada como Ordene)
                var orden = new Ordene
                {
                    IdEstado = 1, // 1 = Pendiente
                    IdTipoEntrega = modelo.IdTipoEntrega,
                    IdUsuario = 1, // Usuario Admin / Cajero por defecto
                    IdCliente = idCliente,
                    IdDireccion = idDireccion,
                    FechaHora = DateTime.Now,
                    ObservacionGeneral = modelo.ObservacionGeneral
                };

                _context.Ordenes.Add(orden);
                await _context.SaveChangesAsync();

                // 3. Crear el detalle de la orden
                foreach (var item in modelo.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(item.IdProducto);
                    if (producto == null) continue;

                    var detalle = new DetalleOrden
                    {
                        IdOrden = orden.IdOrden,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = producto.PrecioActual
                    };

                    _context.DetalleOrdens.Add(detalle);
                    await _context.SaveChangesAsync();

                    // Guardar indicaciones si existen
                    if (!string.IsNullOrWhiteSpace(item.Indicacion))
                    {
                        var indicacion = new IndicacionesDetalle
                        {
                            IdDetalle = detalle.IdDetalle,
                            Indicacion = item.Indicacion
                        };
                        _context.IndicacionesDetalles.Add(indicacion);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { exito = true, idOrden = orden.IdOrden });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { mensaje = "Error al guardar el pedido: " + ex.Message });
            }
        }
    }

    // ViewModels auxiliares
    public class CrearOrdenViewModel
    {
        public short IdTipoEntrega { get; set; }
        public string? NombreCliente { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public string? ReferenciaDireccion { get; set; }
        public string? ObservacionGeneral { get; set; }
        public List<DetalleOrdenViewModel> Detalles { get; set; } = new();
    }

    public class DetalleOrdenViewModel
    {
        public int IdProducto { get; set; }
        public short Cantidad { get; set; }
        public string? Indicacion { get; set; }
    }
}