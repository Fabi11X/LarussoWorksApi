using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;
using LarussoWorksApi.Domain.Interfaces;

namespace LarussoWorksApi.Application.Services
{
    public class EmpleoService : IEmpleoService
    {
        public bool ValidarCreacion(Empleo nuevoEmpleo)
        {
            if(string.IsNullOrEmpty(nuevoEmpleo.NombreEmpleo))
                return false;

            if(string.IsNullOrEmpty(nuevoEmpleo.DescripcionEmpleo))
                return false;
            
            if(string.IsNullOrEmpty(nuevoEmpleo.RequisitosEmpleo))
                return false;

            if(string.IsNullOrEmpty(nuevoEmpleo.PrestacionesEmpleo))
                return false;
                
            if(string.IsNullOrEmpty(nuevoEmpleo.UbicacionEmpleo))
                return false;

            if(string.IsNullOrEmpty(nuevoEmpleo.ImagenEmpleo))
                return false;

            return true;
        }

        public bool ValidarActualizacion(Empleo nuevoEmpleo)
        {
            if(nuevoEmpleo.IdEmpleo <= 0)
                return false;

            if(string.IsNullOrEmpty(nuevoEmpleo.NombreEmpleo))
                return false;

            if(string.IsNullOrEmpty(nuevoEmpleo.DescripcionEmpleo))
                return false;
            
            if(string.IsNullOrEmpty(nuevoEmpleo.RequisitosEmpleo))
                return false;

            if(string.IsNullOrEmpty(nuevoEmpleo.PrestacionesEmpleo))
                return false;
                
            if(string.IsNullOrEmpty(nuevoEmpleo.UbicacionEmpleo))
                return false;

            if(string.IsNullOrEmpty(nuevoEmpleo.ImagenEmpleo))
                return false;

            return true;
        }
    }
}