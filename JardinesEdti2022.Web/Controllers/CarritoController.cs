using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdti2022.Web.Models.ViewModels.Carrito;

namespace JardinesEdti2022.Web.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritosServicios _carritosServicios;
        private readonly IPaisesServicios _paisesServicios;
        private readonly ICiudadesServicios _ciudadesServicios;
        private readonly IMapper _mapper;

        public CarritoController(ICarritosServicios carritosServicios, IPaisesServicios paisesServicios, ICiudadesServicios ciudadesServicios)
        {
            _carritosServicios = carritosServicios;
            _paisesServicios = paisesServicios;
            _ciudadesServicios = ciudadesServicios;
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
    }
}