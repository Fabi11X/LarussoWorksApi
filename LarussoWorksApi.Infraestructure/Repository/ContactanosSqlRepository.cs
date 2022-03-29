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
    public class ContactanosSqlRepository
    {
        private readonly LarussoWorksFinallyContext _context;
        public ContactanosSqlRepository()
        {
            _context = new LarussoWorksFinallyContext(); 
        }

        public IEnumerable<Contactano> ObtenerContactanos()
        {
            return _context.Contactanos;
        }

        public Contactano ObternerContactanosID (int id)
        {
            var contacto = _context.Contactanos.Where(c => c.IdContacto == id);
            return contacto.FirstOrDefault();
        }
        public void CrearContactanos (Contactano nuevoContactanos)
        {
            var dato = nuevoContactanos;
            _context.Contactanos.Add(dato);
            var filas = _context.SaveChanges();
            if(filas <= 0)
            throw new Exception("Error al registrar el Contactanos nuevo");
            return;
        }

        public void ActualizarContactanos (int id, Contactano actualizarContactanos)
        {
            if (id <= 0 || actualizarContactanos == null)
            {
                throw new ArgumentException("Información incompleta, te faltan datos.");
            }
            var dato = ObternerContactanosID(id);
            dato.CorreoContacto = actualizarContactanos.CorreoContacto;
            dato.AsuntoContacto = actualizarContactanos.AsuntoContacto;
            dato.MensajeContacto = actualizarContactanos.MensajeContacto;
            _context.Update(dato);

            _context.Update(dato);
            var row = _context.SaveChanges();
            return;
        }

        public void EliminarContactanos(int id)
        {
            if(id <= 0)
            {
                throw  new ArgumentException("El Contactanos no existe en la base, intente con otro que si exista.");
            }
            
            var dato = ObternerContactanosID(id);
            _context.Remove(dato);
            var row = _context.SaveChanges();
            return; 
        }
    }
}