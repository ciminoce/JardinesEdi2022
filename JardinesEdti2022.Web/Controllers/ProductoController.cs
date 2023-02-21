using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdti2022.Web.Models.ViewModels.Producto;
using PagedList;

namespace JardinesEdti2022.Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        private readonly IProductosServicios _productosServicios;
        private readonly ICategoriasServicios _categoriasServicios;
        private readonly IProveedoresServicios _proveedoresServicios;

        private readonly IMapper _mapper;

        public ProductoController(IProductosServicios productosServicios, ICategoriasServicios categoriasServicios, IProveedoresServicios proveedoresServicios)
        {
            _productosServicios = productosServicios;
            _categoriasServicios = categoriasServicios;
            _proveedoresServicios = proveedoresServicios;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index(int? page, int categoriaId=0)
        {
            page = (page ?? 1);
            var pageSize = 10;
            List<ProductoListVm> listaProductosVm;
            if (categoriaId==0)
            {
                listaProductosVm = _mapper.Map<List<ProductoListVm>>(_productosServicios.GetLista());
                
            }
            else
            {
                listaProductosVm = _mapper.Map<List<ProductoListVm>>(_productosServicios.GetLista(categoriaId));

            }
            var listaCategorias = _categoriasServicios.GetLista();
            var categoriasDropDown = listaCategorias.Select(c => new SelectListItem()
            {
                Text = c.NombreCategoria,
                Value = c.CategoriaId.ToString()
            }).ToList();
            ViewBag.Categorias = categoriasDropDown;
            ViewBag.categoriaId = categoriaId;
            return View(listaProductosVm.ToPagedList(page.Value,pageSize));
        }
    }
}