using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdti2022.Web.Models.ViewModels.Pais;

namespace JardinesEdti2022.Web.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            LoadPaisesMapping();
        }

        private void LoadPaisesMapping()
        {
            CreateMap<Pais, PaisListVm>();
            CreateMap<Pais, PaisEditVm>().ReverseMap();
        }
    }
}