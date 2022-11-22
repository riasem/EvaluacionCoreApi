using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.Localidads.Commands.AsignarLocalidadCliente;

public record AsignarLocalidadClienteCommand(AsignarLocalidadClienteRequest LocalidadCliRequest) : IRequest<ResponseType<string>>;
public class AsignarLocalidadClienteCommandHandler : IRequestHandler<AsignarLocalidadClienteCommand, ResponseType<string>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryAsync<LocalidadColaborador> _repoLocalidadCliAsync;

    public AsignarLocalidadClienteCommandHandler(IMapper mapper, IRepositoryAsync<LocalidadColaborador> repoLocalidadCliAsync)
    {
        _mapper = mapper;
        _repoLocalidadCliAsync = repoLocalidadCliAsync;

    }

    public async Task<ResponseType<string>> Handle(AsignarLocalidadClienteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var objLocalidadClie = _mapper.Map<LocalidadColaborador>(request.LocalidadCliRequest);

            objLocalidadClie.Id = Guid.NewGuid();
            objLocalidadClie.Estado = "A";
            objLocalidadClie.UsuarioCreacion = "Admin";


            var objResult = await _repoLocalidadCliAsync.AddAsync(objLocalidadClie, cancellationToken);
            if (objResult is null)
            {
                return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "001", Succeeded = false };

            }

            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Locación asignada exitosamente", StatusCode = "000", Succeeded = true };

        }
        catch (Exception)
        {
            return new ResponseType<string>() { Data = null, Message = "Ocurrió un error durante el registro", StatusCode = "002", Succeeded = false };
        }
        
    }
}
