using FluentValidation;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador;

public class CreateTurnoColaboradorCommandValidator : AbstractValidator<CreateTurnoColaboradorCommand>
{
    public CreateTurnoColaboradorCommandValidator()
    {

        RuleFor(v => v.TurnoRequest.IdTurno)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

    }
}