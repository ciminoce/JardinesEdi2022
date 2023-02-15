using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Servicios.Facades
{
    public interface IUsuariosServicios
    {
        Usuario GetUsuarioByEmail(string email);
        void Guardar(Usuario usuario);
        Usuario GetUsuarioByEmail(string email, string clave);
        Usuario GetUsuarioById(int v);
    }
}
