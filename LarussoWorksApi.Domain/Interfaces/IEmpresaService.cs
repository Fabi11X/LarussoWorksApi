using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;

namespace LarussoWorksApi.Domain.Interfaces
{
    public interface IEmpresaService
    {
        bool ValidarCreacion(Empresa nuevaEmpresa);

        bool ValidarActualizacion(Empresa nuevaEmpresa);
    }
}