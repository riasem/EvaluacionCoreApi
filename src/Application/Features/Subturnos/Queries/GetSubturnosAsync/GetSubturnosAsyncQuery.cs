using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Subturnos.Dto;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Subturnos.Queries.GetSubturnoById;

public record GetSubturnosAsyncQuery() : IRequest<ResponseType<List<SubturnoType>>>;

public class GetSubturnosAsyncHandler : IRequestHandler<GetSubturnosAsyncQuery, ResponseType<List<SubturnoType>>>
{
    private readonly IRepositoryAsync<SubTurno> _repositoryAsync;
    private readonly IRepositoryAsync<TipoSubTurno> _repositorySubturnoAsync;
    private readonly IMapper _mapper;

    //private readonly ISubturnoRepository _repository;


    public GetSubturnosAsyncHandler(IRepositoryAsync<SubTurno> repository, IRepositoryAsync<TipoSubTurno> repositorySubturno, IMapper mapper)
    {
        _repositorySubturnoAsync = repositorySubturno;
        _repositoryAsync = repository;
        _mapper = mapper;
    }

   
    public async Task<ResponseType<List<SubturnoType>>> Handle(GetSubturnosAsyncQuery request, CancellationToken cancellationToken)
    {
        var objSubturno = await _repositoryAsync.ListAsync(cancellationToken);
        List<SubturnoType> lista = new();

        if (objSubturno is null)
        {
            return new ResponseType<List<SubturnoType>>() { Data = null, Succeeded = false, Message = "Registros no encontrados" };
        }

        foreach (var item in objSubturno)
        {
            lista.Add(new SubturnoType
            {
                Descripcion = item.Descripcion,
                Entrada = item.Entrada,
                Salida = item.Salida,
                MargenEntrada = item.MargenEntrada,
                MargenSalida = item.MargenSalida,
                TotalHoras = item.TotalHoras
            });
        }

        return new ResponseType<List<SubturnoType>>() { Data = lista, Succeeded = true, Message = "Registros encontrados" };
    }
}