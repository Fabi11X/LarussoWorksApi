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
    public class MensajeController : ControllerBase
    {
        [HttpGet]
        [Route("ObtenerMensajes")]
        public IActionResult ObtenerMensajes()
        {
            MensajesSqlRepository repository = new MensajesSqlRepository();
            var mensaje = repository.ObtenerMensajes();
            return Ok(mensaje);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ObternerMensajesID(int id)
        {
            MensajesSqlRepository mensaje = new MensajesSqlRepository();
            var message = mensaje.ObternerMensajesID(id);
            if(message == null)
            {
                return NotFound("El Id no existe");
            }
            return Ok(message);
        }

        [HttpPost]
        [Route("CrearMensajes")]
        public IActionResult CrearMensajes (Mensaje nuevoMensajes)
        {
            MensajesSqlRepository mensaje = new MensajesSqlRepository();

            try
            {
                mensaje.CrearMensajes(nuevoMensajes);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "¡ERROR!, no se pudo realizar el registro");
            }
            return Ok(nuevoMensajes);
        }

        [HttpPut]
        [Route("ActualizarMensajes/{id:int}")]
        public IActionResult ActualizarMensajes (int id, Mensaje actualizarMensaje)
        {
            MensajesSqlRepository mensaje = new MensajesSqlRepository();
            var actualizar = mensaje.ObternerMensajesID(id);
            if(actualizar == null)
            {
                return NotFound($"Id Incorrecto");
            }

            mensaje.ActualizarMensajes(id, actualizarMensaje);
            return Ok(actualizarMensaje);
        }

        [HttpDelete]
        [Route("EliminarMensajes/{id:int}")]
        public IActionResult EliminarMensajes(int id)
        {
            MensajesSqlRepository mensaje = new MensajesSqlRepository();
            var actulizar = mensaje.ObternerMensajesID(id);
            if(actulizar == null)
            {
                return NotFound("El id no existe");
            }

            mensaje.EliminarMensajes(id);
            return Ok("El Mensaje se elimino correctamente");
        }
    }
}