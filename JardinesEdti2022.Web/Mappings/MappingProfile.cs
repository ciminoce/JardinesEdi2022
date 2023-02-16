using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdti2022.Web.Models.ViewModels.Ciudad;
using JardinesEdti2022.Web.Models.ViewModels.Pais;

namespace JardinesEdti2022.Web.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            LoadPaisesMapping();
            LoadCiudadesMapping();
        }

        private void LoadCiudadesMapping()
        {
            CreateMap<Ciudad, CiudadListVm>()
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.NombrePais));
            CreateMap<Ciudad, CiudadEditVm>().ReverseMap();
        }

        private void LoadPaisesMapping()
        {
            CreateMap<Pais, PaisListVm>();
            CreateMap<Pais, PaisEditVm>().ReverseMap();
        }
    }
}