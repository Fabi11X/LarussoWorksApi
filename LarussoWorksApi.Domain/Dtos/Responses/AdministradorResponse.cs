using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarussoWorksApi.Domain.Dtos.Responses
{
    public class AdministradorResponse
    {
        public int IdAdmin { get; set; }
        public string NombreCompletoAdmin { get; set; }
        public string DatosAdmin {get; set;}
        public string SexoAdmin { get; set; }
        public string FechanacAdmin { get; set; }
    }
}