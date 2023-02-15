using System;
using System.Collections.Generic;

namespace JardinesEdi2022.Entidades.Entidades
{
    public class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
