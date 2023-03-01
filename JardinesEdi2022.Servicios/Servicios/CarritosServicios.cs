using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using JardinesEdi2022.Datos;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;

namespace JardinesEdi2022.Servicios.Servicios
{
    public class CarritosServicios : ICarritosServicios
    {
        private readonly ICarritosRepositorio _carritosRepisotorio;
        private readonly IProductosRepositorio _productosRepositorio;

        private readonly IUnitOfWork _unitOfWork;

        public CarritosServicios(ICarritosRepositorio carritosRepositorio, IProductosRepositorio productosRepositorio,
            IUnitOfWork unitOfWork)
        {
            _carritosRepisotorio = carritosRepositorio;
            _productosRepositorio = productosRepositorio;
            _unitOfWork = unitOfWork;
        }

        //public void AgregarAlCarrito(int clienteId, int productoId)
        //{
        //    try
        //    {
        //        using (var tran = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            _carritosRepisotorio.AgregarAlCarrito(clienteId, productoId);
        //            _unitOfWork.Save();
        //            _productosRepositorio.ActualizarStock(productoId, 1);
        //            _unitOfWork.Save();
        //            tran.Complete();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public int CantidadEnCarrito(int clienteId)
        {
            try
            {
                return _carritosRepisotorio.CantidadEnCarrito(clienteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void QuitarDelCarrito(int clienteId, int productoId)
        {
            try
            {
                using (var tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    //obtengo el item
                    var item = _carritosRepisotorio.GetItem(clienteId, productoId);
                    _carritosRepisotorio.QuitarDelCarrito(clienteId, productoId);
                    _unitOfWork.Save();
                    //Agrego al stock lo que había en el carrito
                    _productosRepositorio.ActualizarStock(productoId, -item.Cantidad);
                    _unitOfWork.Save();
                    tran.Complete();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Carrito> GetItemsCarrito(int clienteId)
        {
            try
            {
                return _carritosRepisotorio.GetItemsCarrito(clienteId);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void VaciarCarrito(int clienteId)
        {
            try
            {
                using (var tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    var items = _carritosRepisotorio.GetItemsCarrito(clienteId);
                    foreach (var carrito in items)
                    {
                        _carritosRepisotorio.QuitarDelCarrito(clienteId, carrito.ProductoId);
                        _unitOfWork.Save();
                        _productosRepositorio.ActualizarStock(carrito.ProductoId, -carrito.Cantidad);
                        _unitOfWork.Save();

                    }

                    tran.Complete();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Carrito GetItem(int clienteId, int productoId)
        {
            try
            {
                return _carritosRepisotorio.GetItem(clienteId, productoId);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void AgregarAlCarrito(int clienteId, int productoId, int cantidad=1)
        {
            try
            {
                using (var tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    var productoInDb = _productosRepositorio.GetTEntityPorId(productoId);
                    if (productoInDb.UnidadesEnStock>=cantidad)
                    {
                        _carritosRepisotorio.AgregarAlCarrito(clienteId, productoId,cantidad);
                        _unitOfWork.Save();
                        _productosRepositorio.ActualizarStock(productoId, cantidad);
                        _unitOfWork.Save();
                        tran.Complete();
                        
                    }
                    else
                    {
                        throw new Exception("Stock insuficiente... No se pudo agregar al carrito");
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
