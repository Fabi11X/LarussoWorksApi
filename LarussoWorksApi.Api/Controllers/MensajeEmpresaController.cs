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
    public class MensajeEmpresaController : ControllerBase
    {
         [HttpGet]
        [Route("ObtenerMensajesEmpresa")]
        public IActionResult ObtenerMensajesEmpresa()
        {
            MensajesEmpresasSqlRepository repository = new MensajesEmpresasSqlRepository();
            var mensajeempresa = repository.ObtenerMensajesEmpresa();
            return Ok(mensajeempresa);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ObtenerMensajesEmpresaID(int id)
        {
            MensajesEmpresasSqlRepository mensajeempresa = new MensajesEmpresasSqlRepository();
            var messageempresa = mensajeempresa.ObtenerMensajesEmpresaID(id);
            if(mensajeempresa == null)
            {
                return NotFound("El Id no existe");
            }
            return Ok(mensajeempresa);
        }

        [HttpPost]
        [Route("CrearMensajesEmpresa")]
        public IActionResult CrearMensajesEmpresa (MensajesEmpresa nuevoMensajesEmpresa)
        {
            MensajesEmpresasSqlRepository mensajeempresa = new MensajesEmpresasSqlRepository();

            try
            {
                mensajeempresa.CrearMensajesEmpresa(nuevoMensajesEmpresa);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "¡ERROR!, no se pudo realizar el registro");
            }
            return Ok(nuevoMensajesEmpresa);
        }

        [HttpPut]
        [Route("ActualizarMensajesEmpresa/{id:int}")]
        public IActionResult ActualizarMensajesEmpresa (int id, MensajesEmpresa actualizarMensajeEmpresa)
        {
            MensajesEmpresasSqlRepository mensajeempresa = new MensajesEmpresasSqlRepository();
            var actualizar = mensajeempresa.ObtenerMensajesEmpresaID(id);
            if(actualizar == null)
            {
                return NotFound($"Id Incorrecto");
            }

            mensajeempresa.ActualizarMensajesEmpresa(id, actualizarMensajeEmpresa);
            return Ok(actualizarMensajeEmpresa);
        }

        [HttpDelete]
        [Route("EliminarMensajesEmpresa/{id:int}")]
        public IActionResult EliminarMensajesEmpresa(int id)
        {
            MensajesEmpresasSqlRepository mensajeempresa = new MensajesEmpresasSqlRepository();
            var actulizar = mensajeempresa.ObtenerMensajesEmpresaID(id);
            if(actulizar == null)
            {
                return NotFound("El id no existe");
            }

            mensajeempresa.EliminarMensajesEmpresa(id);
            return Ok("El Mensaje se elimino correctamente");
        }
        
    }
}