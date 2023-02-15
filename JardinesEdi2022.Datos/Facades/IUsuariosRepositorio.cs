using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Facades
{
    public interface IUsuariosRepositorio
    {
        void Guardar(Usuario usuario);
        void Borrar(int id);

        Usuario GetUsuarioByEmail(string email);
        Usuario GetUsuarioByEmail(string email, string clave);

        Usuario GetUsuarioById(int usuarioId);
    }
}
