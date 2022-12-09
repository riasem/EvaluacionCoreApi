using FluentValidation;

namespace EvaluacionCore.Application.Features.Turnos.Commands.UpdateTurnoColaborador;

public class UpdateTurnoColaboradorCommandValidator : AbstractValidator<UpdateTurnoColaboradorCommand>
{
    public UpdateTurnoColaboradorCommandValidator()
    {

        RuleFor(v => v.TurnoRequest.IdTurno)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

    }
}