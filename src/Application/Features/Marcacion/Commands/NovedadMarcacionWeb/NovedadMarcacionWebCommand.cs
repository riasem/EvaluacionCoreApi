using AutoMapper;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Marcacion.Commands.NovedadMarcacionWeb;

public record NovedadMarcacionWebCommand(string Identificacion, string FiltroNovedades, DateTime fechaDesde, DateTime fechaHasta) : IRequest<ResponseType<List<NovedadMarcacionWebType>>>;

public class NovedadMarcacionWebCommandHandler : IRequestHandler<NovedadMarcacionWebCommand, ResponseType<List<NovedadMarcacionWebType>>>
{

    private readonly IMarcacion _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<NovedadMarcacionWebCommandHandler> _log;

    public NovedadMarcacionWebCommandHandler(IMarcacion repository, IMapper mapper, ILogger<NovedadMarcacionWebCommandHandler> log)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }
    

    public async Task<ResponseType<List<NovedadMarcacionWebType>>> Handle(NovedadMarcacionWebCommand request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.ConsultaNovedadMarcacionWeb(request.Identificacion, request.FiltroNovedades, request.fechaDesde, request.fechaHasta, cancellationToken);

        return objResult;

    }
}
