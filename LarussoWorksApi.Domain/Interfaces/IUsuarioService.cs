using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;

namespace LarussoWorksApi.Domain.Interfaces
{
    public interface IUsuarioService
    {
        bool ValidarCreacion(Usuario nuevoUsuario);

        bool ValidarActualizacion(Usuario nuevoUsuario);
    }
}