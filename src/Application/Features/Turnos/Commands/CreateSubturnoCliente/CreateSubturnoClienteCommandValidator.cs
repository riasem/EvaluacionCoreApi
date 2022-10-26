using FluentValidation;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateSubturnoCliente;

public class CreateSubturnoClienteCommandValidator : AbstractValidator<CreateSubturnoClienteCommand>
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