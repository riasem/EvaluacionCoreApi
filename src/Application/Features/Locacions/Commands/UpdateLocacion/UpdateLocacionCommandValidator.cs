using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Localidads.Commands.UpdateLocalidad;

public class UpdateLocalidadCommandValidator : AbstractValidator<UpdateLocalidadCommand>
{
    public UpdateLocalidadCommandValidator()
    {
        RuleFor(v => v.UpdateLocalidad.Codigo)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(5).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

        RuleFor(v => v.UpdateLocalidad.Id)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.UpdateLocalidad.Latitud)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.UpdateLocalidad.Longitud)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.UpdateLocalidad.Descripcion)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
    }
}
