using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.entities;
using LarussoWorksApi.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using LarussoWorksApi.Domain.Interfaces;

namespace LarussoWorksApi.Infraestructure.Repository
{
    public class MensajesSqlRepository
    {
        private readonly LarussoWorksFinallyContext _context;
        public MensajesSqlRepository()
        {
            _context = new LarussoWorksFinallyContext(); 
        }

        public IEnumerable<Mensaje> ObtenerMensajes()
        {
            return _context.Mensajes;
        }

        public Mensaje ObternerMensajesID (int id)
        {
            var mensaje = _context.Mensajes.Where(c => c.IdMensaje == id);
            return mensaje.FirstOrDefault();
        }
        public void CrearMensajes (Mensaje nuevoMensajes)
        {
            var dato = nuevoMensajes;
            _context.Mensajes.Add(dato);
            var filas = _context.SaveChanges();
            if(filas <= 0)
            throw new Exception("Error al registrar el Mensaje nuevo");
            return;
        }

        public void ActualizarMensajes (int id, Mensaje actualizarMensaje)
        {
            if (id <= 0 || actualizarMensaje == null)
            {
                throw new ArgumentException("Información incompleta, te faltan datos.");
            }
            var dato = ObternerMensajesID(id);
            dato.IdUsuario = actualizarMensaje.IdUsuario;
            dato.IdEmpleo = actualizarMensaje.IdEmpleo;
            dato.BuzonMensaje = actualizarMensaje.BuzonMensaje;
            _context.Update(dato);

            _context.Update(dato);
            var row = _context.SaveChanges();
            return;
        }

        public void EliminarMensajes(int id)
        {
            if(id <= 0)
            {
                throw  new ArgumentException("El Mensaje no existe en la base, intente con otro que si exista.");
            }
            
            var dato = ObternerMensajesID(id);
            _context.Remove(dato);
            var row = _context.SaveChanges();
            return; 
        }
    }
}