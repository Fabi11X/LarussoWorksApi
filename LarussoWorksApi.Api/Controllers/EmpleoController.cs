using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using LarussoWorksApi.Infraestructure.Repository;
using LarussoWorksApi.Domain.entities;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using LarussoWorksApi.Domain.Interfaces;
using LarussoWorksApi.Domain.Dtos.Responses;
using LarussoWorksApi.Application.Services;
using LarussoWorksApi.Domain.Dtos.Requests;
using FluentValidation;
using LarussoWorksApi.Api.Helpers;
using LarussoWorksApi.Infraestructure.Data;

namespace LarussoWorksApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleoController : ControllerBase
    {
        private readonly IEmpleoService _service;
        private readonly IEmpleoRepository _repository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IValidator<EmpleoCreateRequest> _createValidator;
        private readonly IValidator<EmpleoUpdateRequest> _updateValidator;

        private readonly LarussoWorksFinallyContext _context;

        public EmpleoController(IEmpleoService service, 
            IEmpleoRepository repository, 
            IHttpContextAccessor httpContext, 
            IMapper mapper,
            IValidator<EmpleoCreateRequest> createValidator,
            IValidator<EmpleoUpdateRequest> updateValidator,
            LarussoWorksFinallyContext context)
        {
            this._service = service;
            this._repository = repository;
            this._httpContext = httpContext;
            this._mapper = mapper;
            this._createValidator = createValidator;
            this._updateValidator = updateValidator;
            this._context = context;
        }

        [HttpGet]
        [Route("")]

        public async Task<IActionResult> ObtenerEmpleo([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var empleos = await _repository.ObtenerEmpleo();
            //var respempleo = _mapper.Map<IEnumerable<Empleo>, IEnumerable<EmpleoResponse>>(empleos);
            return Ok (empleos);
        } 

        [HttpGet]
        [Route("ObtenerEmpleoII")]

        public async Task<IActionResult> ObtenerEmpleoII([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var queryable = _context.Empleos.AsQueryable();
            if(! string.IsNullOrEmpty(nombre))
            {
                queryable = queryable.Where(x => x.NombreEmpleo.Contains(nombre));
            }
            await HttpContext.InsertarParametrosEnRespuesta(queryable, paginacion.CantidadAMostrar);
            var empleos = await _repository.ObtenerEmpleo();
            return Ok (queryable.Paginar(paginacion));

            //var respusuario = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioResponse>>(usuarios);
        }
        
        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> ObternerEmpleoID(int id)
        {
            var empleo = await _repository.ObternerEmpleoID(id);
            if(empleo == null)
            {
                return NotFound("El Empleo con ese ID no existe");
            }
            //var respuesta = _mapper.Map<Empleo, EmpleoResponse>(empleo);
            return Ok(empleo);
        }

        

        [HttpPost]
        [Route("CrearEmpleo")]
        public async Task<IActionResult> CrearEmpleo (EmpleoCreateRequest nuevoEmpleo)
        {
            var validacion = await _createValidator.ValidateAsync(nuevoEmpleo);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(x => $"{x.PropertyName} => Error: {x.ErrorMessage}"));
            var dato = _mapper.Map<EmpleoCreateRequest, Empleo>(nuevoEmpleo);
            int id = await _repository.CrearEmpleo(dato);
            if(id <= 0)
                return Conflict("Error al registrar el nuevo Empleo, revisa los datos.");
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/empleo/{id}";
            //return Created(urlResult, id);
            return Ok(nuevoEmpleo);
        }  

        [HttpPut]
        [Route("ActualizarEmpleo/{id:int}")]
        public async Task<IActionResult> ActualizarEmpleo(int id, [FromBody] EmpleoUpdateRequest actualizarEmpleo)
        {
            var validacion = await _updateValidator.ValidateAsync(actualizarEmpleo);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(dest => $"{dest.PropertyName} => Error: {dest.ErrorMessage}"));
            var dato = _mapper.Map<EmpleoUpdateRequest, Empleo>(actualizarEmpleo);
            if(id <= 0)
                return NotFound("No se encontró el Empleo con el ID introducido.");
            var dato2 = await _repository.ObternerEmpleoID(id);
            if(dato2 == null)
                return NotFound("No se encontró el Empleo con el ID introducido.");
            var Id = await _repository.ActualizarEmpleo(id, dato);
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/empleo/{id}";
            //return Ok("El Empleo se ha actualizado correctamente.");
            return Ok(actualizarEmpleo);
        }  

        
        [HttpDelete]
        [Route("BorrarEmpleo/{id:int}")]
        public async Task<IActionResult> EliminarEmpleo ( int id )
        {
           if (id <= 0)
                return NotFound("No se encontró el Empleo con el ID introducido.");
            var entity = await _repository.ObternerEmpleoID(id);
            if(entity == null)
                return NotFound("No se encontró el Empleo con el ID introducido.");
            var deleted = await _repository.EliminarEmpleo(id);
            if(!deleted)
                Conflict("Error al intentar eliminar el Empleo.");
            return Ok("Empleo eliminado exitosamente.");
        } 
    }
}