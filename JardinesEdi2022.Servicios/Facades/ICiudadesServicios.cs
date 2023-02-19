using System;
using System.Collections.Generic;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Servicios.Facades
{
    public interface ICiudadesServicios
    {
        Ciudad GetEntityPorId(int id);
        void Guardar(Ciudad ciudad);
        bool Existe(Ciudad ciudad);
        bool EstaRelacionado(Ciudad ciudad);
        int GetCantidad();
        void Borrar(int id);
        List<Ciudad> GetLista();
        List<Ciudad> GetLista(int paisId);
        int GetCantidad(int paisId);
    }
}
