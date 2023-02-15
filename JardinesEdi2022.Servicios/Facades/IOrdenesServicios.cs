using System.Collections.Generic;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Servicios.Facades
{
    public interface IOrdenesServicios
    {
        int GetCantidad();
        void CancelarReservas(List<DetalleOrden> listaItemsCompra);
        void Guardar(Orden orden);
        List<Orden> GetLista();
        Orden GetOrdenPorId(int id);
    }
}
