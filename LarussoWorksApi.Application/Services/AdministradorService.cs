using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;
using LarussoWorksApi.Domain.Interfaces;

namespace LarussoWorksApi.Application.Services
{
    public class AdministradorService : IAdministradorService
    {
        public bool ValidarCreacion(Administrador nuevoAdmin)
        {
            if(string.IsNullOrEmpty(nuevoAdmin.NombreAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.ApellidoAdmin))
                return false;
            
            if(string.IsNullOrEmpty(nuevoAdmin.SexoAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.FechanacAdmin))
                return false;
                
            if(string.IsNullOrEmpty(nuevoAdmin.TelefonoAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.CorreoAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.ContrasenaAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.DireccionAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.FotografiaAdmin))
                return false;

            return true;
        }

        public bool ValidarActualizacion(Administrador nuevoAdmin)
        {
            if(nuevoAdmin.IdAdmin <= 0)
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.NombreAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.ApellidoAdmin))
                return false;
            
            if(string.IsNullOrEmpty(nuevoAdmin.SexoAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.FechanacAdmin))
                return false;
                
            if(string.IsNullOrEmpty(nuevoAdmin.TelefonoAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.CorreoAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.ContrasenaAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.DireccionAdmin))
                return false;

            if(string.IsNullOrEmpty(nuevoAdmin.FotografiaAdmin))
                return false;

            return true;
        }
    }
}