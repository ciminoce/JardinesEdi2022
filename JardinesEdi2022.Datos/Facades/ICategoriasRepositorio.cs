using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Facades
{
    public interface ICategoriasRepositorio:IRepositorio<Categoria>
    {
        bool Existe(Categoria categoria);
        bool EstaRelacionado(Categoria categoria);
        int GetCantidad();
    }
}
