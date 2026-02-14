using System;
using FluentValidation;
using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Validators
{
    public class BookInsertValidator : AbstractValidator<BookDto>
    {
        public BookInsertValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("El título es requerido");
            RuleFor(i => i.Author).NotEmpty().WithMessage("El autor es requerido");
            RuleFor(i => i.BookId).GreaterThan(0).WithMessage("El ID del libro debe ser mayor que 0");
            RuleFor(i => i.Genre).NotEmpty().WithMessage("El género es requerido");
        }
    }
}
