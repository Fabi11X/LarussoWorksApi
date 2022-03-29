using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarussoWorksApi.Domain.Dtos.Responses
{
    public class EmpresaResponse
    {
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string LogoEmpresa { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string UbicacionEmpresa { get; set; }
        public string DatosEmpresa { get; set; }
        public string ContrasenaEmpresa { get; set; }
    }
}