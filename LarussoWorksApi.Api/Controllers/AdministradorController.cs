using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using LarussoWorksApi.Infraestructure.Repository;
using Microsoft.AspNetCore.Http;
using LarussoWorksApi.Domain.entities;
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
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorService _service;
        private readonly IAdministradorRepository _repository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IValidator<AdministradorCreateRequest> _createValidator;
        private readonly IValidator<AdministradorUpdateRequest> _updateValidator;

        private readonly LarussoWorksFinallyContext _context;

        public AdministradorController(IAdministradorService service, 
            IAdministradorRepository repository, 
            IHttpContextAccessor httpContext, 
            IMapper mapper,
            IValidator<AdministradorCreateRequest> createValidator,
            IValidator<AdministradorUpdateRequest> updateValidator,
            LarussoWorksFinallyContext context)
        {
            this._repository = repository;
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
        public async Task<IActionResult> ObtenerAdmin([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var admins = await _repository.ObtenerAdmin();
            //var respadmin = _mapper.Map<IEnumerable<Administrador>, IEnumerable<AdministradorResponse>>(admins);
            return Ok (admins);
        } 

         [HttpGet]
        [Route("ObtenerAdminII")]

         public async Task<IActionResult> ObtenerAdminII([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var queryable = _context.Administradors.AsQueryable();
            if(! string.IsNullOrEmpty(nombre))
            {
                queryable = queryable.Where(x => x.NombreAdmin.Contains(nombre));
            }
            await HttpContext.InsertarParametrosEnRespuesta(queryable, paginacion.CantidadAMostrar);
            var usuarios = await _repository.ObtenerAdmin();
            return Ok (queryable.Paginar(paginacion));

        
            //var respusuario = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioResponse>>(usuarios);
        
            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObternerAdminID(int id)
        {
            var admin = await _repository.ObternerAdminID(id);
            if(admin == null)
            {
                return NotFound("La Administrador con ese ID no existe");
            }
            //var respuesta = _mapper.Map<Administrador, AdministradorResponse>(admin);
            return Ok(admin);
        }

        [HttpPost]
        [Route("CrearAdmin")]
        public async Task<IActionResult> CrearAdmin (AdministradorCreateRequest nuevoAdmin)
        {
            var validacion = await _createValidator.ValidateAsync(nuevoAdmin);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(x => $"{x.PropertyName} => Error: {x.ErrorMessage}"));
            var dato = _mapper.Map<AdministradorCreateRequest, Administrador>(nuevoAdmin);
            int id = await _repository.CrearAdmin(dato);
            if(id <= 0)
                return Conflict("Error al registrar al nuevo Administrador, revisa los datos.");
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/administrador/{id}";
            //return Created(urlResult, id);
            return Ok(nuevoAdmin);
        }  

        [HttpPut]
        [Route("ActualizarAdministrador/{id:int}")]
        public async Task<IActionResult> ActualizarAdmin ( int id, [FromBody] AdministradorUpdateRequest actualizarAdmin )
        {
            var validacion = await _updateValidator.ValidateAsync(actualizarAdmin);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(dest => $"{dest.PropertyName} => Error: {dest.ErrorMessage}"));
            var dato = _mapper.Map<AdministradorUpdateRequest, Administrador>(actualizarAdmin);
            if(id <= 0)
                return NotFound("No se encontró el Administrador con la ID introducido.");
            var dato2 = await _repository.ObternerAdminID(id);
            if(dato2 == null)
                return NotFound("No se encontró el Administrador con la ID introducido.");
            var Id = await _repository.ActualizarAdmin(id, dato);
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/administrador/{id}";
            //return Ok("El Administrador se ha actualizado correctamente");
            return Ok(actualizarAdmin);
        }  

        [HttpDelete]
        [Route("BorrarAdministrador/{id:int}")]
        public async Task<IActionResult> EliminarAdministrador (int id)
        {
            if (id <= 0)
                return NotFound("No se encontró el Administrador con el ID introducido.");
            var entity = await _repository.ObternerAdminID(id);
            if(entity == null)
                return NotFound("No se encontró el Administrador con el ID introducido.");
            var deleted = await _repository.EliminarAdmin(id);
            if(!deleted)
                Conflict("Error al intentar eliminar al Administrador.");
            return Ok("Administrador eliminado exitosamente.");
        }

        [HttpGet]
        [Route("Login/{correo}/{contrasena}")]
        public async Task<IActionResult> Login (string correo, string contrasena)
        {
            var admin = await _repository.Login(correo, contrasena);
            return Ok(admin);
        }
    }
}