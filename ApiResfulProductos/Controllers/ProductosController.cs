using ApiResfulProductos.Models;
using ApiResfulProductos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiResfulProductos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _repository;

        public ProductosController(IProductoRepository repository)
        {
            _repository = repository;
        }

        //GET: api/productos
        [HttpGet]
        public IActionResult ObtenerProductos()
        {
            var productos = _repository.ObtenerTodos();
            return Ok(productos);
        }

        //GET: api/productos/{id}
        [HttpGet("{id}")]
        public IActionResult ObtenerProducto(int id)
        {
            var producto = _repository.ObtenerPoId(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        //POST: api/productos
        [HttpPost]
        public IActionResult CrearProducto([FromBody] Producto producto)
        {
            
            if (producto == null)
            {
                return BadRequest("El producto es null.");
            }

            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                return BadRequest("El nombre del producto es obligatorio.");
            }

            if (producto.Precio < 0)
            {
                return BadRequest("El precio del producto debe ser mayor a 0.");
            }

            _repository.crear(producto);
            return CreatedAtAction(nameof(ObtenerProducto), new {id = producto.Id}, producto);
        }

    }
}
