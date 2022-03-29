using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;

namespace LarussoWorksApi.Domain.Interfaces
{
    public interface IEmpleoRepository
    {
        Task<IEnumerable<Empleo>> ObtenerEmpleo();
        Task<Empleo> ObternerEmpleoID (int id);
        Task<int> CrearEmpleo(Empleo nuevoEmpleo);
        Task<bool> ActualizarEmpleo(int id, Empleo actualizarEmpleo);
        Task<bool> EliminarEmpleo(int id);
    }
}