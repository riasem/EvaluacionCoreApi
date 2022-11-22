using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.Localidads.Commands.CreateLocalidad;

public record CreateLocalidadCommand(CreateLocalidadRequest LocalidadRequest) : IRequest<ResponseType<string>>;
public class CreateLocalidadCommandHandler : IRequestHandler<CreateLocalidadCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<Localidad> _repoLocalidadAsync;
    private readonly IMapper _mapper;

    public CreateLocalidadCommandHandler(IRepositoryAsync<Localidad> repoLocalidadAsync, IMapper mapper)
    {
        _repoLocalidadAsync = repoLocalidadAsync;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(CreateLocalidadCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var objLocalidad = _mapper.Map<Localidad>(request.LocalidadRequest);

            objLocalidad.Id = Guid.NewGuid();
            objLocalidad.Estado = "A";
            objLocalidad.UsuarioCreacion = "Admin";


            var objResult = await _repoLocalidadAsync.AddAsync(objLocalidad, cancellationToken);
            if (objResult is null)
            {
                return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la localidad", StatusCode = "001", Succeeded = false };

            }

            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Locación registrada exitosamente", StatusCode = "000", Succeeded = true };
        }
        catch (Exception)
        {
            return new ResponseType<string>() { Data = null, Message = "Ocurrió un error durante el registro de la locación.", StatusCode = "002", Succeeded = false };
        }
        
    }

}
