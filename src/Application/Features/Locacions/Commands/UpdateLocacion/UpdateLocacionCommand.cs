using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Locacions.Commands.UpdateLocacion;

public record UpdateLocacionCommand(UpdateLocacionRequest UpdateLocacion) : IRequest<ResponseType<string>>;

public class UpdateLocacionCommandHandler : IRequestHandler<UpdateLocacionCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<Locacion> _repoLocacionAsync;
    private readonly IMapper _mapper;

    public UpdateLocacionCommandHandler(IRepositoryAsync<Locacion> repoLocacionAsync, IMapper mapper)
    {
        _repoLocacionAsync = repoLocacionAsync;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(UpdateLocacionCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            var objLocacion = _mapper.Map<Locacion>(request.UpdateLocacion);
            var entityLocacion = await _repoLocacionAsync.GetByIdAsync(objLocacion.Id,cancellationToken);
            if (entityLocacion is null)
            {
                return new ResponseType<string>() { Data = null, Message = "No existe la locación que intenta modificar", StatusCode = "999", Succeeded = false };
            }
            objLocacion.IdEmpresa = entityLocacion.IdEmpresa;
            objLocacion.FechaModificacion = DateTime.Now;
            objLocacion.UsuarioModificacion = "Admin";
            await _repoLocacionAsync.UpdateAsync(objLocacion, cancellationToken);
            return new ResponseType<string>() { Data = null, Message = "Locación modificada exitosamente", StatusCode = "000", Succeeded = true };

        }
        catch (Exception ex)
        {

            return new ResponseType<string>() { Data = null, Message = ex.Message, StatusCode = "999", Succeeded = false };
        }
    }
}
