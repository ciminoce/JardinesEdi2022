using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;

namespace JardinesEdti2022.Web.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritosServicios _carritosServicios;

        public CarritoController(ICarritosServicios carritosServicios)
        {
            _carritosServicios = carritosServicios;
        }
        // GET: Carrito
        public ActionResult MostrarCarrito()
        {
            var clienteId = ((Usuario) Session["usuario"]).UsuarioId;
            try
            {
                var listaCarrito = _carritosServicios.GetItemsCarrito(clienteId);
                return View(listaCarrito);
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

        public ActionResult AgregarAlCarrito(int productoId,string returnUrl)
        {
            var clienteId = ((Usuario)Session["usuario"]).UsuarioId;
            try
            {
                _carritosServicios.AgregarAlCarrito(clienteId,productoId);
                TempData["msg"] = "Producto agregado al carrito!!!";
                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar agregar un producto al carrito!!!";
                return Redirect(returnUrl);
            }
        }
    }
}