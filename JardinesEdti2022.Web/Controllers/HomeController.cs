using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JardinesEdi2022.Servicios.Facades;

namespace JardinesEdti2022.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly  ICategoriasServicios _categoriasServicios;
        private readonly IProveedoresServicios _proveedoresServicios;
        private readonly IClientesServicios _clientesServicios;
        //private readonly IOrdenesServicios _ordenesServicios;

        public HomeController(ICategoriasServicios categoriasServicios, IProveedoresServicios proveedoresServicios, IClientesServicios clientesServicios)
        {
            _categoriasServicios = categoriasServicios;
            _proveedoresServicios = proveedoresServicios;
            _clientesServicios = clientesServicios;
            //_ordenesServicios = ordenesServicios;
        }
        public ActionResult Index()
        {
            ViewBag.CantidadCategorias = _categoriasServicios.GetCantidad();
            ViewBag.CantidadProductos = _proveedoresServicios.GetCantidad();
            ViewBag.CantidadClientes = _clientesServicios.GetCantidad();
            //ViewBag.CantidadVentas = _ordenesServicios.GetCantidad();
            ViewBag.CantidadVentas = 0;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}