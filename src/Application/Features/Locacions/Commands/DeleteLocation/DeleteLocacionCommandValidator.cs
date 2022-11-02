using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Localidads.Commands.DeleteLocation;

public class DeleteLocalidadCommandValidator : AbstractValidator<DeleteLocalidadCommand>
{
    public DeleteLocalidadCommandValidator()
    {
        RuleFor(v => v.IdLocalidad)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
    }
}
