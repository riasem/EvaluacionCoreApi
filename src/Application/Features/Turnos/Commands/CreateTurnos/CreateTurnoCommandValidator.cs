using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Domain.Entities.Asistencia;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurno;

public class CreateTurnoCommandValidator : AbstractValidator<CreateTurnoCommand>
{
    private readonly IConfiguration _config;
    public CreateTurnoCommandValidator(IConfiguration config)
    {
        _config = config;
        var listaModalidadJornada = _config.GetSection("modalidadJornada").Get<List<ModalidadJornadaType>>();
        var listaTipoJornada = _config.GetSection("tipoJornada").Get<List<TipoJornadaType>>();

        RuleFor(v => v.TurnoRequest.IdTipoTurno)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.TurnoRequest.IdClaseTurno)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.TurnoRequest.IdSubclaseTurno)
       .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        RuleFor(v => v.TurnoRequest.IdModalidadJornada)
        .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
        .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
        .Custom((list, context) =>
        {
            var valida = listaModalidadJornada.Where(e => e.Id == list).FirstOrDefault();
            if (valida == null)
            {
                context.AddFailure("{PropertyName} no es una modalidad correcta");
            }
        });

        RuleFor(v => v.TurnoRequest.IdTipoJornada)
        .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
        .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
        .Custom((list, context) =>
        {

            var validaT = listaTipoJornada.Where(e => e.Id == list).FirstOrDefault();
            if (validaT == null)
            {
                context.AddFailure("{PropertyName} no es un tipo de jornada correcta");
            }
        });

        RuleFor(v => v.TurnoRequest.CodigoTurno)
        .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
        .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
        .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");


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