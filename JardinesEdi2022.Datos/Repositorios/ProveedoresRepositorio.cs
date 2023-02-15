using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Repositorios
{
    public class ProveedoresRepositorio:IProveedoresRepositorio
    {
        private readonly ViveroSqlDbContext _context;

        public ProveedoresRepositorio(ViveroSqlDbContext context)
        {
            _context = context;
        }
        public Proveedor GetTEntityPorId(int id)
        {
            try
            {
                return _context.Proveedores
                    .Include(c => c.Pais)
                    .Include(c => c.Ciudad)
                    .SingleOrDefault(c => c.ProveedorId == id);
            }
            catch (Exception e)
            {
                throw new Exception("Error al leer");
            }
        }

        public void Guardar(Proveedor proveedor)
        {
            try
            {
                _context.Paises.Attach(proveedor.Pais);
                _context.Ciudades.Attach(proveedor.Ciudad);
                if (proveedor.ProveedorId == 0)
                {
                    //Cuando el id=0 entonces la entidad es nueva ==>alta
                    _context.Proveedores.Add(proveedor);
                }
                else
                {
                    var proveedorInDb =
                        _context.Proveedores.SingleOrDefault(c => c.ProveedorId == proveedor.ProveedorId);
                    if (proveedorInDb == null)
                    {
                        throw new Exception("Proveedor inexistente");
                    }
                    proveedorInDb.NombreProveedor = proveedor.NombreProveedor;
                    proveedorInDb.CiudadId = proveedor.CiudadId;
                    proveedorInDb.PaisId = proveedor.PaisId;
                    proveedorInDb.CodigoPostal = proveedor.CodigoPostal;
                    proveedorInDb.Direccion = proveedor.Direccion;

                    _context.Entry(proveedorInDb).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar guardar un registro");
            }
        }

        public bool Existe(Proveedor proveedor)
        {
            try
            {
                if (proveedor.ProveedorId == 0)
                {
                    return _context.Proveedores.Any(c => c.NombreProveedor == proveedor.NombreProveedor);
                }

                return _context.Proveedores.Any(c => c.NombreProveedor == proveedor.NombreProveedor
                                                                             && c.ProveedorId != proveedor.ProveedorId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Proveedor proveedor)
        {
            try
            {
                return _context.Productos.Any(p=>p.ProveedorId==proveedor.ProveedorId);
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
                return _context.Proveedores.Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Borrar(int id)
        {
            Proveedor proveedorInDb = null;
            try
            {
                proveedorInDb = _context.Proveedores
                    .SingleOrDefault(c => c.ProveedorId == id);
                if (proveedorInDb == null) return;
                _context.Entry(proveedorInDb).State = EntityState.Deleted;
                //_context.SaveChanges();
            }
            catch (Exception e)
            {
                _context.Entry(proveedorInDb).State = EntityState.Unchanged;
                throw new Exception(e.Message);
            }
        }

        public List<Proveedor> GetLista()
        {
            try
            {
                return _context.Proveedores.OrderBy(c => c.NombreProveedor).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
