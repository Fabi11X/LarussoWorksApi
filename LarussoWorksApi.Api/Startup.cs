using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using LarussoWorksApi.Infraestructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using LarussoWorksApi.Domain.Interfaces;
using LarussoWorksApi.Infraestructure.Repository;
using LarussoWorksApi.Application.Services;
using FluentValidation;
using LarussoWorksApi.Infraestructure.Validators;
using LarussoWorksApi.Domain.Dtos.Requests;

namespace LarussoWorksApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LarussoWorksApi.Api", Version = "v1" });
            });
            
            services.AddDbContext<LarussoWorksFinallyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProyectoLarussoWorksFinally")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAdministradorService, AdministradorService>();
            services.AddScoped<IEmpleoService, EmpleoService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            services.AddTransient<IUsuarioRepository, UsuarioSqlRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaSqlRepository>();
            services.AddTransient<IEmpleoRepository, EmpleoSqlRepository>();
            services.AddTransient<ICategoriaRepository, CategoriasSqlRepository>();
            services.AddTransient<IAdministradorRepository, AdministradoresSqlRepository>();
            services.AddTransient<ContactanosSqlRepository, ContactanosSqlRepository>();

            services.AddScoped<IValidator<UsuarioCreateRequest>, UsuarioCreateRequestValidator>();
            services.AddScoped<IValidator<UsuarioUpdateRequest>, UsuarioUpdateRequestValidator>();

            services.AddScoped<IValidator<AdministradorCreateRequest>, AdministradorCreateRequestValidator>();
            services.AddScoped<IValidator<AdministradorUpdateRequest>, AdministradorUpdateRequestValidator>();

            services.AddScoped<IValidator<EmpresaCreateRequest>, EmpresaCreateRequestValidator>();
            services.AddScoped<IValidator<EmpresaUpdateRequest>, EmpresaUpdateRequestValidator>();

            services.AddScoped<IValidator<EmpleoCreateRequest>, EmpleoCreateRequestValidator>();
            services.AddScoped<IValidator<EmpleoUpdateRequest>, EmpleoUpdateRequestValidator>();

            services.AddScoped<IValidator<CategoriaCreateRequest>, CategoriaCreateRequestValidator>();
            services.AddScoped<IValidator<CategoriaUpdateRequest>, CategoriaUpdateRequestValidator>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LarussoWorksApi.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
