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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;
        private readonly ICategoriaRepository _repository;

        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoriaCreateRequest> _createValidator;
        private readonly IValidator<CategoriaUpdateRequest> _updateValidator;

        private readonly LarussoWorksFinallyContext _context;

        public CategoriaController(ICategoriaService service, 
            ICategoriaRepository repository, 
            IHttpContextAccessor httpContext, 
            IMapper mapper,
            IValidator<CategoriaCreateRequest> createValidator,
            IValidator<CategoriaUpdateRequest> updateValidator,
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
        [Route("ObtenerCategoria")]
        public async Task<IActionResult> ObtenerCategoria([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var categorias = await _repository.ObtenerCategoria();
            //var respcategoria = _mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaResponse>>(categorias);
            return Ok (categorias);
        }

        [HttpGet]
        [Route("ObtenerCategoriaII")] 


          public async Task<IActionResult> ObtenerCategoriaII([FromQuery] Paginacion paginacion, [FromQuery] string nombre)
        {
            var queryable = _context.Categorias.AsQueryable();
            if(! string.IsNullOrEmpty(nombre))
            {
                queryable = queryable.Where(x => x.NombreCategoria.Contains(nombre));
            }
            await HttpContext.InsertarParametrosEnRespuesta(queryable, paginacion.CantidadAMostrar);
            var categorias = await _repository.ObtenerCategoria();
            return Ok (queryable.Paginar(paginacion));

        
            //var respusuario = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioResponse>>(usuarios);
        
            
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> ObternerCategoriaID(int id)
        {
            var categoria = await _repository.ObternerCategoriaID(id);
            if(categoria == null)
            {
                return NotFound("La Categoria con ese ID no existe.");
            }
            //var respuesta = _mapper.Map<Categoria, CategoriaResponse>(categoria);
            return Ok(categoria);
        } 

        [HttpPost]
        [Route("CrearCategoria")]
        public async Task<IActionResult> CrearCategoria (CategoriaCreateRequest nuevaCategoria)
        {
            var validacion = await _createValidator.ValidateAsync(nuevaCategoria);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(x => $"{x.PropertyName} => Error: {x.ErrorMessage}"));
            var dato = _mapper.Map<CategoriaCreateRequest, Categoria>(nuevaCategoria);
            int id = await _repository.CrearCategoria(dato);
            if(id <= 0)
                return Conflict("Error al registrar la nueva Categoria, revisa los datos.");
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/categoria/{id}";
            //return Created(urlResult, id);
            return Ok(nuevaCategoria);
        }  

        [HttpPut]
        [Route("ActualizarCategoria/{id:int}")]
        public async Task<IActionResult> ActualizarCategoria ( int id, [FromBody] CategoriaUpdateRequest actualizarCategoria)
        {
            var validacion = await _updateValidator.ValidateAsync(actualizarCategoria);
            if(!validacion.IsValid)
                return UnprocessableEntity(validacion.Errors.Select(dest => $"{dest.PropertyName} => Error: {dest.ErrorMessage}"));
            var dato = _mapper.Map<CategoriaUpdateRequest, Categoria>(actualizarCategoria);
            if(id <= 0)
                return NotFound("No se encontró la Categoria con el id introducido.");
            var dato2 = await _repository.ObternerCategoriaID(id);
            if(dato2 == null)
                return NotFound("No se encontró la Categoria con el id introducido.");
            var Id = await _repository.ActualizarCategoria(id, dato);
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/categoria/{id}";
            //return Ok("La Categoria se ha actualizado correctamente.");
            return Ok(actualizarCategoria);
        }  

        [HttpDelete]
        [Route("BorrarCategoria/{id:int}")]
        public async Task<IActionResult> EliminarCategoria ( int id )
        {
            if (id <= 0)
                return NotFound("No se encontró la Categoria con el ID introducido.");
            var entity = await _repository.ObternerCategoriaID(id);
            if(entity == null)
                return NotFound("No se encontró la Categoria con el ID introducido.");
            var deleted = await _repository.EliminarCategoria(id);
            if(!deleted)
                Conflict("Error al intentar eliminar la Categoria.");
            return Ok("Categoria eliminado exitosamente.");
        }  
    }
}