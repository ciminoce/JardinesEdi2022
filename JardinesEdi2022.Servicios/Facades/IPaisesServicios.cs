using System.Collections.Generic;
using System.Linq;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Servicios.Facades
{
    public interface IPaisesServicios
    {
        List<Pais> GetLista();
        Pais GetEntityPorId(int id);
        void Guardar(Pais pais);
        bool Existe(Pais pais);
        bool EstaRelacionado(Pais pais);
        int GetCantidad();
        void Borrar(int id);
    }
}