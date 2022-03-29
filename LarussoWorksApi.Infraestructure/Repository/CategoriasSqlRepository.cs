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
    public class CategoriasSqlRepository : ICategoriaRepository
    {
        private readonly LarussoWorksFinallyContext _context;
        public CategoriasSqlRepository(LarussoWorksFinallyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> ObtenerCategoria()
        {
            var categoria = _context.Categorias.Select(u => u);
            return await categoria.ToArrayAsync();
        }

        public async Task<Categoria> ObternerCategoriaID (int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);
            return categoria;
        }

        public async Task<int> CrearCategoria (Categoria nuevaCategoria)
        {
            var dato = nuevaCategoria;
            await _context.Categorias.AddAsync(dato);
            var filas = await _context.SaveChangesAsync();
            if(filas <= 0)
            throw new Exception("Error al registrar la Categoria.");
            return dato.IdCategoria;
        }

        public async Task<bool> ActualizarCategoria (int id, Categoria actualizarCategoria)
        {
            if (id <= 0 || actualizarCategoria == null)
            {
               throw new ArgumentException("Información incompleta, te faltan datos.");
            }
           var dato = await ObternerCategoriaID(id);
           dato.NombreCategoria = actualizarCategoria.NombreCategoria;
           _context.Update(dato);
           var row = await _context.SaveChangesAsync();
           return row > 0;
        }

          public async Task<bool> EliminarCategoria(int id)
        {
          if(id <= 0)
          {
              throw  new ArgumentException("La Categoria no existe en la base, intente con otro que si exista.");
          }
          var dato = await ObternerCategoriaID(id);
          _context.Remove(dato);
          var row = await _context.SaveChangesAsync();
          return row > 0; 
        }
    }
}