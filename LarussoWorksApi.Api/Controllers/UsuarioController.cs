using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using LarussoWorksApi.Infraestructure.Repository;
using LarussoWorksApi.Domain.entities;
using Microsoft.AspNetCore.Http;
using LarussoWorksApi.Domain.Interfaces;
using AutoMapper;
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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly IUsuarioRepository _repository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IValidator<UsuarioCreateRequest> _createValidator;
        private readonly IValidator<UsuarioUpdateRequest> _updateValidator;
        private readonly LarussoWorksFinallyContext _context;


        public UsuarioController(

            IUsuarioService service, 
            IUsuarioRepository repository, 
            IHttpContextAccessor httpContext, 
            IMapper mapper, 
            IValidator<UsuarioCreateRequest> createValidator,
            IValidator<UsuarioUpdateRequest> updateValidator,
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

        public async Task<IActionResult> ObtenerUsuario([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var usuarios = await _repository.ObtenerUsuario();
            return Ok(usuarios);

        }
    
        [HttpGet]
        [Route("ObtenerUsuarioII")]
        public async Task<IActionResult> ObtenerUsuarioII([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var queryable = _context.Usuarios.AsQueryable();
            if(! string.IsNullOrEmpty(nombre))
            {
                queryable = queryable.Where(x => x.NombreUsuario.Contains(nombre));
            }
            await HttpContext.InsertarParametrosEnRespuesta(queryable, paginacion.CantidadAMostrar);
            var usuarios = await _repository.ObtenerUsuario();
            return Ok (queryable.Paginar(paginacion));
            //var respusuario = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioResponse>>(usuarios);
        }
        



        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObternerUsuarioID(int id)
        {
            var usuario = await _repository.ObternerUsuarioID(id);
            if(usuario == null)
            {
                return NotFound("El Usuario con ese ID no existe");
            }
            //var respuesta = _mapper.Map<Usuario, UsuarioResponse>(usuario);
            return Ok(usuario);
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario (UsuarioCreateRequest nuevoUsuario)
        {
            var validacion = await _createValidator.ValidateAsync(nuevoUsuario);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(x => $"{x.PropertyName} => Error: {x.ErrorMessage}"));
            var dato = _mapper.Map<UsuarioCreateRequest, Usuario>(nuevoUsuario);
            int id = await _repository.CrearUsuario(dato);
            if(id <= 0)
                return Conflict("Error al registrar al nuevo Usuario, revisa los datos.");
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/usuario/{id}";
            //return Created(urlResult, id);
            return Ok(nuevoUsuario);
        }  

        [HttpPut]
        [Route("ActualizarUsuario/{id:int}")]
        public async Task<IActionResult> ActualizarUsuario ( int id,[FromBody]UsuarioUpdateRequest actualizarUsuario)
        {
            var validacion = await _updateValidator.ValidateAsync(actualizarUsuario);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(dest => $"{dest.PropertyName} => Error: {dest.ErrorMessage}"));
            var dato = _mapper.Map<UsuarioUpdateRequest, Usuario>(actualizarUsuario);
            if(id <= 0)
                return NotFound("No se encontró el Usuario con el ID introducido.");
            var dato2 = await _repository.ObternerUsuarioID(id);
            if(dato2 == null)
                return NotFound("No se encontró el Usuario con la ID introducido.");
            var Id = await _repository.ActualizarUsuario(id, dato);
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/usuario/{id}";
            //return Ok("El Usuario se ha actualizado correctamente.");
            return Ok(actualizarUsuario);
        }  

        
        [HttpDelete]
        [Route("BorrarUsuario/{id:int}")]
        public async Task<IActionResult> EliminarUsuario (int id)
        {
            if (id <= 0)
                return NotFound("No se encontró el Usuario con el ID introducido.");
            var entity = await _repository.ObternerUsuarioID(id);
            if(entity == null)
                return NotFound("No se encontró el Usuario con el ID introducido.");
            var deleted = await _repository.EliminarUsuario(id);
            if(!deleted)
                Conflict("Error al intentar eliminar al Usuario.");
            return Ok("Usuario eliminado exitosamente.");
        }

        [HttpGet]
        [Route("Login/{correo}/{contrasena}")]
        public async Task<IActionResult> Login (string correo, string contrasena)
        {
            var usuario = await _repository.Login(correo, contrasena);
            return Ok(usuario);
        }
    }
}
