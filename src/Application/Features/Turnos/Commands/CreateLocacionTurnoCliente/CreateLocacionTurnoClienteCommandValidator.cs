using EvaluacionCore.Application.Features.Turnos.Commands.CreateLocalidadTurnoCliente;
using FluentValidation;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateLocalidadTurnoCliente;

public class CreateSubturnoClienteCommandValidator : AbstractValidator<CreateLocalidadTurnoClienteCommand>
{
    public CreateSubturnoClienteCommandValidator()
    {

        RuleFor(v => v.TurnoRequest.IdSubturno)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.TurnoRequest.IdCliente)
          .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
          .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

    }
}