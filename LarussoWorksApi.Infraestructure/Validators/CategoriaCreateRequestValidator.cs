using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.Dtos.Requests;
using FluentValidation;

namespace LarussoWorksApi.Infraestructure.Validators
{
    public class CategoriaCreateRequestValidator : AbstractValidator<CategoriaCreateRequest>
    {
        public CategoriaCreateRequestValidator()
        {
            RuleFor(x => x.NombreCategoria)
                .NotEmpty().WithMessage("El Nombre de la Categoria, no puede estar vacia")
                .Must(x => x.Length > 3).WithMessage("El Nombre de la Categoria, debe tener mas de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El Nombre de la Categoria, debe tener menos de 51 caracteres");
        }
    }
}