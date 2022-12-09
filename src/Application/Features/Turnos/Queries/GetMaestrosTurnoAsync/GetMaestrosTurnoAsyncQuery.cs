using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.Turnos.Queries.GetMaestrosTurnoAsync;

public record GetMaestrosTurnoAsyncQuery() : IRequest<ResponseType<MaestrosTurnoResponseType>>;

public class GetMaestrosTurnoAsyncHandler : IRequestHandler<GetMaestrosTurnoAsyncQuery, ResponseType<MaestrosTurnoResponseType>>
{
    private readonly IRepositoryAsync<ClaseTurno> _repositoryClassAsync;
    private readonly IRepositoryAsync<SubclaseTurno> _repositorySubcAsync;
    private readonly IRepositoryAsync<TipoTurno> _repositoryTurnoAsync;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private string uriEnpoint = "";

    //private readonly ITurnoRepository _repository;


    public GetMaestrosTurnoAsyncHandler(IRepositoryAsync<ClaseTurno> repository, IRepositoryAsync<SubclaseTurno> repositorySubt, IConfiguration config, IRepositoryAsync<TipoTurno> repositoryTurno, IMapper mapper)
    {
        _repositorySubcAsync = repositorySubt;
        _repositoryTurnoAsync = repositoryTurno;
        _repositoryClassAsync = repository;
        _mapper = mapper;
        _config = config;
    }


    public async Task<ResponseType<MaestrosTurnoResponseType>> Handle(GetMaestrosTurnoAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objClaseTurno = await _repositoryClassAsync.ListAsync(cancellationToken);
            var objSubclaseTurno = await _repositorySubcAsync.ListAsync(cancellationToken);
            var objTipoTurno = await _repositoryTurnoAsync.ListAsync(cancellationToken);

            List<TipoJornadaType> listaTipoJornada = new();
            List<ModalidadJornadaType> listaModalidadJornada = new();

            var modalidadJornadaTypes = _config.GetSection("modalidadJornada").Get<List<ModalidadJornadaType>>();
            var tipoJornadaTypes = _config.GetSection("tipoJornada").Get<List<TipoJornadaType>>();

            MaestrosTurnoResponseType listaMaestrosTurno = new()
            {
                ClaseTurnoType = _mapper.Map<List<ClaseTurnoType>>(objClaseTurno),
                TipoTurnoType = _mapper.Map<List<TipoTurnoType>>(objTipoTurno),
                SubclaseTurnoType = _mapper.Map<List<SubclaseTurnoType>>(objSubclaseTurno),
                TipoJornadaType = tipoJornadaTypes,
                ModalidadJornadaType = modalidadJornadaTypes
            };

            return new ResponseType<MaestrosTurnoResponseType>() { Data = listaMaestrosTurno, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        }
        catch (Exception e)
        {
            return new ResponseType<MaestrosTurnoResponseType>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }

    }
}