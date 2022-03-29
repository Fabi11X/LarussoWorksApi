using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;

namespace LarussoWorksApi.Domain.Interfaces
{
    public interface IAdministradorRepository
    {
        Task<IEnumerable<Administrador>> ObtenerAdmin();
        Task<Administrador> ObternerAdminID (int id);
        Task<int> CrearAdmin(Administrador nuevoAdmin);
        Task<bool> ActualizarAdmin(int id, Administrador actualizarAdmin);
        Task<bool> EliminarAdmin(int id);
        Task<Administrador> Login (string correo, string contrasena);
    }
}