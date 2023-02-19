using System;
using System.Collections.Generic;
using JardinesEdi2022.Datos;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;

namespace JardinesEdi2022.Servicios.Servicios
{
    public class CiudadesServicios:ICiudadesServicios
    {
        private readonly ICiudadesRepositorio _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public CiudadesServicios(ICiudadesRepositorio repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }


        public Ciudad GetEntityPorId(int id)
        {
            try
            {
                return _repositorio.GetTEntityPorId(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Ciudad ciudad)
        {
            try
            {
                _repositorio.Guardar(ciudad);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Existe(Ciudad ciudad)
        {
            try
            {
                return _repositorio.Existe(ciudad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Ciudad ciudad)
        {
            try
            {
                return _repositorio.EstaRelacionado(ciudad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int GetCantidad()
        {
            try
            {
                return _repositorio.GetCantidad();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void Borrar(int id)
        {
            try
            {
                _repositorio.Borrar(id);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<Ciudad> GetLista()
        {
            try
            {
                return _repositorio.GetLista();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Ciudad> GetLista(int paisId)
        {
            try
            {
                return _repositorio.GetLista(paisId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public int GetCantidad(int paisId)
        {
            try
            {
                return _repositorio.GetCantidad(paisId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
