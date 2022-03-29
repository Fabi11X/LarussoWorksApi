using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;
using LarussoWorksApi.Domain.Interfaces;

namespace LarussoWorksApi.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        public bool ValidarCreacion(Usuario nuevoUsuario)
        {
            if(string.IsNullOrEmpty(nuevoUsuario.NombreUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.ApellidoUsuario))
                return false;
            
            if(string.IsNullOrEmpty(nuevoUsuario.SexoUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.FechanacUsuario))
                return false;
                
            if(string.IsNullOrEmpty(nuevoUsuario.CorreoUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.ContrasenaUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.TelefonoUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.FotografiaUsuario))
                return false;

                



            return true;
        }

        public bool ValidarActualizacion(Usuario nuevoUsuario)
        {
            if(nuevoUsuario.IdUsuario <= 0)
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.NombreUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.ApellidoUsuario))
                return false;
            
            if(string.IsNullOrEmpty(nuevoUsuario.SexoUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.FechanacUsuario))
                return false;
                
            if(string.IsNullOrEmpty(nuevoUsuario.CorreoUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.ContrasenaUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.TelefonoUsuario))
                return false;

            if(string.IsNullOrEmpty(nuevoUsuario.FotografiaUsuario))
                return false;

            return true;
        }
    }
}