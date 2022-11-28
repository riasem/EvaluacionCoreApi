using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.EvalCore.Commands.EvaluacionAsistencias;

public record EvaluarAsistenciasCommand(string Identificacion, DateTime? FechaDesde, DateTime? FechaHasta) : IRequest<ResponseType<string>>;


public class EvaluarAsistenciasCommandHandler : IRequestHandler<EvaluarAsistenciasCommand, ResponseType<string>>
{
    private readonly IEvaluacion _repository;
    private readonly IMapper _mapper;

    public EvaluarAsistenciasCommandHandler(IRepositoryAsync<TurnoColaborador> repositoryTurnoCol, IRepositoryAsync<LocalidadColaborador> repositoryLocalidadCol, 
        IRepositoryAsync<Turno> repositoryTurno, IConfiguration config, IMapper mapper, IEvaluacion repository)
    {
        _repository = repository; 
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(EvaluarAsistenciasCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var (objResult, sucess) = await _repository.EvaluateAsistencias(request.Identificacion, request.FechaDesde, request.FechaHasta);

            return new ResponseType<string>() { Data = null, Message = objResult, StatusCode = sucess == 1 ? "000" : "001", Succeeded = sucess == 1 };
        }
        catch (Exception)
        {
            return new ResponseType<string>() { Data = null, Message = "Ocurrió un error.", StatusCode = "002", Succeeded = false };
        }
        
    }

}
