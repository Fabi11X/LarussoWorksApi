using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.Dtos.Requests;
using FluentValidation;

namespace LarussoWorksApi.Infraestructure.Validators
{
    public class EmpleoUpdateRequestValidator : AbstractValidator<EmpleoUpdateRequest>
    {
        public EmpleoUpdateRequestValidator()
        {
            RuleFor(x => x.NombreEmpleo)
                .NotEmpty().WithMessage("El Nombre, no puede estar vacio")
                .Must(x => x.Length > 3).WithMessage("El Nombre, debe tener mas de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El Nombre, debe tener menos de 51 caracteres");

            RuleFor(x => x.DescripcionEmpleo)
                .NotEmpty().WithMessage("La Descripción, no debe estar vacia")
                .Must(x => x.Length > 10).WithMessage("La Descripción, deben tener mas de 50 caracteres")
                .Must(x => x.Length < 1000).WithMessage("La Descripción, deben tener menos de 300 caracteres");

            RuleFor(x => x.RequisitosEmpleo)
                .NotEmpty().WithMessage("Los Requisitos, no pueden estar vacias")
                .Must(x => x.Length > 10).WithMessage("Los Requistos, debe tener mas de 20 caracteres")
                .Must(x => x.Length < 1000).WithMessage("La Requisitos, debe tener menos de 200 caracteres");

            RuleFor(x => x.PrestacionesEmpleo)
                .NotEmpty().WithMessage("Las Prestaciones, no pueden estar vacias")
                .Must(x => x.Length > 10).WithMessage("Las Prestaciones, deben tener mas de 20 caracteres")
                .Must(x => x.Length < 1000).WithMessage("La descripcion, deben tener menos de 200 caracteres");

            RuleFor(x => x.UbicacionEmpleo)
                .NotEmpty().WithMessage("La Dirección, no puede estar vacia")
                .Must(x => x.Length > 10).WithMessage("La Dirección, debe tener mas de 20 caracteres")
                .Must(x => x.Length < 100).WithMessage("La Dirección, debe tener menos de 100 caracteres");

            RuleFor(x => x.IdCategoria)
                .NotNull().NotEmpty().WithMessage("La Categoría, no debe ser vacio o nulo (acepta puros enteros 1, 2, etc...)");

            RuleFor(x => x.IdEmpresa)
                .NotNull().NotEmpty().WithMessage("La Empresa, no debe ser vacio o nulo (acepta puros enteros 1, 2, etc...)");     
        }
    }
}