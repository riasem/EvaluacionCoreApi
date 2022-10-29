using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
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
        var objLocalidad = _mapper.Map<Localidad>(request.LocalidadRequest);

        objLocalidad.Id = Guid.NewGuid();
        objLocalidad.Estado = "A";
        objLocalidad.UsuarioCreacion = "Admin";


        var objResult = await _repoLocalidadAsync.AddAsync(objLocalidad, cancellationToken);
        if (objResult is null)
        {
            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Ocurrió un error al registrar el turno", StatusCode = "000", Succeeded = true };

        }

        return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Locación registrada exitosamente", StatusCode = "000", Succeeded = true };
    }

}
