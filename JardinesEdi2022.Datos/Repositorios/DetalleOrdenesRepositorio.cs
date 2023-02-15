using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Repositorios
{
    public class DetalleOrdenesRepositorio:IDetalleOrdenesRepositorio
    {
        private readonly ViveroSqlDbContext _context;

        public DetalleOrdenesRepositorio(ViveroSqlDbContext context)
        {
            _context = context;
        }

        public void Guardar(DetalleOrden detalle)
        {
            try
            {
                _context.DetallesOrdenes.Add(detalle);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<DetalleOrden> GetLista(int OrdenId)
        {
            try
            {
                return _context.DetallesOrdenes
                    .Include(d=>d.Producto)
                    .Where(d => d.OrdenId == OrdenId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public decimal GetTotal(int OrdenId)
        {
            try
            {
                return _context.DetallesOrdenes.Sum(d => d.Cantidad * d.PrecioUnitario);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public List<DetalleOrden> GetLista()
        {
            try
            {
                return _context.DetallesOrdenes.AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public DetalleOrden GetTEntityPorId(int id)
        {
            try
            {
                return _context.DetallesOrdenes
                    .Include(d => d.Producto)
                    .SingleOrDefault(d => d.DetalleOrdenId == id);
            }
            catch (Exception e)
            {
               
                throw;
            }
        }

        public void Borrar(int id)
        {
            try
            {
                var detalle = _context.DetallesOrdenes.SingleOrDefault(d => d.DetalleOrdenId == id);
                _context.Entry(detalle).State = EntityState.Deleted;
            }
            catch (Exception e)
            {
               
                throw;
            }
        }
    }
}
