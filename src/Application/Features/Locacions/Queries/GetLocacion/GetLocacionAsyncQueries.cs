using AutoMapper;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Locacions.Dto;
using EvaluacionCore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Queries.GetLocacion;

public record GetLocacionAsyncQueries(string IdLocacion) : IRequest<ResponseType<List<LocacionType>>>;

public class GetLocacionAsyncQueriesHandler : IRequestHandler<GetLocacionAsyncQueries, ResponseType<List<LocacionType>>>
{
    private readonly IRepositoryAsync<Locacion> _repositoryAsync;
    private readonly IMapper _mapper;

    public GetLocacionAsyncQueriesHandler(IRepositoryAsync<Locacion> repository, IMapper mapper)
    {
        _repositoryAsync = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<List<LocacionType>>> Handle(GetLocacionAsyncQueries request, CancellationToken cancellationToken)
    {

        var objLocacion = await _repositoryAsync.ListAsync(cancellationToken);

        if (objLocacion is null)
        {
            throw new NotFoundException($"Registro no encontrado {request.IdLocacion}");
        }

        if (request.IdLocacion is not null)
        {
            objLocacion =  objLocacion.Where(x => x.Id == Guid.Parse(request.IdLocacion) && x.Estado == "A").ToList();

        }
        else
        {
            objLocacion = objLocacion.Where(x => x.Estado == "A").ToList();
        }
        return new ResponseType<List<LocacionType>>() { Data = _mapper.Map<List<LocacionType>>(objLocacion), Succeeded = true };
        
        
    }
}
