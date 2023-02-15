using System;
using System.Collections.Generic;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Servicios.Facades
{
    public interface IProveedoresServicios
    {
        List<Proveedor> GetLista();
        Proveedor GetEntityPorId(int id);
        void Guardar(Proveedor proveedor);
        bool Existe(Proveedor proveedor);
        bool EstaRelacionado(Proveedor proveedor);
        int GetCantidad();
        void Borrar(int id);
    }
}
