using System;
using System.Collections.Generic;

#nullable disable

namespace LarussoWorksApi.Domain.entities
{
    public partial class Mensaje
    {
        public int IdMensaje { get; set; }
        public int IdUsuario { get; set; }
        public int IdEmpleo { get; set; }
        public string BuzonMensaje { get; set; }

        public virtual Empleo IdEmpleoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
