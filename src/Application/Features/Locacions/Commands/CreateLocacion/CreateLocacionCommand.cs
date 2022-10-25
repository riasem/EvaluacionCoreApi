using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Locacions.Commands.CreateLocacion;

public record CreateLocacionCommand(CreateLocacionRequest LocacionRequest) : IRequest<ResponseType<string>>;
public class CreateLocacionCommandHandler : IRequestHandler<CreateLocacionCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<Locacion> _repoLocacionAsync;
    private readonly IMapper _mapper;

    public CreateLocacionCommandHandler(IRepositoryAsync<Locacion> repoLocacionAsync, IMapper mapper)
    {
        _repoLocacionAsync = repoLocacionAsync;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(CreateLocacionCommand request, CancellationToken cancellationToken)
    {
        var objLocacion = _mapper.Map<Locacion>(request.LocacionRequest);

        objLocacion.Id = Guid.NewGuid();
        objLocacion.Estado = "A";
        objLocacion.UsuarioCreacion = "Admin";


        var objResult = await _repoLocacionAsync.AddAsync(objLocacion, cancellationToken);
        if (objResult is null)
        {
            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Ocurrió un error al registrar el turno", StatusCode = "000", Succeeded = true };

        }

        return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Locación registrada exitosamente", StatusCode = "000", Succeeded = true };
    }

}
