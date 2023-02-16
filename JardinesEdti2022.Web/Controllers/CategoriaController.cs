using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdti2022.Web.Models.ViewModels.Categoria;
using PagedList;

namespace JardinesEdti2022.Web.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        private readonly ICategoriasServicios _categoriasServicios;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriasServicios categoriasServicios)
        {
            _categoriasServicios = categoriasServicios;
            _mapper = AutoMapperConfig.Mapper;
        }

        public ActionResult Index(int? pageSize, int? page)
        {
            var listaCategoriaVm = _mapper.Map<List<CategoriaListVm>>(_categoriasServicios.GetLista());
            listaCategoriaVm = listaCategoriaVm.OrderBy(c => c.NombreCategoria).ToList();
            page = (page ?? 1);
            pageSize = (pageSize ?? 10);
            return View(listaCategoriaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriaEditVm categoriaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaVm);
            }

            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaVm);
                if (_categoriasServicios.Existe(categoria))
                {
                    ModelState.AddModelError(string.Empty, "Categoría existente!!!");
                    return View(categoriaVm);
                }
                _categoriasServicios.Guardar(categoria);
                TempData["msg"] = "Registro agregado satisfactoriamente!!!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["msg"] = "Error al intentar agregar una categoría!!!";
                return RedirectToAction("Index");
            }



        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var categoria = _categoriasServicios.GetEntityPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound("Código de País inexistente!!!");
            }

            var categoriaVm = _mapper.Map<CategoriaEditVm>(categoria);
            return View(categoriaVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriaEditVm categoriaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaVm);
            }

            var categoria = _mapper.Map<Categoria>(categoriaVm);
            try
            {
                if (_categoriasServicios.Existe(categoria))
                {
                    ModelState.AddModelError(string.Empty, "País existente!!!");
                    return View(categoriaVm);
                }
                _categoriasServicios.Guardar(categoria);
                TempData["msg"] = "País actualizado satisfactoriamente!!!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(categoriaVm);

            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var categoria = _categoriasServicios.GetEntityPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound("Código de País inexistente!!!");
            }

            var categoriaVm = _mapper.Map<CategoriaListVm>(categoria);
            return View(categoriaVm);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                _categoriasServicios.Borrar(id);
                TempData["msg"] = "Registro borrado satisfactoriamente";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Error al intentar borrar el registro";
                return RedirectToAction("Index");
            }
        }

    }
}