using FluentValidation;

namespace EvaluacionCore.Application.Features.Subturnos.Commands.CreateSubturno;

public class CreateSubturnoCommandValidator : AbstractValidator<CreateSubturnoCommand>
{
    public CreateSubturnoCommandValidator()
    {

        RuleFor(v => v.SubturnoRequest.IdTipoSubturno)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.SubturnoRequest.CodigoSubturno)
          .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
          .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.SubturnoRequest.Entrada)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.SubturnoRequest.Salida)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.SubturnoRequest.MargenEntrada)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");


        RuleFor(v => v.SubturnoRequest.MargenSalida)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

    }
}