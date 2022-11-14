using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Queries.GetTurnoById;

public record GetTurnosAsyncQuery() : IRequest<ResponseType<List<TurnoResponseType>>>;

public class GetTurnosAsyncHandler : IRequestHandler<GetTurnosAsyncQuery, ResponseType<List<TurnoResponseType>>>
{
    private readonly IRepositoryAsync<Turno> _repositoryAsync;
    //private readonly IRepositoryAsync<SubTurno> _repositorySubtAsync;
    private readonly IRepositoryAsync<TipoTurno> _repositoryTurnoAsync;
    private readonly IMapper _mapper;

    //private readonly ITurnoRepository _repository;


    public GetTurnosAsyncHandler(IRepositoryAsync<Turno> repository,/* IRepositoryAsync<SubTurno> repositorySubt,*/ IRepositoryAsync<TipoTurno> repositoryTurno, IMapper mapper)
    {
        //_repositorySubtAsync = repositorySubt;
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
            var _subturno = objTurno.Where(e => e.IdTurnoPadre != null).ToList();


            List <TurnoType> listaTurno = new();
            List<TurnoResponseType> lista = new();

            if (objTurno.Count < 1)
            {
                return new ResponseType<List<TurnoResponseType>>() { Data = null, Succeeded = true, StatusCode = "001", Message = "La consulta no retorna datos" };
            }

            var agr = objTurno.GroupBy(x => x.IdTipoJornada).ToList();



            foreach (var item in objTurno)
            {

                List<SubturnoType> listaSubturno = new();

                var tipoTurno = await _repositoryTurnoAsync.FirstOrDefaultAsync(new TipoTurnoByIdSpec(item.IdTipoTurno), cancellationToken);
                //var _subturno = await _repositorySubtAsync.ListAsync(new SubturnoByTurnoIdSpec(item.Id), cancellationToken);

                if (_subturno.Count > 0)
                {
                    foreach (var subturno_ in _subturno)
                    {
                        listaSubturno.Add(new SubturnoType
                        {
                            TotalHoras = subturno_.TotalHoras,
                            Descripcion = subturno_.Descripcion,
                            Entrada = subturno_.Entrada,
                            MargenEntrada = subturno_.MargenEntrada,
                            MargenSalida = subturno_.MargenSalida,
                            Salida = subturno_.Salida,
                            IdTipoTurno = subturno_.IdTipoTurno,
                        });
                    }
                }

                listaTurno.Add(new TurnoType
                {
                    Id = item.Id,
                    TipoTurno = tipoTurno.Descripcion,
                    TipoJornada = item.IdTipoJornada.ToString(),
                    Descripcion = item.Descripcion,
                    CodigoTurno = item.CodigoTurno,
                    Entrada = item.Entrada,
                    MargenEntrada = item.MargenEntrada,
                    Salida = item.Salida,
                    MargenSalida = item.MargenSalida,
                    TotalHoras = item.TotalHoras,
                    SubturnoType = listaSubturno
                });

            };

            var prev = listaTurno.GroupBy(x => x.TipoJornada).ToList();

            for (int i = 0; i < prev.Count; i++)
            {
                lista.Add(new TurnoResponseType
                {
                    TipoJornada = prev[i].Key.ToString(),
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