using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;

namespace LarussoWorksApi.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerUsuario();
        Task<Usuario> ObternerUsuarioID (int id);
        Task<int> CrearUsuario(Usuario nuevoUsuario);
        Task<bool> ActualizarUsuario(int id, Usuario actualizarUsuario);
        Task<bool> EliminarUsuario(int id);
        Task<Usuario> Login (string correo, string contrasena);
    }
}