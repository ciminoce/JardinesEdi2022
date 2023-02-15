using System;
using System.Collections.Generic;
using System.Transactions;
using JardinesEdi2022.Datos;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;

namespace JardinesEdi2022.Servicios.Servicios
{
    public class OrdenesServicios:IOrdenesServicios
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrdenesRepositorio _repositorio;

        private readonly IProductosRepositorio _repositorioProductos;
        private readonly IDetalleOrdenesRepositorio _repositorioDetalles;

        public OrdenesServicios(ViveroSqlDbContext context, IUnitOfWork unitOfWork, IOrdenesRepositorio repositorio, IProductosRepositorio repositorioProductos, IDetalleOrdenesRepositorio repositorioDetalles)
        {
            _unitOfWork = unitOfWork;
            _repositorio = repositorio;
            _repositorioProductos = repositorioProductos;
            _repositorioDetalles = repositorioDetalles;
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

        public void CancelarReservas(List<DetalleOrden> listaItemsCompra)
        {
            try
            {
                foreach (var detalleOrden in listaItemsCompra)
                {
                    _repositorioProductos.SetearReservarProducto(detalleOrden.ProductoId,-detalleOrden.Cantidad);
                }
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Orden orden)
        {

            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    _repositorio.Guardar(orden);
                    _unitOfWork.Save();//Guardo para obtener el Id de la orden
                    foreach (var item in orden.DetalleOrdenes)
                    {
                        item.OrdenId = orden.OrdenId;
                        _repositorioDetalles.Guardar(item);
                        _repositorioProductos.ActualizarStock(item.ProductoId, item.Cantidad);
                    }
                    _unitOfWork.Save();

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Orden> GetLista()
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

        public Orden GetOrdenPorId(int id)
        {
            try
            {
                var orden = _repositorio.GetTEntityPorId(id);
                orden.DetalleOrdenes = _repositorioDetalles.GetLista(id);
                return orden;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
