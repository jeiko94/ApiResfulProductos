//repositorio que almacene los productos en memoria.
using ApiResfulProductos.Models;

namespace ApiResfulProductos.Repositories
{
    public interface IProductoRepository
    {
        IEnumerable<Producto> ObtenerTodos();
        Producto ObtenerPoId(int id);
        void crear(Producto producto);
        void Actualizar(Producto producto);
        void Eliminar(int id);
    }
}
