using System;
using System.Collections.Generic;
using System.Linq;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Repositorios
{
    public class UsuariosRepositorio:IUsuariosRepositorio
    {
        private readonly ViveroSqlDbContext _context;

        public UsuariosRepositorio(ViveroSqlDbContext context)
        {
            _context = context;
        }
        public Usuario GetTEntityPorId(int id)
        {
            try
            {
                return _context.Usuarios.SingleOrDefault(u => u.UsuarioId == id);
            }
            catch (Exception e)
            {
               
                throw;
            }
        }

        public void Guardar(Usuario usuario)
        {
            if (usuario.UsuarioId==0)
            {
                _context.Usuarios.Add(usuario);
            }
            else
            {
                var usuarioInDb = _context.Usuarios.Find(usuario.UsuarioId);
                usuarioInDb.Activo = usuario.Activo;
                usuarioInDb.Nombre = usuario.Nombre;
                usuarioInDb.Apellido = usuario.Apellido;
                usuarioInDb.Correo = usuario.Correo;
                usuarioInDb.Clave = usuario.Clave;
                usuarioInDb.Reestablecer = usuario.Reestablecer;
                usuarioInDb.Rol = usuario.Rol;
            }
        }


        public void Borrar(int id)
        {
            try
            {
                var usuarioInDb = _context.Usuarios.Find(id);
                usuarioInDb.Activo =false;

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
                return _context.Usuarios.SingleOrDefault(u => u.Correo == email && u.Clave==clave);
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
                return _context.Usuarios.Find(usuarioId);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            try
            {
                return _context.Usuarios.SingleOrDefault(u => u.Correo == email);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }
    }
}
