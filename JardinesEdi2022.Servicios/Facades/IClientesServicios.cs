using System;
using System.Collections.Generic;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Servicios.Facades
{
    public interface IClientesServicios
    {
        List<Cliente> GetLista();

        Cliente GetEntityPorId(int id);
        void Guardar(Cliente cliente);
        bool Existe(Cliente cliente);
        bool EstaRelacionado(Cliente cliente);
        int GetCantidad();
        void Borrar(int id);
    }
}
