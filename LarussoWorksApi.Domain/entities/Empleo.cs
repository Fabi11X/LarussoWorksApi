using System;
using System.Collections.Generic;

#nullable disable

namespace LarussoWorksApi.Domain.entities
{
    public partial class Empleo
    {
        public Empleo()
        {
            Mensajes = new HashSet<Mensaje>();
        }

        public int IdEmpleo { get; set; }
        public string NombreEmpleo { get; set; }
        public string DescripcionEmpleo { get; set; }
        public string RequisitosEmpleo { get; set; }
        public string PrestacionesEmpleo { get; set; }
        public string UbicacionEmpleo { get; set; }
        public string ImagenEmpleo { get; set; }
        public int IdCategoria { get; set; }
        public int IdEmpresa { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
    }
}
