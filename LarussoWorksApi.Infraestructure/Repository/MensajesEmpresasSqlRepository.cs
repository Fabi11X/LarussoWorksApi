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
    public class MensajesEmpresasSqlRepository
    {
         private readonly LarussoWorksFinallyContext _context;
        public MensajesEmpresasSqlRepository()
        {
            _context = new LarussoWorksFinallyContext(); 
        }

        public IEnumerable<MensajesEmpresa> ObtenerMensajesEmpresa()
        {
            return _context.MensajesEmpresas;
        }

        public MensajesEmpresa ObtenerMensajesEmpresaID (int id)
        {
            var mensajeempresa = _context.MensajesEmpresas.Where(c => c.IdMensajeEmpresa == id);
            return mensajeempresa.FirstOrDefault();
        }
        public void CrearMensajesEmpresa (MensajesEmpresa nuevoMensajesEmpresa)
        {
            var dato = nuevoMensajesEmpresa;
            _context.MensajesEmpresas.Add(dato);
            var filas = _context.SaveChanges();
            if(filas <= 0)
            throw new Exception("Error al registrar el Mensaje de la empresa nuevo");
            return;
        }

        public void ActualizarMensajesEmpresa (int id, MensajesEmpresa actualizarMensajeEmpresa)
        {
            if (id <= 0 || actualizarMensajeEmpresa == null)
            {
                throw new ArgumentException("Información incompleta, te faltan datos.");
            }
            var dato = ObtenerMensajesEmpresaID(id);
            dato.IdEmpresa = actualizarMensajeEmpresa.IdEmpresa;
            dato.IdUsuario = actualizarMensajeEmpresa.IdUsuario;
            dato.MensajeEmpresa = actualizarMensajeEmpresa.MensajeEmpresa;
            _context.Update(dato);

            _context.Update(dato);
            var row = _context.SaveChanges();
            return;
        }

        public void EliminarMensajesEmpresa(int id)
        {
            if(id <= 0)
            {
                throw  new ArgumentException("El Mensaje de la empresa no existe en la base, intente con otro que si exista.");
            }
            
            var dato = ObtenerMensajesEmpresaID(id);
            _context.Remove(dato);
            var row = _context.SaveChanges();
            return; 
        }
    }
}