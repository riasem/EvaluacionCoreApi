using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Localidads.Commands.CreateLocalidad;

public class CreateLocalidadCommandValidator : AbstractValidator<CreateLocalidadCommand>
{
    public CreateLocalidadCommandValidator()
    {
        RuleFor(v => v.LocalidadRequest.Codigo)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(5).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

        RuleFor(v => v.LocalidadRequest.IdEmpresa)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.LocalidadRequest.Latitud)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.LocalidadRequest.Longitud)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.LocalidadRequest.Descripcion)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
       .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

    }
}
