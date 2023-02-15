using System.Collections.Generic;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Servicios.Facades
{
    public interface ICategoriasServicios
    {
        Categoria GetEntityPorId(int id);
        void Guardar(Categoria categoria);
        bool Existe(Categoria categoria);
        bool EstaRelacionado(Categoria categoria);
        int GetCantidad();
        void Borrar(int id);
        List<Categoria> GetLista();
       
    }
}
