using System.Collections.Generic;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Facades
{
    public interface IDetalleOrdenesRepositorio:IRepositorio<DetalleOrden>
    {
        List<DetalleOrden> GetLista(int OrdenId);
        decimal GetTotal(int OrdenId);
    }
}
