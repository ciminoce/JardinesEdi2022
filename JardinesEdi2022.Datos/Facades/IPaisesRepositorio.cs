using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Facades
{
    public interface IPaisesRepositorio:IRepositorio<Pais>
    {
        bool Existe(Pais pais);
        bool EstaRelacionado(Pais pais);
        int GetCantidad();
    }
}
