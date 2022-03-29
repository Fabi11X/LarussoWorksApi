using System.Globalization;
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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;
        private readonly IEmpresaRepository _repository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IValidator<EmpresaCreateRequest> _createValidator;
        private readonly IValidator<EmpresaUpdateRequest> _updateValidator;

        private readonly LarussoWorksFinallyContext _context;

        public EmpresaController(IEmpresaService service, 
            IEmpresaRepository repository, 
            IHttpContextAccessor httpContext, 
            IMapper mapper,
            IValidator<EmpresaCreateRequest> createValidator,
            IValidator<EmpresaUpdateRequest> updateValidator,
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
        [Route("ObtenerEmpresa")]

        public async Task<IActionResult>ObtenerEmpresa([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var empresas = await _repository.ObtenerEmpresa();
            //var respempresa = _mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaResponse>>(empresas);
            return Ok (empresas);
        } 

       

         [HttpGet]
        [Route("ObtenerEmpresaII")]
        public async Task<IActionResult> ObtenerEmpresaII([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var queryable = _context.Empresas.AsQueryable();
            if(! string.IsNullOrEmpty(nombre))
            {
                queryable = queryable.Where(x => x.NombreEmpresa.Contains(nombre));
            }
            await HttpContext.InsertarParametrosEnRespuesta(queryable, paginacion.CantidadAMostrar);
            var empresas = await _repository.ObtenerEmpresa();
            return Ok (queryable.Paginar(paginacion));
            //var respusuario = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioResponse>>(usuarios);
         
        }

        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObternerEmpresaID(int id)
        {
            var empresa = await _repository.ObternerEmpresaID(id);
            if(empresa == null)
            {
                return NotFound("La Empresa con ese ID no existe");
            }
            //var respuesta = _mapper.Map<Empresa, EmpresaResponse>(empresa);
            return Ok(empresa);
        }  

        [HttpPost]
        [Route("CrearEmpresa")]
        public async Task<IActionResult> CrearEmpresa (EmpresaCreateRequest nuevaEmpresa)
        {
            var validacion = await _createValidator.ValidateAsync(nuevaEmpresa);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(x => $"{x.PropertyName} => Error: {x.ErrorMessage}"));
            var dato = _mapper.Map<EmpresaCreateRequest, Empresa>(nuevaEmpresa);
            int id = await _repository.CrearEmpresa(dato);
            if(id <= 0)
                return Conflict("Error al registrar a la nueva Empresa, revisa los datos.");
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/empresa/{id}";
            //return Created(urlResult, id);
            return Ok(nuevaEmpresa);
        }  

        [HttpPut]
        [Route("ActualizarEmpresa/{id:int}")]

        public async Task<IActionResult> ActualizarEmpresa(int id, [FromBody] EmpresaUpdateRequest actualizarEmpresa)
        {
            var validacion = await _updateValidator.ValidateAsync(actualizarEmpresa);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(dest => $"{dest.PropertyName} => Error: {dest.ErrorMessage}"));
            var dato = _mapper.Map<EmpresaUpdateRequest, Empresa>(actualizarEmpresa);
            if(id <= 0)
                return NotFound("No se encontró la Empresa con el ID introducido.");
            var dato2 = await _repository.ObternerEmpresaID(id);
            if(dato2 == null)
                return NotFound("No se encontró la Empresa con el ID introducido.");
            var Id = await _repository.ActualizarEmpresa(id, dato);
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/empresa/{id}";
            //return Ok("La Empresa se ha actualizado correctamente");
            return Ok(actualizarEmpresa);
        }  

        
        [HttpDelete]
        [Route("BorrarEmpresa/{id:int}")]
        public async Task<IActionResult> EliminarEmpresa ( int id )
        {
            if (id <= 0)
                return NotFound("No se encontró la Empresa con el ID introducido.");
            var entity = await _repository.ObternerEmpresaID(id);
            if(entity == null)
                return NotFound("No se encontró la Empresa con el ID introducido.");
            var deleted = await _repository.EliminarEmpresa(id);
            if(!deleted)
                Conflict("Error al intentar eliminar a la Empresa.");
            return Ok("Empresa eliminado exitosamente.");
    
        }

        [HttpGet]
        [Route("Login/{correo}/{contrasena}")]
        public async Task<IActionResult> Login (string correo, string contrasena)
        {
            var empresa = await _repository.Login(correo, contrasena);
            return Ok(empresa);
        }
    }
}