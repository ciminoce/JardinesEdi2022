using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdti2022.Web.Models.ViewModels.Ciudad;
using PagedList;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace JardinesEdti2022.Web.Controllers
{
    public class CiudadController : Controller
    {
        // GET: Ciudad
        private readonly ICiudadesServicios _ciudadesServicios;
        private readonly IPaisesServicios _paisesServicios;
        private readonly IMapper _mapper;

        public CiudadController(ICiudadesServicios ciudadesServicios, IPaisesServicios paisesServicios)
        {
            _ciudadesServicios = ciudadesServicios;
            _paisesServicios = paisesServicios;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index(int? pageSize, int? page)
        {
            var listaCiudadesVm =_mapper.Map<List<CiudadListVm>>(_ciudadesServicios.GetLista());
            listaCiudadesVm = listaCiudadesVm
                .OrderBy(c => c.NombreCiudad)
                .ThenBy(c => c.Pais)
                .ToList();
            page = (page ?? 1);
            pageSize=(pageSize ?? 10);
            ViewBag.PageSize = pageSize;
            return View(listaCiudadesVm.ToPagedList(page.Value,pageSize.Value));
        }

        public ActionResult Create()
        {
            var listaPaises = _paisesServicios.GetLista();
            var paisesDropDown = listaPaises.Select(p => new SelectListItem()
            {
                Text = p.NombrePais,
                Value = p.PaisId.ToString()
            }).ToList();
            var ciudadVm = new CiudadEditVm()
            {
                Paises = paisesDropDown
            };
            return View(ciudadVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                var listaPaises = _paisesServicios.GetLista();
                var paisesDropDown = listaPaises.Select(p => new SelectListItem()
                {
                    Text = p.NombrePais,
                    Value = p.PaisId.ToString()
                }).ToList();
                ciudadVm.Paises = paisesDropDown;

                return View(ciudadVm);
            }

            var ciudad = _mapper.Map<Ciudad>(ciudadVm);
            try
            {
                if (_ciudadesServicios.Existe(ciudad))
                {
                    ModelState.AddModelError(string.Empty,"Ciudad existente!!!");
                    var listaPaises = _paisesServicios.GetLista();

                    var paisesDropDown = listaPaises.Select(p => new SelectListItem()
                    {
                        Text = p.NombrePais,
                        Value = p.PaisId.ToString()
                    }).ToList();
                    ciudadVm.Paises = paisesDropDown;

                    return View(ciudadVm);
                }
                _ciudadesServicios.Guardar(ciudad);
                TempData["msg"] = "Ciudad agregada satisfactoriamente!!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar agregar una ciudad!!";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ciudad = _ciudadesServicios.GetEntityPorId(id.Value);
            if (ciudad==null)
            {
                return HttpNotFound("Código de ciudad erróneo!!!");
            }

            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);
            var listaPaises = _paisesServicios.GetLista();

            var paisesDropDown = listaPaises.Select(p => new SelectListItem()
            {
                Text = p.NombrePais,
                Value = p.PaisId.ToString()
            }).ToList();
            ciudadVm.Paises = paisesDropDown;
            return View(ciudadVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                var listaPaises = _paisesServicios.GetLista();

                var paisesDropDown = listaPaises.Select(p => new SelectListItem()
                {
                    Text = p.NombrePais,
                    Value = p.PaisId.ToString()
                }).ToList();
                ciudadVm.Paises = paisesDropDown;

                return View(ciudadVm);
            }

            var ciudad = _mapper.Map<Ciudad>(ciudadVm);
            try
            {
                if (_ciudadesServicios.Existe(ciudad))
                {
                    ModelState.AddModelError(string.Empty,"Ciudad existente!!!");
                    var listaPaises = _paisesServicios.GetLista();

                    var paisesDropDown = listaPaises.Select(p => new SelectListItem()
                    {
                        Text = p.NombrePais,
                        Value = p.PaisId.ToString()
                    }).ToList();
                    ciudadVm.Paises = paisesDropDown;

                    return View(ciudadVm);
                }
                _ciudadesServicios.Guardar(ciudad);
                TempData["msg"] = "Ciudad editada satisfactoriamente!!!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar editar una ciudad!!!";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ciudad = _ciudadesServicios.GetEntityPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad erróneo!!!");
            }

            var ciudadVm = _mapper.Map<CiudadListVm>(ciudad);
            return View(ciudadVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var ciudad = _ciudadesServicios.GetEntityPorId(id);
                if (_ciudadesServicios.EstaRelacionado(ciudad))
                {
                    var ciudadVm = _mapper.Map<CiudadListVm>(ciudad);
                    ModelState.AddModelError(string.Empty,"Ciudad con registros relacionados... Baja denegada");
                    return View(ciudadVm);
                }
                _ciudadesServicios.Borrar(id);
                TempData["msg"] = "Ciudad eliminada satisfactoriamente!!!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar dar de baja una ciudad";
                return RedirectToAction("Index");
            }
        }

    }
}