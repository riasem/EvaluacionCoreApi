using FluentValidation;

namespace EvaluacionCore.Application.Features.Clients.Commands.CreateTurno;

public class CreateTurnoCommandValidator : AbstractValidator<CreateTurnoCommand>
{
    public CreateTurnoCommandValidator()
    {

        RuleFor(v => v.TurnoRequest.IdTipoTurno)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.TurnoRequest.CodigoTurno)
          .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
          .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");



        RuleFor(v => v.TurnoRequest.Entrada)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.TurnoRequest.Salida)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.TurnoRequest.MargenEntrada)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.TurnoRequest.MargenSalida)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

    }
}