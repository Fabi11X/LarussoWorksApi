using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LarussoWorksApi.Domain.entities;
using LarussoWorksApi.Infraestructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LarussoWorksApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoController : Controller
    {
        [HttpGet]
        [Route("ObtenerContactanos")]
        public IActionResult ObtenerContactanos()
        {
            ContactanosSqlRepository repository = new ContactanosSqlRepository();
            var contacto = repository.ObtenerContactanos();
            return Ok(contacto);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ObternerContactanosID(int id)
        {
            ContactanosSqlRepository contacto = new ContactanosSqlRepository();
            var contact = contacto.ObternerContactanosID(id);
            if(contact == null)
            {
                return NotFound("El Id no existe");
            }
            return Ok(contact);
        }

        [HttpPost]
        [Route("CrearContactanos")]
        public IActionResult CrearContactanos (Contactano nuevoContactanos)
        {
            ContactanosSqlRepository contacto = new ContactanosSqlRepository();

            try
            {
                contacto.CrearContactanos(nuevoContactanos);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "¡ERROR!, no se pudo realizar el registro");
            }
            return Ok(nuevoContactanos);
        }

        [HttpPut]
        [Route("ActualizarContactanos/{id:int}")]
        public IActionResult ActualizarContactanos (int id, Contactano actualizarContactanos)
        {
            ContactanosSqlRepository contacto = new ContactanosSqlRepository();
            var actualizar = contacto.ObternerContactanosID(id);
            if(actualizar == null)
            {
                return NotFound($"Id Incorrecto");
            }

            contacto.ActualizarContactanos(id, actualizarContactanos);
            return Ok(actualizarContactanos);
        }

        [HttpDelete]
        [Route("EliminarContactanos/{id:int}")]
        public IActionResult EliminarContactanos(int id)
        {
            ContactanosSqlRepository contacto = new ContactanosSqlRepository();
            var actulizar = contacto.ObternerContactanosID(id);
            if(actulizar == null)
            {
                return NotFound("El id no existe");
            }

            contacto.EliminarContactanos(id);
            return Ok("El Contactanos se elimino correctamente");
        }

    }
}