using System;
using FluentValidation;
using BibliotecaAPI.DTOs;
using System.Text.RegularExpressions;

namespace BibliotecaAPI.Validators
{
    public class UserInsertValidator : AbstractValidator<UserDto>
    {
        public UserInsertValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(u => u.Email).NotNull().WithMessage("El email es requerido");
            RuleFor(u => u.UserName).Length(3, 35).WithMessage("El nombre debe ser de 3 a 35 caracteres");
            RuleFor(u => u.UserId).GreaterThan(0).WithMessage("El ID del usuario debe ser mayor que 0");
            RuleFor(u => u.UserName).Matches("Maricon|gay", RegexOptions.IgnoreCase).WithMessage("Blas maricon gilipollas ostias chupa pollas carajo tio");

        }
    }
}
