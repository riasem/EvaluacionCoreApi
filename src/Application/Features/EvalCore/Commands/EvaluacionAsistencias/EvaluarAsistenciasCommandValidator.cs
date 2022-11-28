using FluentValidation;

namespace EvaluacionCore.Application.Features.EvalCore.Commands.EvaluacionAsistencias;

public class EvaluarAsistenciasCommandValidator : AbstractValidator<EvaluarAsistenciasCommand>
{
    public EvaluarAsistenciasCommandValidator()
    {

        When(v => v.Identificacion.ToUpper() != "", () =>
        {
            RuleFor(v => v.Identificacion)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
                .MinimumLength(10).WithMessage("{PropertyName} debe tener por lo menos {MinLength} caracteres")
                .Matches("^[0-9]+$").WithMessage("{PropertyName} debe contener ser solo numeros.");
        });

        //When(v => v.FechaDesde != null && v.FechaHasta != null, () =>
        //{
        //    RuleFor(v => v.FechaDesde > v.FechaHasta)
        //});
    }
}