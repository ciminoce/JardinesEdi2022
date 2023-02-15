using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Repositorios
{
    public class OrdenesRepositorio:IOrdenesRepositorio
    {
        private readonly ViveroSqlDbContext _context;

        public OrdenesRepositorio(ViveroSqlDbContext context)
        {
            _context = context;
        }
        public List<Orden> GetLista(int registros, int pagina)
        {
            try
            {
                return _context.Ordenes
                    .Include(o=>o.Cliente)
                    .Include(o=>o.DetalleOrdenes)
                    .OrderBy(o => o.OrdenId)
                    .Skip(registros * (pagina - 1))
                    .Take(registros)
                    .AsNoTracking()
                    .ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Orden GetTEntityPorId(int id)
        {
            try
            {
                return _context.Ordenes
                    .Include(o => o.Cliente)
                    .SingleOrDefault(o => o.OrdenId == id);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Orden orden)
        {
            try
            {
                _context.Ordenes.Add(orden);
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
                return _context.Ordenes.Count();
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
                var orden = _context.Ordenes.SingleOrDefault(o => o.OrdenId == id);
                _context.Entry(orden).State = EntityState.Deleted;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public List<Orden> GetLista()
        {
            try
            {
                return _context.Ordenes
                    .Include(o => o.Cliente)
                    .Include(o => o.DetalleOrdenes)
                    .OrderBy(o => o.OrdenId)
                    .AsNoTracking()
                    .ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
