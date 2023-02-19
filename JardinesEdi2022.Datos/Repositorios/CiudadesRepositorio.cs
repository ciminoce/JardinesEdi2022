using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Repositorios
{
    public class CiudadesRepositorio:ICiudadesRepositorio
    {
        private readonly ViveroSqlDbContext _context;

        public CiudadesRepositorio(ViveroSqlDbContext context)
        {
            _context = context;
        }
        public Ciudad GetTEntityPorId(int id)
        {
            try
            {
                return _context.Ciudades.SingleOrDefault(p => p.CiudadId == id);
            }
            catch (Exception e)
            {
                throw new Exception("Error al leer");
            }
        }

        public void Guardar(Ciudad ciudad)
        {
            try
            {
                if (ciudad.Pais!=null)
                {
                    _context.Paises.Attach(ciudad.Pais);

                }
                if (ciudad.CiudadId == 0)
                {
                    //Cuando el id=0 entonces la entidad es nueva ==>alta
                    _context.Ciudades.Add(ciudad);

                }
                else
                {
                    var ciudadInDb =
                        _context.Ciudades.SingleOrDefault(p => p.CiudadId == ciudad.CiudadId);
                    if (ciudadInDb == null)
                    {
                        throw new Exception("Ciudad inexistente");
                    }

                    ciudadInDb.NombreCiudad = ciudad.NombreCiudad;
                    ciudadInDb.PaisId = ciudad.PaisId;
                    _context.Entry(ciudadInDb).State = EntityState.Modified;

                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar guardar un registro");
            }
        }

        public bool Existe(Ciudad ciudad)
        {
            try
            {
                if (ciudad.CiudadId == 0)
                {
                    return _context.Ciudades.Any(c => c.NombreCiudad == ciudad.NombreCiudad && 
                                                      c.PaisId==ciudad.PaisId);
                }

                return _context.Ciudades.Any(c => c.NombreCiudad == ciudad.NombreCiudad
                                                  && c.PaisId==ciudad.PaisId
                                                         && c.CiudadId != ciudad.CiudadId);
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
                return _context.Clientes.Any(c => c.CiudadId == ciudad.CiudadId) ||
                       _context.Proveedores.Any(p => p.CiudadId == ciudad.CiudadId);
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
                return _context.Ciudades.Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Borrar(int id)
        {
            Ciudad ciudadInDb = null;
            try
            {
                ciudadInDb = _context.Ciudades
                    .SingleOrDefault(p => p.CiudadId == id);
                if (ciudadInDb == null) return;
                _context.Entry(ciudadInDb).State = EntityState.Deleted;
                //_context.SaveChanges();
            }
            catch (Exception e)
            {
                _context.Entry(ciudadInDb).State = EntityState.Unchanged;
                throw new Exception(e.Message);
            }
        }


        public List<Ciudad> GetLista()
        {
            try
            {
                return _context.Ciudades.Include(c=>c.Pais).OrderBy(c=>c.Pais.NombrePais)
                    .ThenBy(c=>c.NombreCiudad)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar leer la tabla de Ciudades");
            }
        }

        public List<Ciudad> GetLista(int paisId)
        {
            try
            {
                return _context.Ciudades.Where(c => c.PaisId == paisId)
                    .OrderBy(c => c.NombreCiudad)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar leer la tabla de Ciudades");
            }
        }

        public int GetCantidad(int paisId)
        {
            try
            {
                return _context.Ciudades.Count(c=>c.PaisId==paisId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
