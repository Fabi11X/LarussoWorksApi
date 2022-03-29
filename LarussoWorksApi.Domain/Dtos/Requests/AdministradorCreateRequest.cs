using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarussoWorksApi.Domain.Dtos.Requests
{
    public class AdministradorCreateRequest
    {
        public string NombreAdmin { get; set; }
        public string ApellidoAdmin { get; set; }
        public string SexoAdmin { get; set; }
        public string FechanacAdmin { get; set; }
        public string TelefonoAdmin { get; set; }
        public string CorreoAdmin { get; set; }
        public string ContrasenaAdmin { get; set; }
        public string DireccionAdmin { get; set; }
        public string FotografiaAdmin { get; set; }
        public string NivelAdmin { get; set; }
        
    }
}