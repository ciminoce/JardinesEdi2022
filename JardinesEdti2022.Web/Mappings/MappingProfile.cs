using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdti2022.Web.Models.ViewModels.Carrito;
using JardinesEdti2022.Web.Models.ViewModels.Categoria;
using JardinesEdti2022.Web.Models.ViewModels.Ciudad;
using JardinesEdti2022.Web.Models.ViewModels.Cliente;
using JardinesEdti2022.Web.Models.ViewModels.Pais;
using JardinesEdti2022.Web.Models.ViewModels.Producto;

namespace JardinesEdti2022.Web.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            LoadPaisesMapping();
            LoadCiudadesMapping();
            LoadCategoriasMapping();
            LoadClientesMapping();
            LoadProductoMapping();
            LoadCarritoMapping();
        }

        private void LoadCarritoMapping()
        {
            CreateMap<Carrito, CarritoListVm>().ForMember(dest => dest.Producto,
                    opt => opt.MapFrom(src => src.Producto.NombreProducto))
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Producto.PrecioUnitario));
        }

        private void LoadProductoMapping()
        {
            CreateMap<Producto, ProductoListVm>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.NombreCategoria));
            CreateMap<Producto, ProductoEditVm>().ReverseMap();
        }

        private void LoadClientesMapping()
        {
            CreateMap<Cliente, ClienteListVm>()
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.NombrePais))
                .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad.NombreCiudad));
            CreateMap<Cliente, ClienteEditVm>().ReverseMap();
            CreateMap<Cliente, ClienteDeleteVm>()
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.NombrePais))
                .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad.NombreCiudad));


        }

        private void LoadCategoriasMapping()
        {
            CreateMap<Categoria, CategoriaListVm>();
            CreateMap<Categoria, CategoriaEditVm>().ReverseMap();

        }

        private void LoadCiudadesMapping()
        {
            CreateMap<Ciudad, CiudadListVm>()
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.NombrePais));
            CreateMap<Ciudad, CiudadEditVm>().ReverseMap();
            CreateMap<Ciudad, CiudadPaisDetailsVm>();
        }

        private void LoadPaisesMapping()
        {
            CreateMap<Pais, PaisListVm>();
            CreateMap<Pais, PaisEditVm>().ReverseMap();
        }
    }
}