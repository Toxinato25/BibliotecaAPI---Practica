using System;
using FluentValidation;
using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Validators
{
    public class LoanInsertValidator : AbstractValidator<LoanDto>
    {
        public LoanInsertValidator()
        {
            RuleFor(u => u.UserId).GreaterThan(0).WithMessage("El ID del usuario debe ser mayor que 0");
            RuleFor(u => u.BookId).GreaterThan(0).WithMessage("El ID del libro debe ser mayor que 0");
            RuleFor(u => u.LoanDate).NotNull().WithMessage("La fecha de préstamo no puede ser null");
            RuleFor(u => u.LoanId).GreaterThan(0).WithMessage("El ID del préstamo debe ser mayor que 0");

        }
    }
}
