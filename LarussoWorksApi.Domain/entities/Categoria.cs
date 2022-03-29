using System;
using System.Collections.Generic;

#nullable disable

namespace LarussoWorksApi.Domain.entities
{
    public partial class Categoria
    {
        public Categoria()
        {
            Empleos = new HashSet<Empleo>();
        }

        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }

        public virtual ICollection<Empleo> Empleos { get; set; }
    }
}
