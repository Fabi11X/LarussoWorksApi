using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.Dtos.Requests;
using FluentValidation;

namespace LarussoWorksApi.Infraestructure.Validators
{
    public class EmpresaUpdateRequestValidator : AbstractValidator<EmpresaUpdateRequest>
    {
        public EmpresaUpdateRequestValidator()
        {
            RuleFor(x => x.NombreEmpresa)
                .NotEmpty().WithMessage("El Nombre, no puede estar vacio")
                .Must(x => x.Length > 3).WithMessage("El Nombre, debe tener mas de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El Nombre, debe tener menos de 51 caracteres");

            RuleFor(x => x.DescripcionEmpresa)
                .NotEmpty().WithMessage("La Descripción, no debe estar vacia")
                .Must(x => x.Length > 10).WithMessage("La Descripción, debe tener mas de 20 caracteres")
                .Must(x => x.Length < 1000).WithMessage("La Descripción, debe tener menos de 299 caracteres");

            RuleFor(x => x.TelefonoEmpresa)
                .NotNull()
                .NotEmpty().WithMessage("El Teléfono, no debe estar vacio")
                .Length(10).WithMessage("Teléfono de contacto,  debe tener una longitud de '10' caracteres")
                .Must(x => x != "1111111111" && x != "2222222222"
                    && x != "3333333333" && x != "4444444444"
                    && x != "5555555555" && x != "6666666666"
                    && x != "7777777777" && x != "8888888888"
                    && x != "9999999999").WithMessage("Teléfono de contacto, no tiene un formato valido (No repetir 10 digitos)");
        }
    }
}