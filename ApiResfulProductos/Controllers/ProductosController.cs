using ApiResfulProductos.DTOs;
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
                return NotFound("Producto inexistente, no se encuentra");
            }

            return Ok(producto);
        }

        //POST: api/productos
        [HttpPost]
        public IActionResult CrearProducto([FromBody] CrearProductoDto crearProductoDto)
        {
            
            if (crearProductoDto == null)
            {
                return BadRequest("El producto es null.");
            }

            if (string.IsNullOrWhiteSpace(crearProductoDto.Nombre))
            {
                return BadRequest("El nombre del producto es obligatorio.");
            }

            if (crearProductoDto.Precio <= 0)
            {
                return BadRequest("El precio del producto debe ser mayor a 0.");
            }

            var producto = new Producto
            {
                Nombre = crearProductoDto.Nombre,
                Precio = crearProductoDto.Precio,
            };

            _repository.crear(producto);

            var productoDto = new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
            };

            return CreatedAtAction(nameof(ObtenerProducto), new { id = producto.Id}, productoDto);
            //return Ok(crearProductoDto);
            //return CreatedAtAction(nameof(ObtenerProducto), new {id = producto.Id}, producto);
        }
        //Actualizar registros por ID
        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(int id, [FromBody] Producto producto)
        {
            if (producto == null || producto.Id != id)
            {
                return BadRequest("Ha ocurrido una excepción");
            }

            var productoExistente = _repository.ObtenerPoId(id);

            if (productoExistente == null)
            {
                return NotFound("No se encontro el producto.");
            }

            if(productoExistente != null)
            {
                _repository.Actualizar(producto);
                return Ok("Producto actualizado con éxito.");
            }
            
            return NoContent();
        }

        //Eliminar registros por ID
        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            var productoEliminar = _repository.ObtenerPoId(id);

            if (productoEliminar == null)
            {
                return NotFound($"No se encontro registros con el {id} para borrar.");
            }

            _repository.Eliminar(id);
            return NoContent();
        }

    }
}
