using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JardinesEdti2022.Web.Models.ViewModels.Categoria
{
    public class CategoriaListVm
    {
        public int CategoriaId { get; set; }
        [Display(Name = "Categoría")]
        public string NombreCategoria { get; set; }
        [Display(Name = "Descripción")]

        public string Descripcion { get; set; }

    }
}