using System.Linq;
using LarussoWorksApi.Domain.entities;

namespace LarussoWorksApi.Api.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, Paginacion paginacion)
        {
            return queryable
            .Skip((paginacion.pagina -1) * paginacion.CantidadAMostrar)
            .Take(paginacion.CantidadAMostrar);
            
        }
    }
}