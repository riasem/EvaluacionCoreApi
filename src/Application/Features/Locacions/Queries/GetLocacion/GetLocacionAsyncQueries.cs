using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Localidads.Dto;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.Localidads.Queries.GetLocalidad;

public record GetLocalidadAsyncQueries(string IdLocalidad) : IRequest<ResponseType<List<LocalidadType>>>;

public class GetLocalidadAsyncQueriesHandler : IRequestHandler<GetLocalidadAsyncQueries, ResponseType<List<LocalidadType>>>
{
    private readonly IRepositoryAsync<Localidad> _repositoryAsync;
    private readonly IMapper _mapper;

    public GetLocalidadAsyncQueriesHandler(IRepositoryAsync<Localidad> repository, IMapper mapper)
    {
        _repositoryAsync = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<List<LocalidadType>>> Handle(GetLocalidadAsyncQueries request, CancellationToken cancellationToken)
    {
        try
        {
            var objLocalidad = await _repositoryAsync.ListAsync(cancellationToken);

            if (request.IdLocalidad is not null)
            {
                objLocalidad = objLocalidad.Where(x => x.Id == Guid.Parse(request.IdLocalidad) && x.Estado == "A").ToList();

            }
            else
            {
                objLocalidad = objLocalidad.Where(x => x.Estado == "A").ToList();
            }
            return new ResponseType<List<LocalidadType>>() { Data = _mapper.Map<List<LocalidadType>>(objLocalidad), Message = "Consulta Generada exitosamente", StatusCode = "000", Succeeded = true };

        }
        catch (Exception e)
        {
            return new ResponseType<List<LocalidadType>>() { Data = null, Message = "Ocurrió un error durante la consulta", StatusCode = "002", Succeeded = false };
        }
        
        
    }
}
