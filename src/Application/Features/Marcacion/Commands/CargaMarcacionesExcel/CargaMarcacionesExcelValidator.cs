using EvaluacionCore.Application.Features.EvalCore.Commands.EvaluacionAsistencias;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesExcel;

public class CargaMarcacionesExcelValidator : AbstractValidator<CargaMarcacionesExcelCommand>
{
    public CargaMarcacionesExcelValidator()
    {
        RuleForEach(x => x.marcacionesExcel).ChildRules(marcacion =>
        {
            marcacion.RuleFor(x => x.Identificacion)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
            .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
            .MinimumLength(10).WithMessage("{PropertyName} debe tener por lo menos {MinLength} caracteres")
            .Matches("^[0-9]+$").WithMessage("{PropertyName} debe contener ser solo numeros.");


            marcacion.RuleFor(x => x.IdentificacionDispositivo)
            .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
            .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
            .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
            .MinimumLength(10).WithMessage("{PropertyName} debe tener por lo menos {MinLength} caracteres")
            .Matches("^[0-9]+$").WithMessage("{PropertyName} debe contener ser solo numeros.");

        }
        );
    }
}
