using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Queries.GetTurnoById;

public record GetTurnosAsyncQuery() : IRequest<ResponseType<List<TurnoResponseType>>>;

public class GetTurnosAsyncHandler : IRequestHandler<GetTurnosAsyncQuery, ResponseType<List<TurnoResponseType>>>
{
    private readonly IRepositoryAsync<Turno> _repositoryAsync;
    private readonly IRepositoryAsync<TipoTurno> _repositoryTurnoAsync;
    private readonly IMapper _mapper;

    //private readonly ITurnoRepository _repository;


    public GetTurnosAsyncHandler(IRepositoryAsync<Turno> repository, IRepositoryAsync<TipoTurno> repositoryTurno, IMapper mapper)
    {
        _repositoryTurnoAsync = repositoryTurno;
        _repositoryAsync = repository;
        _mapper = mapper;
    }

   
    public async Task<ResponseType<List<TurnoResponseType>>> Handle(GetTurnosAsyncQuery request, CancellationToken cancellationToken)
    {
        var objTurno = await _repositoryAsync.ListAsync(cancellationToken);
        List<TurnoResponseType> lista = new();
        List<TurnoType> listaTurno = new();

        var agrupado = from turno in objTurno group turno by turno.IdTipoTurno;


        if (objTurno.Count < 1)
        {
            return new ResponseType<List<TurnoResponseType>>() { Data = null, Succeeded = false, Message = "Registros no encontrados" };
        }

        var agr = objTurno.GroupBy(x => x.IdTipoTurno).ToList();


        foreach (var itema in agrupado)
        {
            var tipoTurno = await _repositoryTurnoAsync.FirstOrDefaultAsync(new TipoTurnoByIdSpec(itema.FirstOrDefault().IdTipoTurno), cancellationToken);

            lista.Add(new TurnoResponseType
            {
                TipoTurno = tipoTurno.Descripcion,
                TurnoType = _mapper.Map<List<TurnoType>>(itema) 
            });

        };

        return new ResponseType<List<TurnoResponseType>>() { Data = lista, Succeeded = true, Message = "Registros encontrados" };
    }
}