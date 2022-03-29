using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LarussoWorksApi.Domain.Dtos.Requests;
using FluentValidation;

namespace LarussoWorksApi.Infraestructure.Validators
{
    public class UsuarioUpdateRequestValidator : AbstractValidator<UsuarioUpdateRequest>
    {
        public UsuarioUpdateRequestValidator()
        {
            
            RuleFor(x => x.NombreUsuario)
                .NotEmpty().WithMessage("El Nombre, no puede estar vacio")
                .Must(x => x.Length > 3).WithMessage("El Nombre, debe tener mas de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El Nombre, debe tener menos de 51 caracteres");

            RuleFor(x => x.ApellidoUsuario)
                .NotEmpty().WithMessage("El Apellido, no puede estar vacio")
                .Must(x => x.Length > 3).WithMessage("El Apellido, debe tener mas de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El Apellido, debe tener menos de 51 caracteres");

            RuleFor(x => x.SexoUsuario)
                .NotNull().WithMessage("El Sexo del Usuario, no debe estar vacio")
                .Must(x => x == "Femenino" || x == "Masculino").WithMessage("Escriba Femenimo o Masculino segun sea el caso, por favor");

            RuleFor(dest => dest.FechanacUsuario).NotNull().NotEmpty().Length(10, 10).Must(dest => dest.Contains("/"))
                .LessThanOrEqualTo(DateTime.Today.Date.ToString("yyyy/MM/dd")).WithMessage("La Fecha de Nacimiento no es valida (2000/12/31)");

            RuleFor(x => x.TelefonoUsuario)
                .NotNull()
                .NotEmpty().WithMessage("El Teléfono, no debe estar vacio")
                .Length(10).WithMessage("Teléfono de contacto, debe tener una longitud de '10' caracteres")
                .Must(x => x != "1111111111" && x != "2222222222"
                    && x != "3333333333" && x != "4444444444"
                    && x != "5555555555" && x != "6666666666"
                    && x != "7777777777" && x != "8888888888"
                    && x != "9999999999").WithMessage("Teléfono de contacto, no tiene un formato valido (No repetir 10 digitos)");

                    
        }
    }
}