using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.Turnos.Queries.GetTurnosAsync;

public record GetTurnosAsyncQuery() : IRequest<ResponseType<List<TurnoResponseType>>>;

public class GetTurnosAsyncHandler : IRequestHandler<GetTurnosAsyncQuery, ResponseType<List<TurnoResponseType>>>
{
    private readonly IConfiguration _config;
    private readonly IRepositoryAsync<Turno> _repositoryAsync;
    private readonly IRepositoryAsync<ClaseTurno> _repositoryClaseAsync;
    private readonly IRepositoryAsync<SubclaseTurno> _repositorySubClaseAsync;
    private readonly IRepositoryAsync<TipoTurno> _repositoryTurnoAsync;
    private readonly IMapper _mapper;

    //private readonly ITurnoRepository _repository;


    public GetTurnosAsyncHandler(IRepositoryAsync<Turno> repository, IConfiguration config,
        IRepositoryAsync<ClaseTurno> repositoryClase, IRepositoryAsync<SubclaseTurno> repositorySubClase,
        IRepositoryAsync<TipoTurno> repositoryTurno, IMapper mapper)
    {
        _config = config;
        _repositoryClaseAsync = repositoryClase;
        _repositorySubClaseAsync = repositorySubClase;
        _repositoryTurnoAsync = repositoryTurno;
        _repositoryAsync = repository;
        _mapper = mapper;
    }


    public async Task<ResponseType<List<TurnoResponseType>>> Handle(GetTurnosAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objTurno = await _repositoryAsync.ListAsync(cancellationToken);
            var obtTurnosPadre = objTurno.Where(e => e.IdTurnoPadre == null).ToList();


            var modalidadJornadaTypes = _config.GetSection("modalidadJornada").Get<List<ModalidadJornadaType>>();
            var tipoJornadaTypes = _config.GetSection("tipoJornada").Get<List<TipoJornadaType>>();


            List <TurnoType> listaTurno = new();
            List<TurnoResponseType> lista = new();

            if (objTurno.Count < 1)
            {
                return new ResponseType<List<TurnoResponseType>>() { Data = null, Succeeded = true, StatusCode = "001", Message = "La consulta no retorna datos" };
            }

            var agr = objTurno.GroupBy(x => x.IdTipoJornada).ToList();

            var tipoTurno_ = await _repositoryTurnoAsync.ListAsync(cancellationToken);
            var claseTurno_ = await _repositoryClaseAsync.ListAsync(cancellationToken);
            var subClaseTurno_ = await _repositorySubClaseAsync.ListAsync(cancellationToken);
            

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
                            MargenEntrada = subturno_.MargenEntrada,
                            MargenSalida = subturno_.MargenSalida,
                            Salida = subturno_.Salida,
                            IdTipoTurno = subturno_.IdTipoTurno,
                            CodigoTurno = subturno_.CodigoTurno,
                            TipoTurno = subturno_.IdTipoTurno.ToString(),
                            MargenEntradaPosterior = subturno_.MargenEntradaPosterior,
                            MargenEntradaPrevio = subturno_.MargenEntradaPrevio,
                            MargenSalidaPosterior = subturno_.MargenSalidaPosterior,
                            MargenSalidaPrevio = subturno_.MargenSalidaPrevio
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

                    ClaseTurno =  claseTurno.Descripcion.ToString() ?? "",
                    SubclaseTurno = subClaseTurno.Descripcion.ToString() ?? "",
                    TipoTurno = tipoTurno.Descripcion.ToString() ?? "",
                    TipoJornada = tipoJornada.Descripcion.ToString() ?? "",
                    ModalidadJornada = modalidadJornada.Descripcion.ToString() ?? "",

                    Descripcion = item.Descripcion,
                    CodigoTurno = item.CodigoTurno,
                    Entrada = item.Entrada,
                    MargenEntrada = item.MargenEntrada,
                    Salida = item.Salida,
                    MargenSalida = item.MargenSalida,
                    MargenEntradaPosterior = item.MargenEntradaPosterior,
                    MargenEntradaPrevio = item.MargenEntradaPrevio,
                    MargenSalidaPosterior = item.MargenSalidaPosterior,
                    MargenSalidaPrevio = item.MargenSalidaPrevio,
                    CodigoIntegracion = item.CodigoIntegracion,

                    TotalHoras = item.TotalHoras,
                    SubturnoType = listaSubturno
                });

            };

            var prev = listaTurno.GroupBy(x => x.TipoJornada).ToList();

            for (int i = 0; i < prev.Count; i++)
            {
                var tipoJ = prev[i].Key.ToString();
                var idTipoJ = tipoJornadaTypes.Where(e => e.Descripcion == tipoJ).FirstOrDefault().Id;
                lista.Add(new TurnoResponseType
                {
                    IdTipoJornada = idTipoJ.ToString(),
                    TipoJornada = tipoJ,
                    TurnoType = _mapper.Map<List<TurnoType>>(prev[i])
                });
            }

            return new ResponseType<List<TurnoResponseType>>() { Data = lista, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        }
        catch (Exception e)
        {
            return new ResponseType<List<TurnoResponseType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }
        
    }
}