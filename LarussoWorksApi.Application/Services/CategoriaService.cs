using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;
using LarussoWorksApi.Domain.Interfaces;

namespace LarussoWorksApi.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        public bool ValidarCreacion(Categoria nuevaCategoria)
        {
            if(string.IsNullOrEmpty(nuevaCategoria.NombreCategoria))
                return false;

            return true;
        }

        public bool ValidarActualizacion(Categoria nuevaCategoria)
        {
            if(nuevaCategoria.IdCategoria <= 0)
                return false;

            if(string.IsNullOrEmpty(nuevaCategoria.NombreCategoria))
                return false;

            return true;
        }
    }
}