using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;

namespace LarussoWorksApi.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObtenerCategoria();
        Task<Categoria> ObternerCategoriaID (int id);
        Task<int> CrearCategoria(Categoria nuevaCategoria);
        Task<bool> ActualizarCategoria(int id, Categoria actualizarCategoria);
        Task<bool> EliminarCategoria(int id);
    }
}