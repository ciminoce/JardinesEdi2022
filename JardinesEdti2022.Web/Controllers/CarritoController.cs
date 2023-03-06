using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Entidades.Enums;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdi2022.Utilidades;
using JardinesEdti2022.Web.Models.ViewModels.Carrito;

namespace JardinesEdti2022.Web.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritosServicios _carritosServicios;
        private readonly IPaisesServicios _paisesServicios;
        private readonly ICiudadesServicios _ciudadesServicios;
        private readonly IOrdenesServicios _ordenesServicios;
        private readonly IMapper _mapper;

        public CarritoController(ICarritosServicios carritosServicios, IPaisesServicios paisesServicios, ICiudadesServicios ciudadesServicios, IOrdenesServicios ordenesServicios)
        {
            _carritosServicios = carritosServicios;
            _paisesServicios = paisesServicios;
            _ciudadesServicios = ciudadesServicios;
            _ordenesServicios = ordenesServicios;
            _mapper = AutoMapperConfig.Mapper;
        }

        public ActionResult MostrarCarrito()
        {
            var clienteId = ((Usuario) Session["usuario"]).UsuarioId;
            try
            {
                var listaCarrito = _carritosServicios.GetItemsCarrito(clienteId);
                var listaCarritoVm = _mapper.Map<List<CarritoListVm>>(listaCarrito);
                return View(listaCarritoVm);
            }
            catch (Exception e)
            {
                TempData["Error"] = "Error al intentar acceder al carrito";
                return RedirectToAction("IndexCustomer","Producto");
            }
        }

        public JsonResult CantidadEnCarrito()
        {
            var clienteId = ((Usuario) Session["usuario"]).UsuarioId;
            var cantidad = _carritosServicios.CantidadEnCarrito(clienteId);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarAlCarrito(int productoId,string returnUrl)
        {
            var clienteId = ((Usuario)Session["usuario"]).UsuarioId;
            try
            {
                _carritosServicios.AgregarAlCarrito(clienteId,productoId,1);
                TempData["msg"] = "Producto agregado al carrito!!!";
                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar agregar un producto al carrito!!!";
                return Redirect(returnUrl);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarAlCarritoMasDeUno(int productoId, int cantidad)
        {
            var clienteId = ((Usuario)Session["usuario"]).UsuarioId;
            try
            {
                _carritosServicios.AgregarAlCarrito(clienteId, productoId,cantidad);
                TempData["msg"] = "Producto agregado al carrito!!!";
                return RedirectToAction("MostrarCarrito");
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("MostrarCarrito");
            }
        }

        public ActionResult QuitarDelCarrito(int productoId)
        {
            var clienteId = ((Usuario)Session["usuario"]).UsuarioId;
            try
            {
                _carritosServicios.QuitarDelCarrito(clienteId, productoId);
                TempData["msg"] = "Producto eliminado del carrito!!!";
                return RedirectToAction("MostrarCarrito");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar quitar un producto del carrito!!!";
                return RedirectToAction("MostrarCarrito");
            }

        }

        public ActionResult CancelarCompra()
        {
            var clienteId = ((Usuario)Session["usuario"]).UsuarioId;
            try
            {
                _carritosServicios.VaciarCarrito(clienteId);
                
                return RedirectToAction("MostrarCarrito");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar vaciar el carrito!!!";
                return RedirectToAction("MostrarCarrito");
            }

        }

        public ActionResult FinalizarCompra()
        {
            var clienteId = ((Usuario)Session["usuario"]).UsuarioId;
            var listaPaises = _paisesServicios.GetLista();
            var DropDownPaises = listaPaises.Select(p => new SelectListItem()
            {
                Text = p.NombrePais,
                Value = p.PaisId.ToString()
            }).ToList();
            var listaCiudades = _ciudadesServicios.GetLista();
            var DropDownCiudades = listaCiudades.Select(c => new SelectListItem()
            {
                Text = c.NombreCiudad,
                Value = c.CiudadId.ToString()
            }).ToList();

            var carritoVm = new CarritoFinalizarCompra()
            {
                ItemsCarrito = _mapper.Map<List<CarritoListVm>>(_carritosServicios.GetItemsCarrito(clienteId)),
                Paises = DropDownPaises,
                Ciudades = DropDownCiudades
            };
            return View(carritoVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalizarCompra(CarritoFinalizarCompra carritoVm)
        {
            var clienteId = ((Usuario)Session["usuario"]).UsuarioId;
            carritoVm.ItemsCarrito = _mapper.Map<List<CarritoListVm>>(_carritosServicios.GetItemsCarrito(clienteId));
            if (!ModelState.IsValid)
            {
                var listaPaises = _paisesServicios.GetLista();
                var DropDownPaises = listaPaises.Select(p => new SelectListItem()
                {
                    Text = p.NombrePais,
                    Value = p.PaisId.ToString()
                }).ToList();
                var listaCiudades = _ciudadesServicios.GetLista();
                var DropDownCiudades = listaCiudades.Select(c => new SelectListItem()
                {
                    Text = c.NombreCiudad,
                    Value = c.CiudadId.ToString()
                }).ToList();
                carritoVm.Paises = DropDownPaises;
                carritoVm.Ciudades = DropDownCiudades;

                return View(carritoVm);
            }
            //Si está todo OK entonces tengo que crear la orden con sus items
            //Creación del detalle de la orden
            List<DetalleOrden> detalleOrden = new List<DetalleOrden>();
            foreach (var itemCarrito in carritoVm.ItemsCarrito)
            {
                DetalleOrden dOrden = new DetalleOrden()
                {
                    ProductoId = itemCarrito.ProductoId,
                    Cantidad = itemCarrito.Cantidad,
                    PrecioUnitario = itemCarrito.Precio

                };
                detalleOrden.Add(dOrden);
            }
            //Creación de la Orden
            Orden orden = new Orden()
            {
                FechaCompra = DateTime.Now,
                PaisId = carritoVm.PaisEnvioId,
                CiudadId = carritoVm.CiudadEnvioId,
                Direccion = carritoVm.DireccionEnvio,
                CodigoPostal = carritoVm.CodigoPostalEnvio,
                Contacto = carritoVm.ContactoEnvio,
                Telefono = carritoVm.TelefonoEnvio,
                Total = detalleOrden.Sum(d => d.Cantidad * d.PrecioUnitario),
                ClienteId = clienteId,
                DetalleOrdenes = detalleOrden
            };
            try
            {
                /*Simular el cobro mediante un método
                 obteniendo el id de la transacción y el 
                estado de la misma, esto es, si fue aprobada o no*/
                Tuple<string, EstadoOrden> infoPago = HelperPago.ProcesarPago(orden.Total);
                orden.TransaccionId = infoPago.Item1;
                orden.EstadoOrden = infoPago.Item2;
                //Guardo la orden y sus items
                _ordenesServicios.Guardar(orden);
                ViewData["TransacciónId"] = orden.TransaccionId;
                ViewData["Estado"] = orden.EstadoOrden;
                return View("InformacionDelPago");


            }
            catch (Exception e)
            {
                ViewData["TransacciónId"] = orden.TransaccionId;
                ViewData["Estado"] = orden.EstadoOrden;
                return View("InformacionDelPago");
            }
        }

        [HttpGet]
        public JsonResult GetCiudades(int paisId)
        {
            var ciudades = _ciudadesServicios.GetLista(paisId).Select(c => new SelectListItem
            {
                Value = c.CiudadId.ToString(),
                Text = c.NombreCiudad
            }).ToList();

            return Json(ciudades, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MisCompras()
        {
            var clienteId = ((Usuario) Session["usuario"]).UsuarioId;
            try
            {
                var listaOrdenes = _ordenesServicios.GetLista(clienteId);
                return View(listaOrdenes);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return View();
            }
        }
    }
}