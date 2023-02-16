using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdi2022.Utilidades;
using JardinesEdti2022.Web.Models.ViewModels.Cliente;
using PagedList;

namespace JardinesEdti2022.Web.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        private readonly IClientesServicios _clientesServicios;
        private readonly IPaisesServicios _paisesServicios;
        private readonly ICiudadesServicios _ciudadesServicios;
        private readonly IUsuariosServicios _usuariosServicios;
        private readonly IMapper _mapper;

        public ClienteController(IClientesServicios clientesServicios,
            IPaisesServicios paisesServicios,
            ICiudadesServicios ciudadesServicios, IUsuariosServicios usuariosServicios)
        {
            _clientesServicios = clientesServicios;
            _paisesServicios = paisesServicios;
            _ciudadesServicios = ciudadesServicios;
            _usuariosServicios = usuariosServicios;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index(int? pageSize, int? page)
        {
            var listaClientesVm = _mapper
                .Map<List<ClienteListVm>>(_clientesServicios.GetLista());
            listaClientesVm = listaClientesVm.OrderBy(c => c.Nombres).ThenBy(c => c.Apellido)
                .ToList();
            page = (page ?? 1);
            pageSize = (pageSize ?? 12);
            ViewBag.PageSize = pageSize;
            return View(listaClientesVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {
            var clienteVm = new ClienteEditVm();
            var listaPaises = _paisesServicios.GetLista();

            var paisesDropDown = listaPaises.Select(p => new SelectListItem()
            {
                Text = p.NombrePais,
                Value = p.PaisId.ToString()
            }).ToList();
            clienteVm.Paises = paisesDropDown;
            var listaCiudades = _ciudadesServicios.GetLista();

            var ciudadessDropDown = listaCiudades.Select(c => new SelectListItem()
            {
                Text = c.NombreCiudad,
                Value = c.CiudadId.ToString()
            }).ToList();
            clienteVm.Ciudades = ciudadessDropDown;

            return View(clienteVm);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteEditVm clienteVm)
        {
            if (!ModelState.IsValid)
            {
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

            var cliente = _mapper.Map<Cliente>(clienteVm);
            try
            {
                //Verifico que el mail no exista
                if (_usuariosServicios.GetUsuarioByEmail(cliente.Email) == null)
                {
                    _clientesServicios.Guardar(cliente);
                    //ojo no cambiar el mail del cliente
                    //string clave = HelperUsuario.GenerarClave();
                    string clave = cliente.Email;
                    var usuario = new Usuario()
                    {
                        Correo = cliente.Email,
                        Nombre = cliente.Nombres,
                        Apellido = cliente.Apellido,
                        Activo = true,
                        Rol = WC.CustomerRole,
                        Reestablecer = true,
                        Clave = clave

                    };
                    string asunto = "Creación de Cuenta";
                    string mensaje = $"<h3>Su cuenta se ha creado satisfactoriamente.<h3><br/><p>Su contraseña es {clave}</p>";
                    bool respuesta = HelperUsuario.EnviarCorreo(usuario.Correo, asunto, mensaje);

                    if (respuesta)
                    {
                        try
                        {
                            usuario.Clave = HelperUsuario.ConvertirSha256(clave);
                            _usuariosServicios.Guardar(usuario);
                            TempData["msg"] = "Cliente registrado satisfactoriamente";
                            return RedirectToAction("Index");
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError(string.Empty, e.Message);
                            return View(clienteVm);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "No se pudo enviar el correo!!!");
                        return View(clienteVm);
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Mail existente!!!");
                    return View(clienteVm);
                }

            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Index");
            }
        }


    }
}