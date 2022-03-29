using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;
using LarussoWorksApi.Domain.Interfaces;

namespace LarussoWorksApi.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        public bool ValidarCreacion(Empresa nuevaEmpresa)
        {
            if(string.IsNullOrEmpty(nuevaEmpresa.NombreEmpresa))
                return false;

            if(string.IsNullOrEmpty(nuevaEmpresa.LogoEmpresa))
                return false;
            
            if(string.IsNullOrEmpty(nuevaEmpresa.DescripcionEmpresa))
                return false;

            if(string.IsNullOrEmpty(nuevaEmpresa.UbicacionEmpresa))
                return false;
                
            if(string.IsNullOrEmpty(nuevaEmpresa.TelefonoEmpresa))
                return false;

            if(string.IsNullOrEmpty(nuevaEmpresa.CorreoEmpresa))
                return false;

            if(string.IsNullOrEmpty(nuevaEmpresa.ContrasenaEmpresa))
                return false;

            return true;
        }

        public bool ValidarActualizacion(Empresa nuevaEmpresa)
        {
            if(nuevaEmpresa.IdEmpresa <= 0)
                return false;

            if(string.IsNullOrEmpty(nuevaEmpresa.NombreEmpresa))
                return false;

            if(string.IsNullOrEmpty(nuevaEmpresa.LogoEmpresa))
                return false;
            
            if(string.IsNullOrEmpty(nuevaEmpresa.DescripcionEmpresa))
                return false;

            if(string.IsNullOrEmpty(nuevaEmpresa.UbicacionEmpresa))
                return false;
                
            if(string.IsNullOrEmpty(nuevaEmpresa.TelefonoEmpresa))
                return false;

            if(string.IsNullOrEmpty(nuevaEmpresa.CorreoEmpresa))
                return false;

            if(string.IsNullOrEmpty(nuevaEmpresa.ContrasenaEmpresa))
                return false;

            return true;
        }
    }
}