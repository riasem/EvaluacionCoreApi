using EvaluacionCore.Application.Features.Turnos.Commands.AsignarTurno;
using FluentValidation;

namespace EvaluacionCore.Application.Features.Turnos.Commands.AsignarTurno;

public class AsignarTurnoCommandValidator : AbstractValidator<AsignarTurnoCommand>
{
    public AsignarTurnoCommandValidator()
    {

        RuleFor(v => v.TurnoRequest.IdSubturno)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.TurnoRequest.IdCliente)
          .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
          .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

    }
}