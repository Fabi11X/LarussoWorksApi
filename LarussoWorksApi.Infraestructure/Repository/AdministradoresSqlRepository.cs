using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;
using LarussoWorksApi.Infraestructure.Data;
using LarussoWorksApi.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using LarussoWorksApi.Domain.Interfaces;


namespace LarussoWorksApi.Infraestructure.Repository
{
    public class AdministradoresSqlRepository : IAdministradorRepository
    {
        private readonly LarussoWorksFinallyContext _context;
        public AdministradoresSqlRepository(LarussoWorksFinallyContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<Administrador>> ObtenerAdmin()
        {
            var admin = _context.Administradors.Select(a => a);
            return await admin.ToListAsync();
        }

        public async Task<Administrador> ObternerAdminID (int id)
        {
            var admin = await _context.Administradors.FirstOrDefaultAsync(a => a.IdAdmin == id);
            return admin;
        }
        public async Task<int> CrearAdmin (Administrador nuevoAdmin)
        {
            var dato = nuevoAdmin;
            await _context.Administradors.AddAsync(dato);
            var filas = await _context.SaveChangesAsync();
            if(filas <= 0)
            throw new Exception("Error al registrar administrador");
            return dato.IdAdmin;
        }

        public async Task<bool> ActualizarAdmin (int id, Administrador actualizarAdmin)
        {
            if (id <= 0 || actualizarAdmin == null)
            {
                throw new ArgumentException("Información incompleta, te faltan datos.");
            }
            var dato = await ObternerAdminID(id);
            dato.NombreAdmin = actualizarAdmin.NombreAdmin;
            dato.ApellidoAdmin = actualizarAdmin.ApellidoAdmin;
            dato.SexoAdmin = actualizarAdmin.SexoAdmin;
            dato.FechanacAdmin = actualizarAdmin.FechanacAdmin;
            dato.TelefonoAdmin = actualizarAdmin.TelefonoAdmin;
            dato.DireccionAdmin = actualizarAdmin.DireccionAdmin;
            dato.FotografiaAdmin = actualizarAdmin.FotografiaAdmin;
            _context.Update(dato);
            var row = await _context.SaveChangesAsync();
            return row > 0;
        }

        public async Task<bool> EliminarAdmin(int id)
        {
            if(id <= 0)
            {
                throw  new ArgumentException("El Administrador no existe en la base, intente con otro que si exista.");
            }
            var dato = await ObternerAdminID(id);
            _context.Remove(dato);
            var row = await _context.SaveChangesAsync();
            return row > 0; 
        }

        public async Task<Administrador> Login (string correo, string contrasena)
        {
            var loginA = await _context.Administradors.Where(u => u.CorreoAdmin == correo && u.ContrasenaAdmin == contrasena).FirstOrDefaultAsync();
            return loginA;
        }
    }
}