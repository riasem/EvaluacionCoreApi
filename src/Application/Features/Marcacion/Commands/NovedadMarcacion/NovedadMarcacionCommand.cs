using AutoMapper;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Marcacion.Commands.NovedadMarcacion;

public record NovedadMarcacionCommand(string Identificacion, DateTime fechaDesde, DateTime fechaHasta) : IRequest<ResponseType<List<NovedadMarcacionType>>>;

public class NovedadMarcacionCommandHandler : IRequestHandler<NovedadMarcacionCommand, ResponseType<List<NovedadMarcacionType>>>
{

    private readonly IMarcacion _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<NovedadMarcacionCommandHandler> _log;

    public NovedadMarcacionCommandHandler(IMarcacion repository, IMapper mapper, ILogger<NovedadMarcacionCommandHandler> log)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }
    

    public async Task<ResponseType<List<NovedadMarcacionType>>> Handle(NovedadMarcacionCommand request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.ConsultaNovedadMarcacion(request.Identificacion, request.fechaDesde, request.fechaHasta, cancellationToken);

        return objResult;

    }
}
