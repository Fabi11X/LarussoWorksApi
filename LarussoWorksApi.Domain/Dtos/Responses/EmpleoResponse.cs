using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarussoWorksApi.Domain.Dtos.Responses
{
    public class EmpleoResponse
    {
        public int IdEmpleo { get; set; }
        public string NombreEmpleo { get; set; }
        public string DescripcionEmpleo { get; set; }
        public string DatosEmpleo { get; set; }
        public string UbicacionEmpleo { get; set; }
    }
}