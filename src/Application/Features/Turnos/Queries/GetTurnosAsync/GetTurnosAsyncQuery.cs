using AutoMapper;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Clients.Dto;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EnrolApp.Application.Features.Clients.Queries.GetTurnoById;

public record GetTurnosAsyncQuery(string Codigo) : IRequest<ResponseType<List<TurnoType>>>;

public class GetTurnosAsyncHandler : IRequestHandler<GetTurnosAsyncQuery, ResponseType<List<TurnoType>>>
{
    private readonly IRepositoryAsync<Turno> _repositoryAsync;
    private readonly IMapper _mapper;

    //private readonly ITurnoRepository _repository;


    public GetTurnosAsyncHandler(IRepositoryAsync<Turno> repository, IMapper mapper)
    {
        _repositoryAsync = repository;
        _mapper = mapper;
    }

   
    public async Task<ResponseType<List<TurnoType>>> Handle(GetTurnosAsyncQuery request, CancellationToken cancellationToken)
    {
        var objTurno = await _repositoryAsync.ListAsync(cancellationToken);

        if (objTurno is null)
        { 
            throw new NotFoundException($"Registro no encontrado {request.Codigo}");
        }
        return new ResponseType<List<TurnoType>>() { Data = _mapper.Map<List<TurnoType>>(objTurno), Succeeded = true };
    }
}