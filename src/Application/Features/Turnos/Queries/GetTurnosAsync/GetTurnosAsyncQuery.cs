using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Clients.Dto;
using EvaluacionCore.Application.Features.Prospectos.Specifications;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Clients.Queries.GetTurnoById;

public record GetTurnosAsyncQuery() : IRequest<ResponseType<List<TurnoType>>>;

public class GetTurnosAsyncHandler : IRequestHandler<GetTurnosAsyncQuery, ResponseType<List<TurnoType>>>
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

   
    public async Task<ResponseType<List<TurnoType>>> Handle(GetTurnosAsyncQuery request, CancellationToken cancellationToken)
    {
        var objTurno = await _repositoryAsync.ListAsync(cancellationToken);
        List<TurnoType> lista = new List<TurnoType>();

        if (objTurno is null)
        {
            return new ResponseType<List<TurnoType>>() { Data = null, Succeeded = false, Message = "Registros no encontrados" };
        }

        foreach (var item in objTurno)
        {
            var tipoTurno = await _repositoryTurnoAsync.FirstOrDefaultAsync(new TipoTurnoByIdSpec(item.IdTipoTurno), cancellationToken);
            lista.Add(new TurnoType
            {
                CodigoTurno = item.CodigoTurno,
                Descripcion = item.Descripcion,
                Entrada = item.Entrada,
                Salida = item.Salida,
                IdTipoTurno = item.IdTipoTurno,
                TipoTurno = tipoTurno.Descripcion,
                MargenEntrada = item.MargenEntrada,
                MargenSalida = item.MargenSalida,
                TotalHoras = item.TotalHoras
            });
        }

        return new ResponseType<List<TurnoType>>() { Data = lista, Succeeded = true, Message = "Registros encontrados" };
    }
}