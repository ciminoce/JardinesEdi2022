using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Facades
{
    public interface IOrdenesRepositorio:IRepositorio<Orden>
    {
        int GetCantidad();
    }
}
