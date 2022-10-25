using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Commands.UpdateLocacion;

public class UpdateLocacionCommandValidator : AbstractValidator<UpdateLocacionCommand>
{
    public UpdateLocacionCommandValidator()
    {
        RuleFor(v => v.UpdateLocacion.Codigo)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(5).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

        RuleFor(v => v.UpdateLocacion.Id)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.UpdateLocacion.Latitud)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.UpdateLocacion.Longitud)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.UpdateLocacion.Descripcion)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
    }
}
