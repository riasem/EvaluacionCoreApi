using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Commands.CreateLocacion;

public class CreateLocacionCommandValidator : AbstractValidator<CreateLocacionCommand>
{
    public CreateLocacionCommandValidator()
    {
        RuleFor(v => v.LocacionRequest.Codigo)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(5).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

        RuleFor(v => v.LocacionRequest.IdEmpresa)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.LocacionRequest.Latitud)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.LocacionRequest.Longitud)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.LocacionRequest.Descripcion)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

    }
}
