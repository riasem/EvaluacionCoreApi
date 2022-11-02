using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Localidads.Commands.UpdateLocalidad;

public record UpdateLocalidadCommand(UpdateLocalidadRequest UpdateLocalidad) : IRequest<ResponseType<string>>;

public class UpdateLocalidadCommandHandler : IRequestHandler<UpdateLocalidadCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<Localidad> _repoLocalidadAsync;
    private readonly IMapper _mapper;

    public UpdateLocalidadCommandHandler(IRepositoryAsync<Localidad> repoLocalidadAsync, IMapper mapper)
    {
        _repoLocalidadAsync = repoLocalidadAsync;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(UpdateLocalidadCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            var objLocalidad = _mapper.Map<Localidad>(request.UpdateLocalidad);
            var entityLocalidad = await _repoLocalidadAsync.GetByIdAsync(objLocalidad.Id,cancellationToken);
            if (entityLocalidad is null)
            {
                return new ResponseType<string>() { Data = null, Message = "No existe la locación que intenta modificar", StatusCode = "201", Succeeded = false };
            }
            objLocalidad.IdEmpresa = entityLocalidad.IdEmpresa;
            objLocalidad.FechaModificacion = DateTime.Now;
            objLocalidad.UsuarioModificacion = "Admin";
            await _repoLocalidadAsync.UpdateAsync(objLocalidad, cancellationToken);
            return new ResponseType<string>() { Data = null, Message = "Locación actualizada exitosamente", StatusCode = "200", Succeeded = true };

        }
        catch (Exception ex)
        {
            return new ResponseType<string>() { Data = null, Message = "No se pudo actualizar la locación", StatusCode = "201", Succeeded = false };
        }
    }
}
