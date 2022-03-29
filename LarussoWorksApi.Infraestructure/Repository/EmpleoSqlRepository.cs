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
    public class EmpleoSqlRepository : IEmpleoRepository
    {
        private readonly LarussoWorksFinallyContext _context;
        public EmpleoSqlRepository(LarussoWorksFinallyContext context)
        {
          _context = context;
        }

        public async Task<IEnumerable<Empleo>> ObtenerEmpleo()
        {
          var empleo = _context.Empleos.Select(e => e);
          return await empleo.ToListAsync();
        }

        public async Task<Empleo> ObternerEmpleoID (int id)
        {
            var empleo = await _context.Empleos.FirstOrDefaultAsync(e => e.IdEmpleo == id);
            return empleo;
        }

        public async Task<int> CrearEmpleo (Empleo nuevoEmpleo)
        {
            var dato = nuevoEmpleo;
            await _context.Empleos.AddAsync(dato);
            var filas = await _context.SaveChangesAsync();
            if(filas <= 0)
            throw new Exception("Error al registrar el Empleo");
            return dato.IdEmpleo;
        }

        public async Task<bool> ActualizarEmpleo (int id, Empleo actualizarEmpleo)
        {
            if (id < 0 || actualizarEmpleo == null)
            {
               throw new ArgumentException("Información incompleta, te faltan datos.");
            }
           var dato = await ObternerEmpleoID(id);
           dato.NombreEmpleo = actualizarEmpleo.NombreEmpleo;
           dato.DescripcionEmpleo = actualizarEmpleo.DescripcionEmpleo;
           dato.RequisitosEmpleo = actualizarEmpleo.RequisitosEmpleo;
           dato.PrestacionesEmpleo = actualizarEmpleo.PrestacionesEmpleo;
           dato.UbicacionEmpleo = actualizarEmpleo.UbicacionEmpleo;
           dato.ImagenEmpleo = actualizarEmpleo.ImagenEmpleo;
           dato.IdCategoria = actualizarEmpleo.IdCategoria;
           dato.IdEmpresa = actualizarEmpleo.IdEmpresa;
           _context.Update(dato);
           var row = await _context.SaveChangesAsync();
           return row > 0;
        }

        public async Task<bool> EliminarEmpleo(int id)
        {
          if(id <= 0)
          {
              throw  new ArgumentException("La Empresa no existe en la base, intente con otro que si exista.");
          }
          var dato = await ObternerEmpleoID(id);
          _context.Remove(dato);
          var row = await _context.SaveChangesAsync();
          return row > 0; 
          

        }
        
    }
}