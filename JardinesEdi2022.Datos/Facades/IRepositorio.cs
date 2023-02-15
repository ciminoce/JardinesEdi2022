using System.Collections.Generic;

namespace JardinesEdi2022.Datos.Facades
{
    public interface IRepositorio<TEntity> where TEntity:class
    {

        List<TEntity> GetLista();
        TEntity GetTEntityPorId(int id);
        void Guardar(TEntity TEntity);
        void Borrar(int id);

    }
}
