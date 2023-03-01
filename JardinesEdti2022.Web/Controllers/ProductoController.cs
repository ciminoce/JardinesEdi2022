using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdi2022.Utilidades;
using JardinesEdti2022.Web.Models.ViewModels.Producto;
using PagedList;

namespace JardinesEdti2022.Web.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public ActionResult IndexCustomer(int? page, int categoriaId = 0)
        {
            page = (page ?? 1);
            var pageSize = 10;
            List<ProductoListVm> listaProductosVm;
            if (categoriaId == 0)
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
            return View(listaProductosVm.ToPagedList(page.Value, pageSize));
        }


        public ActionResult Create()
        {
            var productoVm = new ProductoEditVm();
            var listaCategorias = _categoriasServicios.GetLista();
            var CategoriasDropDown = listaCategorias.Select(c => new SelectListItem()
            {
                Text = c.NombreCategoria,
                Value = c.CategoriaId.ToString()
            }).ToList();
            var listaProveedores = _proveedoresServicios.GetLista();
            var ProveedoresDropDown = listaProveedores.Select(p => new SelectListItem()
            {
                Text = p.NombreProveedor,
                Value = p.ProveedorId.ToString()
            }).ToList();
            productoVm.Categorias = CategoriasDropDown;
            productoVm.Proveedores = ProveedoresDropDown;

            return View(productoVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoEditVm productoVm)
        {
            if (!ModelState.IsValid)
            {
                var listaCategorias = _categoriasServicios.GetLista();
                var CategoriasDropDown = listaCategorias.Select(c => new SelectListItem()
                {
                    Text = c.NombreCategoria,
                    Value = c.CategoriaId.ToString()
                }).ToList();
                var listaProveedores = _proveedoresServicios.GetLista();
                var ProveedoresDropDown = listaProveedores.Select(p => new SelectListItem()
                {
                    Text = p.NombreProveedor,
                    Value = p.ProveedorId.ToString()
                }).ToList();
                productoVm.Categorias = CategoriasDropDown;
                productoVm.Proveedores = ProveedoresDropDown;
                return View(productoVm);
            }

            var producto = _mapper.Map<Producto>(productoVm);
            try
            {
                if (_productosServicios.Existe(producto))
                {
                    var listaCategorias = _categoriasServicios.GetLista();
                    var CategoriasDropDown = listaCategorias.Select(c => new SelectListItem()
                    {
                        Text = c.NombreCategoria,
                        Value = c.CategoriaId.ToString()
                    }).ToList();
                    var listaProveedores = _proveedoresServicios.GetLista();
                    var ProveedoresDropDown = listaProveedores.Select(p => new SelectListItem()
                    {
                        Text = p.NombreProveedor,
                        Value = p.ProveedorId.ToString()
                    }).ToList();
                    productoVm.Categorias = CategoriasDropDown;
                    productoVm.Proveedores = ProveedoresDropDown;
                    ModelState.AddModelError(string.Empty, "Producto existente!!!");
                    return View(productoVm);

                }

                if (productoVm.ImagenFile!=null)
                {
                    var extension = Path.GetExtension(productoVm.ImagenFile.FileName);
                    var filename = Guid.NewGuid().ToString();
                    FileHelper.UploadPhoto(productoVm.ImagenFile, WC.ProductImageFolder, filename + extension);
                    producto.Imagen = filename + extension;
                }
                _productosServicios.Guardar(producto);
                TempData["msg"] = "Producto agregado satisfactoriamente!!!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar guardar un producto!!!";
                return RedirectToAction("Index");

            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = _productosServicios.GetEntityPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Código de Producto inexistente!!!");
            }

            var productoVm = _mapper.Map<ProductoEditVm>(producto);
            var listaCategorias = _categoriasServicios.GetLista();
            var CategoriasDropDown = listaCategorias.Select(c => new SelectListItem()
            {
                Text = c.NombreCategoria,
                Value = c.CategoriaId.ToString()
            }).ToList();
            var listaProveedores = _proveedoresServicios.GetLista();
            var ProveedoresDropDown = listaProveedores.Select(p => new SelectListItem()
            {
                Text = p.NombreProveedor,
                Value = p.ProveedorId.ToString()
            }).ToList();
            productoVm.Categorias = CategoriasDropDown;
            productoVm.Proveedores = ProveedoresDropDown;

            return View(productoVm);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoEditVm productoVm)
        {

            if (!ModelState.IsValid)
            {
                var listaCategorias = _categoriasServicios.GetLista();
                var CategoriasDropDown = listaCategorias.Select(c => new SelectListItem()
                {
                    Text = c.NombreCategoria,
                    Value = c.CategoriaId.ToString()
                }).ToList();
                var listaProveedores = _proveedoresServicios.GetLista();
                var ProveedoresDropDown = listaProveedores.Select(p => new SelectListItem()
                {
                    Text = p.NombreProveedor,
                    Value = p.ProveedorId.ToString()
                }).ToList();
                productoVm.Categorias = CategoriasDropDown;
                productoVm.Proveedores = ProveedoresDropDown;
                return View(productoVm);
            }
            var producto = _mapper.Map<Producto>(productoVm);
            try
            {
                if (_productosServicios.Existe(producto))
                {
                    var listaCategorias = _categoriasServicios.GetLista();
                    var CategoriasDropDown = listaCategorias.Select(c => new SelectListItem()
                    {
                        Text = c.NombreCategoria,
                        Value = c.CategoriaId.ToString()
                    }).ToList();
                    var listaProveedores = _proveedoresServicios.GetLista();
                    var ProveedoresDropDown = listaProveedores.Select(p => new SelectListItem()
                    {
                        Text = p.NombreProveedor,
                        Value = p.ProveedorId.ToString()
                    }).ToList();
                    productoVm.Categorias = CategoriasDropDown;
                    productoVm.Proveedores = ProveedoresDropDown;
                    ModelState.AddModelError(string.Empty, "Producto existente!!!");
                    return View(productoVm);

                }

                if (productoVm.ImagenFile != null)
                {
                    //borro la imagen anterior
                    var response = FileHelper.DeletePhoto(WC.ProductImageFolder + producto.Imagen);
                    string extension = Path.GetExtension(productoVm.ImagenFile.FileName);
                    string filename = Guid.NewGuid().ToString();

                    var file = $"{filename}{extension}";
                    response = FileHelper.UploadPhoto(productoVm.ImagenFile, WC.ProductImageFolder, file);
                    producto.Imagen = file;

                }
                _productosServicios.Guardar(producto);



                TempData["msg"] = "Producto editado satisfactoriamente!!!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar editar un producto!!!";
                return RedirectToAction("Index");

            }

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = _productosServicios.GetEntityPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Código de Producto inexistente!!!");
            }

            var productoVm = _mapper.Map<ProductoListVm>(producto);

            return View(productoVm);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var producto = _productosServicios.GetEntityPorId(id);
                var productoVm = _mapper.Map<ProductoListVm>(producto);
                if (_productosServicios.EstaRelacionado(producto))
                {
                    ModelState.AddModelError(string.Empty,"Producto relacionado... Baja denegada");
                    return View(productoVm);
                }

                if (System.IO.File.Exists(WC.ProductImageFolder+producto.Imagen))
                {
                    var response = FileHelper.DeletePhoto(WC.ProductImageFolder + producto.Imagen);
                }
                _productosServicios.Borrar(id);
                TempData["msg"] = "Producto borrado satisfactoriamente!!!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = "Error al intentar borrar un producto!!!";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = _productosServicios.GetEntityPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Código de Producto inexistente!!!");
            }

            var productoVm = _mapper.Map<ProductoListVm>(producto);

            return View(productoVm);

        }
    }
}