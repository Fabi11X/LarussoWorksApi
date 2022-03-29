using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LarussoWorksApi.Api.Helpers
{
    public  static class HttpContextExtensions
    {
        public static async Task InsertarParametrosEnRespuesta<T>(this HttpContext context,
        IQueryable<T> queryable, int cantidadRegistroAMostrar)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double conteo = await queryable.CountAsync();
            double totalPaginas = Math.Ceiling(conteo / cantidadRegistroAMostrar);
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
        }
        
    }
}