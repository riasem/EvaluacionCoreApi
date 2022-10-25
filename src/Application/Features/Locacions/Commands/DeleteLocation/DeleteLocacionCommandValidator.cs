using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Commands.DeleteLocation;

public class DeleteLocacionCommandValidator : AbstractValidator<DeleteLocacionCommand>
{
    public DeleteLocacionCommandValidator()
    {
        RuleFor(v => v.IdLocacion)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
    }
}
