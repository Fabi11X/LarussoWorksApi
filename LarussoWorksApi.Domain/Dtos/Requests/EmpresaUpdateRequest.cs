using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarussoWorksApi.Domain.Dtos.Requests
{
    public class EmpresaUpdateRequest
    {
        public string NombreEmpresa { get; set; }
        public string LogoEmpresa { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string UbicacionEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
    }
}