using System;
using System.Collections.Generic;

#nullable disable

namespace LarussoWorksApi.Domain.entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            Mensajes = new HashSet<Mensaje>();
            MensajesEmpresas = new HashSet<MensajesEmpresa>();
        }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string SexoUsuario { get; set; }
        public string FechanacUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string ContrasenaUsuario { get; set; }
        public string TelefonoUsuario { get; set; }
        public string FotografiaUsuario { get; set; }
        public string NivelUsuario { get; set; }
        public string ObjProUsuario { get; set; }
        public string ExperienciaUsuario { get; set; }
        public string HabilidadUsuario { get; set; }
        public string EducacionUsuario { get; set; }

        public virtual ICollection<Mensaje> Mensajes { get; set; }
        public virtual ICollection<MensajesEmpresa> MensajesEmpresas { get; set; }
    }
}
