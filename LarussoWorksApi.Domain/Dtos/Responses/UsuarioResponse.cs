using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarussoWorksApi.Domain.Dtos.Responses
{
    public class UsuarioResponse
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string DatosUsuario { get; set; }
        public string SexoUsuario { get; set; }
        public string FechanacUsuario { get; set; }
    }
}