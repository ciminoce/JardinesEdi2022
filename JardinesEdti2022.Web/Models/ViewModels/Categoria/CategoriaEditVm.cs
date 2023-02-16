using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JardinesEdti2022.Web.Models.ViewModels.Categoria
{
    public class CategoriaEditVm
    {
        public int CategoriaId { get; set; }
        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "La categoría es requerida")]
        [StringLength(50, ErrorMessage = "El campo debe contener entre {2} y  {1} caracteres")]
        public string NombreCategoria { get; set; }
        [Display(Name = "Descripción")]
        [MaxLength(255, ErrorMessage = "El campo no puede contener más de {1} caracteres")]

        public string Descripcion { get; set; }

    }
}