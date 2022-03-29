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
    public class EmpresaSqlRepository : IEmpresaRepository
    {
        private readonly LarussoWorksFinallyContext _context;
        public EmpresaSqlRepository(LarussoWorksFinallyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> ObtenerEmpresa()
        {
            var empresa = _context.Empresas.Select(u => u);
            return await empresa.ToListAsync();
        }

        public async Task<Empresa> ObternerEmpresaID (int id)
        {
            var empresas = await _context.Empresas.FirstOrDefaultAsync(e => e.IdEmpresa == id);
            return empresas;
        }

        public async Task<int> CrearEmpresa (Empresa nuevaEmpresa)
        {
            var dato = nuevaEmpresa;
            await _context.Empresas.AddAsync(dato);
            var filas = await _context.SaveChangesAsync();
            if(filas <= 0)
            throw new Exception("Error al registrar la Empresa.");
            return dato.IdEmpresa;
        }

        public async Task<bool> ActualizarEmpresa (int id, Empresa actualizarEmpresa)
        {
            if (id < 0 || actualizarEmpresa == null)
            {
                throw new ArgumentException("Información incompleta, te faltan datos.");
            }
            var dato = await ObternerEmpresaID(id);
            dato.NombreEmpresa = actualizarEmpresa.NombreEmpresa;
            dato.LogoEmpresa = actualizarEmpresa.LogoEmpresa;
            dato.DescripcionEmpresa = actualizarEmpresa.DescripcionEmpresa;
            dato.UbicacionEmpresa = actualizarEmpresa.UbicacionEmpresa;
            dato.TelefonoEmpresa = actualizarEmpresa.TelefonoEmpresa;
            _context.Update(dato);
            var row = await _context.SaveChangesAsync();
            return row > 0;
        }

        public async Task<bool> EliminarEmpresa(int id)
        {
            if(id <= 0)
            {
                throw  new ArgumentException("La Empresa no existe en la base, intente con otro que si exista.");
            }
            var dato = await ObternerEmpresaID(id);
            _context.Remove(dato);
            var row = await _context.SaveChangesAsync();
            return row > 0; 
        }

        public async Task<Empresa> Login (string correo, string contrasena)
        {
            var loginE = await _context.Empresas.Where(u => u.CorreoEmpresa == correo && u.ContrasenaEmpresa == contrasena).FirstOrDefaultAsync();
            return loginE;
        }
    }
}