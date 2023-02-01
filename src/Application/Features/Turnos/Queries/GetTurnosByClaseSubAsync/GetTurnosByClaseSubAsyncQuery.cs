using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EvaluacionCore.Application.Features.Turnos.Queries.GetTurnosByClaseSubAsync;

public record GetTurnosByClaseSubAsyncQuery(Guid? IdClase, Guid? IdSubClase) : IRequest<ResponseType<List<TurnoType>>>;
public class GetTurnosByClaseSubAsyncQueryHandler : IRequestHandler<GetTurnosByClaseSubAsyncQuery, ResponseType<List<TurnoType>>>
{
    private readonly ILogger<GetTurnosByClaseSubAsyncQueryHandler> _log;
    private readonly IConfiguration _config;
    private readonly IRepositoryAsync<Turno> _repositoryAsync;
    private readonly IRepositoryAsync<ClaseTurno> _repositoryClaseAsync;
    private readonly IRepositoryAsync<SubclaseTurno> _repositorySubClaseAsync;
    private readonly IRepositoryAsync<TipoTurno> _repositoryTurnoAsync;
    private readonly IMapper _mapper;

    public GetTurnosByClaseSubAsyncQueryHandler(IRepositoryAsync<Turno> repository, IConfiguration config,
       IRepositoryAsync<ClaseTurno> repositoryClase, IRepositoryAsync<SubclaseTurno> repositorySubClase,
       IRepositoryAsync<TipoTurno> repositoryTurno, IMapper mapper, ILogger<GetTurnosByClaseSubAsyncQueryHandler> log)
    {
        _config = config;
        _repositoryClaseAsync = repositoryClase;
        _repositorySubClaseAsync = repositorySubClase;
        _repositoryTurnoAsync = repositoryTurno;
        _repositoryAsync = repository;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<List<TurnoType>>> Handle(GetTurnosByClaseSubAsyncQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var objTurno = await _repositoryAsync.ListAsync(cancellationToken);
            

            var obtTurnosPadre = objTurno.Where(e => e.IdTurnoPadre == null && e.IdClaseTurno == (request.IdClase == Guid.Empty || request.IdClase == null ? e.IdClaseTurno : request.IdClase)  && e.IdSubclaseTurno == (request.IdSubClase == Guid.Empty || request.IdSubClase == null ? e.IdSubclaseTurno : request.IdSubClase)).ToList();
            var tipoTurno_ = await _repositoryTurnoAsync.ListAsync(cancellationToken);
            var claseTurno_ = await _repositoryClaseAsync.ListAsync(cancellationToken);
            var subClaseTurno_ = await _repositorySubClaseAsync.ListAsync(cancellationToken);
            var modalidadJornadaTypes = _config.GetSection("modalidadJornada").Get<List<ModalidadJornadaType>>();
            var tipoJornadaTypes = _config.GetSection("tipoJornada").Get<List<TipoJornadaType>>();

            List<TurnoType> listaTurno = new();


            foreach (var item in obtTurnosPadre)
            {
                List<SubturnoType> listaSubturno = new();

                var tipoTurno = tipoTurno_.Where(e => e.Id == item.IdTipoTurno).FirstOrDefault();
                var claseTurno = claseTurno_.Where(e => e.Id == item.IdClaseTurno).FirstOrDefault();
                var subClaseTurno = subClaseTurno_.Where(e => e.Id == item.IdSubclaseTurno).FirstOrDefault();
                var modalidadJornada = modalidadJornadaTypes.Where(e => e.Id == item.IdModalidadJornada).FirstOrDefault();
                var tipoJornada = tipoJornadaTypes.Where(e => e.Id == item.IdTipoJornada).FirstOrDefault();



                var _subturno = objTurno.Where(e => e.IdTurnoPadre == item.Id).ToList();

                if (_subturno.Count > 0)
                {
                    foreach (var subturno_ in _subturno)
                    {
                        listaSubturno.Add(new SubturnoType
                        {
                            Id = subturno_.Id,
                            IdTurnoPadre = subturno_.IdTurnoPadre,
                            TotalHoras = subturno_.TotalHoras,
                            Descripcion = subturno_.Descripcion,
                            Entrada = subturno_.Entrada,
                            Salida = subturno_.Salida,
                            IdTipoTurno = subturno_.IdTipoTurno,
                            CodigoTurno = subturno_.CodigoTurno,
                            TipoTurno = subturno_.IdTipoTurno.ToString(),
                            MargenEntradaPrevio = subturno_.MargenEntradaPrevio,
                            MargenSalidaPosterior = subturno_.MargenSalidaPosterior,
                            MargenEntradaGracia = subturno_.MargenEntradaGracia,
                            MargenSalidaGracia = subturno_.MargenSalidaGracia,
                            CodigoEntrada = subturno_.CodigoEntrada,
                            CodigoSalida = subturno_.CodigoSalida
                        });
                    }
                }

                listaTurno.Add(new TurnoType
                {
                    Id = item.Id,
                    IdTurnoPadre = item.IdTurnoPadre,

                    IdClaseTurno = item.IdClaseTurno,
                    IdModalidadJornada = item.IdModalidadJornada,
                    IdSubclaseTurno = item.IdSubclaseTurno,
                    IdTipoJornada = item.IdTipoJornada,
                    IdTipoTurno = item.IdTipoTurno,

                    ClaseTurno = claseTurno.Descripcion.ToString() ?? "",
                    SubclaseTurno = subClaseTurno.Descripcion.ToString() ?? "",
                    TipoTurno = tipoTurno.Descripcion.ToString() ?? "",
                    TipoJornada = tipoJornada.Descripcion.ToString() ?? "",
                    ModalidadJornada = modalidadJornada.Descripcion.ToString() ?? "",

                    Descripcion = item.Descripcion,
                    CodigoTurno = item.CodigoTurno,
                    Entrada = item.Entrada,
                    Salida = item.Salida,
                    CodigoIntegracion = item.CodigoIntegracion,
                    MargenEntradaPrevio = item.MargenEntradaPrevio,
                    MargenSalidaPosterior = item.MargenSalidaPosterior,
                    MargenEntradaGracia = item.MargenEntradaGracia,
                    MargenSalidaGracia = item.MargenSalidaGracia,
                    CodigoEntrada = item.CodigoEntrada,
                    CodigoSalida = item.CodigoSalida,


                    TotalHoras = item.TotalHoras,
                    SubturnoType = listaSubturno
                });

            };

            return new ResponseType<List<TurnoType>>() { Data = listaTurno, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<List<TurnoType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
        }


 
    }
}
