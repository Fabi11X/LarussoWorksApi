using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;

namespace LarussoWorksApi.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> ObtenerEmpresa();
        Task<Empresa> ObternerEmpresaID (int id);
        Task<int> CrearEmpresa(Empresa nuevaEmpresa);
        Task<bool> ActualizarEmpresa(int id, Empresa actualizarEmpresa);
        Task<bool> EliminarEmpresa(int id);
        Task<Empresa> Login (string correo, string contrasena);
    }
}