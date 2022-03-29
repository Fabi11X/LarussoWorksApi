using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;
using LarussoWorksApi.Infraestructure.Data;
using LarussoWorksApi.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using LarussoWorksApi.Domain.Interfaces;

namespace LarussoWorksApi.Infraestructure.Repository
{
    public class UsuarioSqlRepository : IUsuarioRepository
    {

        private readonly LarussoWorksFinallyContext _context;

        public UsuarioSqlRepository(LarussoWorksFinallyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuario()
        {
            var usuario = _context.Usuarios.Select(u => u);
            return await usuario.ToListAsync();
        }

        public async Task<Usuario> ObternerUsuarioID (int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);
            return usuario;
        }

        public async Task<int> CrearUsuario (Usuario nuevoUsuario)
        {
            var dato = nuevoUsuario;
            await _context.Usuarios.AddAsync(dato);
            var filas = await _context.SaveChangesAsync();
            if(filas <= 0)
            throw new Exception("Error al registrar el Usuario.");
            return dato.IdUsuario;
        }

        public async Task<bool> ActualizarUsuario (int id, Usuario actualizarUsuario)
        {
            if (id <= 0 || actualizarUsuario == null)
            {
                throw new ArgumentException("Información incompleta, te faltan datos.");
            }
            var dato = await ObternerUsuarioID(id);
            dato.NombreUsuario = actualizarUsuario.NombreUsuario;
            dato.ApellidoUsuario = actualizarUsuario.ApellidoUsuario;
            dato.SexoUsuario = actualizarUsuario.SexoUsuario;
            dato.FechanacUsuario = actualizarUsuario.FechanacUsuario;
            dato.TelefonoUsuario = actualizarUsuario.TelefonoUsuario;
            dato.FotografiaUsuario = actualizarUsuario.FotografiaUsuario;
            dato.ObjProUsuario = actualizarUsuario.ObjProUsuario;
            dato.ExperienciaUsuario = actualizarUsuario.ExperienciaUsuario;
            dato.HabilidadUsuario = actualizarUsuario.HabilidadUsuario;
            dato.EducacionUsuario = actualizarUsuario.EducacionUsuario;
            _context.Update(dato);
            var row = await _context.SaveChangesAsync();
            return row > 0;
        }
        public async Task<bool> EliminarUsuario(int id)
        {
            if(id <= 0)
            {
                throw  new ArgumentException("El Usuario no existe en la base, intente con otro que si exista.");
            }
            var dato = await ObternerUsuarioID(id);
            _context.Remove(dato);
            var row = await _context.SaveChangesAsync();
            return row > 0; 
        }

        public async Task<Usuario> Login (string correo, string contrasena)
        {
            var loginU = await _context.Usuarios.Where(u => u.CorreoUsuario == correo && u.ContrasenaUsuario == contrasena).FirstOrDefaultAsync();
            return loginU;
        }
    }
}