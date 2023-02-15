using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Facades
{
    public interface IProveedoresRepositorio:IRepositorio<Proveedor>
    {
        bool Existe(Proveedor proveedor);
        bool EstaRelacionado(Proveedor proveedor);
        int GetCantidad();
    }
}
