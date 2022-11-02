using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Localidads.Commands.DeleteLocation;

public record DeleteLocalidadCommand(string IdLocalidad) : IRequest<ResponseType<string>>;

public class DeleteLocalidadCommandHandler : IRequestHandler<DeleteLocalidadCommand,ResponseType<string>>
{
    private readonly IRepositoryAsync<Localidad> _repoLocalidadAsync;
    private readonly IMapper _mapper;
    public DeleteLocalidadCommandHandler(IRepositoryAsync<Localidad> repoLocalidadAsync, IMapper mapper)
    {
        _repoLocalidadAsync = repoLocalidadAsync;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(DeleteLocalidadCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var objLocalidad = await _repoLocalidadAsync.GetByIdAsync(Guid.Parse(request.IdLocalidad), cancellationToken);
            if (objLocalidad is null)
            {
                return new ResponseType<string>() { Data = null, Message = "No existe la locación que desea eliminar", StatusCode = "301", Succeeded = false };
            }
            objLocalidad.Estado = "I";
            await _repoLocalidadAsync.UpdateAsync(objLocalidad, cancellationToken);

            return new ResponseType<string>() { Data = null, Message = "Locación eliminada exitosamente", StatusCode = "300", Succeeded = true };

        }
        catch (Exception ex)
        {
            return new ResponseType<string>() { Data = null, Message = "No se pudo eliminar la locación.", StatusCode = "301", Succeeded = false };
        }


        
    }
}
