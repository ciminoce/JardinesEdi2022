using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdi2022.Utilidades;
using JardinesEdti2022.Web.Models.ViewModels.Cliente;

namespace JardinesEdti2022.Web.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        private readonly IUsuariosServicios _usuariosServicios;
        private readonly IPaisesServicios _paisesServicios;
        private readonly ICiudadesServicios _ciudadesServicios;

        private readonly IMapper _mapper;

        public AccesoController(IUsuariosServicios usuariosServicios, IPaisesServicios paisesServicios, ICiudadesServicios ciudadesServicios)
        {
            _usuariosServicios = usuariosServicios;
            _paisesServicios = paisesServicios;
            _ciudadesServicios = ciudadesServicios;
            _mapper = AutoMapperConfig.Mapper;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string clave)
        {
            var usuario = _usuariosServicios.GetUsuarioByEmail(email);
            if (usuario==null)
            {
                ViewBag.Error = "Usuario no registrado!!!";
                return View();
            }

            if (!usuario.Activo)
            {
                ViewBag.Error = "Usuario no activo!!!";
                return View();

            }
            string claveConvertida = HelperUsuario.ConvertirSha256(clave);
            if (usuario.Clave!=claveConvertida)
            {
                ViewBag.Error = "Clave errónea!!!";
                return View();

            }

            if (usuario.Reestablecer)
            {
                TempData["usuarioId"] = usuario.UsuarioId;
                return RedirectToAction("ResetPassword");
            }

            TempData["usuarioId"] = null;
            Session["usuario"] = usuario;
            FormsAuthentication.SetAuthCookie(usuario.Correo, false);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            var clienteVm = new ClienteEditVm();
            clienteVm.Paises = _paisesServicios.GetLista().Select(p => new SelectListItem
            {
                Value = p.PaisId.ToString(),
                Text = p.NombrePais
            }).ToList();
            clienteVm.Ciudades = _ciudadesServicios.GetLista().Select(c => new SelectListItem
            {
                Value = c.CiudadId.ToString(),
                Text = c.NombreCiudad
            }).ToList();

            return View(clienteVm);
        }

        public ActionResult PasswordRecovery()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string usuarioId, string clave, string nuevaClave, string confirmarClave)
        {
            var usuario = _usuariosServicios.GetUsuarioById(Convert.ToInt32(usuarioId));
            string claveConvertida = HelperUsuario.ConvertirSha256(clave);
            if (usuario.Clave!=claveConvertida)
            {
                ViewBag.Error = "Clave actual errónea!!!";
                TempData["usuarioId"] = usuario.UsuarioId;
                return View();
            }else if (nuevaClave!=confirmarClave)
            {
                ViewBag.Error = "La nueva clave y su confirmación no coinciden!!";
                TempData["usurioId"] = usuario.UsuarioId;
                TempData["clave"] = clave;
                return View();
            }

            string nuevaClaveConvertida = HelperUsuario.ConvertirSha256(nuevaClave);
            usuario.Reestablecer = false;
            usuario.Clave = nuevaClaveConvertida;
            _usuariosServicios.Guardar(usuario);
            return RedirectToAction("Login");
        }
    }
}