using System;
using System.Collections.Generic;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Servicios.Facades
{
    public interface IProductosServicios
    {
        List<Producto> GetLista();
        Producto GetEntityPorId(int id);
        void Guardar(Producto producto);
        bool Existe(Producto producto);
        bool EstaRelacionado(Producto producto);
        int GetCantidad();
        void Borrar(int id);
        List<Producto> GetLista(int id);
        void SetearReservarProducto(int productoId, int cantidad);
    }
}
