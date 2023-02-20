using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using JardinesEdti2022.Web.Models.ViewModels.Ciudad;
using PagedList;

namespace JardinesEdti2022.Web.Models.ViewModels.Pais
{
    public class PaisDetailsVm
    {
        public PaisListVm Pais { get; set; }

        public IPagedList<CiudadPaisDetailsVm> Ciudades { get; set; }
    }
}