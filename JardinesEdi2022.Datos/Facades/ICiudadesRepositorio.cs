using JardinesEdi2022.Entidades.Entidades;
using System.Collections.Generic;

namespace JardinesEdi2022.Datos.Facades
{
    public interface ICiudadesRepositorio:IRepositorio<Ciudad>
    {
        bool Existe(Ciudad Ciudad);
        bool EstaRelacionado(Ciudad Ciudad);
        int GetCantidad();
        List<Ciudad> GetLista(int paisId);
    }
}
