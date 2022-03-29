using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using LarussoWorksApi.Domain.Dtos.Requests;
using LarussoWorksApi.Domain.Dtos.Responses;
using LarussoWorksApi.Domain.entities;

namespace LarussoWorksApi.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioResponse>()
            .ForMember(destino => destino.NombreCompleto, opt => opt.MapFrom(src => $"{src.NombreUsuario} {src.ApellidoUsuario}"))
            .ForMember(destino => destino.DatosUsuario, opt => opt.MapFrom(src => $"Correo: {src.CorreoUsuario} y Telefono: {src.TelefonoUsuario}"));

            CreateMap<UsuarioCreateRequest, Usuario>();
            CreateMap<UsuarioUpdateRequest, Usuario>();

            CreateMap<Empresa, EmpresaResponse>()
            .ForMember(destino => destino.DatosEmpresa, opt => opt.MapFrom(src => $"Correo: {src.CorreoEmpresa} y Telefono: {src.TelefonoEmpresa}"));
            
            CreateMap<EmpresaCreateRequest, Empresa>();
            CreateMap<EmpresaUpdateRequest, Empresa>();

            CreateMap<Empleo, EmpleoResponse>()
            .ForMember(destino => destino.DatosEmpleo, opt => opt.MapFrom(src => $"Requisitos: {src.RequisitosEmpleo} y Prestaciones: {src.PrestacionesEmpleo}"));

            CreateMap<EmpleoCreateRequest, Empleo>();
            CreateMap<EmpleoUpdateRequest, Empleo>();

            CreateMap<Administrador, AdministradorResponse>()
            .ForMember(destino => destino.NombreCompletoAdmin, opt => opt.MapFrom(src => $"{src.NombreAdmin} {src.ApellidoAdmin}"))
            .ForMember(destino => destino.DatosAdmin, opt => opt.MapFrom(src => $"Correo: {src.CorreoAdmin} y Telefono {src.TelefonoAdmin}"));

            CreateMap<AdministradorCreateRequest, Administrador>();
            CreateMap<AdministradorUpdateRequest, Administrador>();

            CreateMap<Categoria, CategoriaResponse>();

            CreateMap<CategoriaCreateRequest, Categoria>();
            CreateMap<CategoriaUpdateRequest, Categoria>();
        }
    }
}