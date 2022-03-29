using System;
using System.Collections.Generic;

#nullable disable

namespace LarussoWorksApi.Domain.entities
{
    public partial class MensajesEmpresa
    {
        public int IdMensajeEmpresa { get; set; }
        public int IdEmpresa { get; set; }
        public int IdUsuario { get; set; }
        public string MensajeEmpresa { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
