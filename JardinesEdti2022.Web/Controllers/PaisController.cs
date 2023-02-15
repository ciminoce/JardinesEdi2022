using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdti2022.Web.Models.ViewModels.Pais;

namespace JardinesEdti2022.Web.Controllers
{
    public class PaisController : Controller
    {
        // GET: Pais
        private readonly IPaisesServicios _paisesServicios;
        private readonly IMapper _mapper;

        public PaisController(IPaisesServicios paisesServicios)
        {
            _paisesServicios = paisesServicios;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index()
        {

            var listaPaisesVm = _mapper.Map<List<PaisListVm>>(_paisesServicios.GetLista());

            return View(listaPaisesVm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaisEditVm paisVm)
        {
            if (!ModelState.IsValid)
            {
                return View(paisVm);
            }

            var pais = _mapper.Map<Pais>(paisVm);
            try
            {
                if (_paisesServicios.Existe(pais))
                {
                    ModelState.AddModelError(string.Empty, "País existente!!!");
                    return View(paisVm);
                }
                _paisesServicios.Guardar(pais);
                TempData["msg"] = "País agregado satisfactoriamente!!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar agregar un país";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pais = _paisesServicios.GetEntityPorId(id.Value);
            if (pais == null)
            {
                return HttpNotFound("Código de país inexistente!!!");
            }

            var paisVm = _mapper.Map<PaisEditVm>(pais);
            return View(paisVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaisEditVm paisVm)
        {
            if (!ModelState.IsValid)
            {
                return View(paisVm);
            }

            var pais = _mapper.Map<Pais>(paisVm);
            try
            {
                if (_paisesServicios.Existe(pais))
                {
                    ModelState.AddModelError(string.Empty, "País existente!!!");
                    return View(paisVm);
                }

                _paisesServicios.Guardar(pais);
                TempData["msg"] = "País editado satisfactoriamente!!";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar editar un país";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pais = _paisesServicios.GetEntityPorId(id.Value);
            if (pais == null)
            {
                return HttpNotFound("Código de país inexistente!!!");
            }

            var paisVm = _mapper.Map<PaisListVm>(pais);
            return View(paisVm);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var pais = _paisesServicios.GetEntityPorId(id);
            try
            {
                if (_paisesServicios.EstaRelacionado(pais))
                {
                    ModelState.AddModelError(string.Empty, "País con registros relacionados... Baja denegada");
                    return View(_mapper.Map<PaisListVm>(pais));
                }
                _paisesServicios.Borrar(id);
                TempData["msg"] = "País eliminado satisfactoriamente!!";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar eliminar un país";
                return RedirectToAction("Index");
            }
        }
    }
}