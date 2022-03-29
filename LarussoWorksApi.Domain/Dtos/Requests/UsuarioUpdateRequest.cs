using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarussoWorksApi.Domain.Dtos.Requests
{
    public class UsuarioUpdateRequest
    {
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string SexoUsuario { get; set; }
        public string FechanacUsuario { get; set; }
        public string TelefonoUsuario { get; set; }
        public string FotografiaUsuario { get; set; }
        public string ObjProUsuario { get; set; }
        public string ExperienciaUsuario { get; set; }
        public string HabilidadUsuario { get; set; }
        public string EducacionUsuario { get; set; }
    }
}