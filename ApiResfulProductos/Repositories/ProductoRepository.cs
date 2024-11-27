using ApiResfulProductos.Models;

namespace ApiResfulProductos.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly List<Producto> _productos = new List<Producto>();
        private int _nextId = 1;

        public IEnumerable<Producto> ObtenerTodos()
        {
            return _productos;
        }
        public Producto ObtenerPoId(int id)
        {
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return _productos.FirstOrDefault(p => p.Id == id);
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
        public void crear(Producto producto)
        {
            producto.Id = _nextId++;
            _productos.Add(producto);
        }
        public void Actualizar(Producto producto)
        {
            var index = _productos.FindIndex(p => p.Id == producto.Id);

            if (index != -1)
            {
                _productos[index] = producto;
            }
        }
        public void Eliminar(int id)
        {
            var producto = ObtenerPoId(id);

            if (producto != null)
            {
                _productos.Remove(producto);
            }
        }
    }
}
