using System;
using JardinesEdi2022.Datos;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;

namespace JardinesEdi2022.Servicios.Servicios
{
    public class UsuariosServicios:IUsuariosServicios
    {
        private readonly IUsuariosRepositorio _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public UsuariosServicios(IUsuariosRepositorio repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }
        public Usuario GetUsuarioByEmail(string email)
        {
            try
            {
                return _repositorio.GetUsuarioByEmail(email);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }
        public Usuario GetUsuarioByEmail(string email,string clave)
        {
            try
            {
                return _repositorio.GetUsuarioByEmail(email, clave);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Usuario GetUsuarioById(int usuarioId)
        {
            try
            {
                return _repositorio.GetUsuarioById(usuarioId);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void Guardar(Usuario usuario)
        {
            try
            {
                _repositorio.Guardar(usuario);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
               
                throw;
            }
        }
    }
}
