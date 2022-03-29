using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarussoWorksApi.Domain.Dtos.Requests
{
    public class EmpleoUpdateRequest
    {
        public string NombreEmpleo { get; set; }
        public string DescripcionEmpleo { get; set; }
        public string RequisitosEmpleo { get; set; }
        public string PrestacionesEmpleo { get; set; }
        public string UbicacionEmpleo { get; set; }
        public string ImagenEmpleo { get; set; }
        public int IdCategoria { get; set; }
        public int IdEmpresa { get; set; }
    }
}