using JardinesEdi2022.Entidades.Entidades;
using System.Collections.Generic;

namespace JardinesEdi2022.Datos.Facades
{
    public interface IOrdenesRepositorio:IRepositorio<Orden>
    {
        int GetCantidad();
        List<Orden> GetLista(int clienteId);
    }
}
