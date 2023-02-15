using JardinesEdi2022.Entidades.Entidades;
using System.Collections.Generic;

namespace JardinesEdi2022.Datos.Facades
{
    public interface IProductosRepositorio:IRepositorio<Producto>
    {
        bool Existe(Producto producto);
        bool EstaRelacionado(Producto producto);
        int GetCantidad();
        void SetearReservarProducto(int productoId, int cantidad);
        void ActualizarStock(int productoId, int cantidad);
        List<Producto> GetLista(int categoriaId);
    }
}
