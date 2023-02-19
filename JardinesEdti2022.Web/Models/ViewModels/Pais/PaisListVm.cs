using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JardinesEdti2022.Web.Models.ViewModels.Pais
{
    public class PaisListVm
    {
        public int PaisId { get; set; }

        [Display(Name = "País")]
        public string NombrePais { get; set; }

        [Display(Name = "Cant. Ciudades")]
        public int CantidadCiudades { get; set; }
    }
}