using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Commands.AsignarLocacionCliente;

public record AsignarLocacionClienteCommand(AsignarLocacionClienteRequest LocacionCliRequest) : IRequest<ResponseType<string>>;
public class AsignarLocacionClienteCommandHandler : IRequestHandler<AsignarLocacionClienteCommand, ResponseType<string>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryAsync<LocacionCliente> _repoLocacionCliAsync;

    public AsignarLocacionClienteCommandHandler(IMapper mapper, IRepositoryAsync<LocacionCliente> repoLocacionCliAsync)
    {
        _mapper = mapper;
        _repoLocacionCliAsync = repoLocacionCliAsync;

    }

    public async Task<ResponseType<string>> Handle(AsignarLocacionClienteCommand request, CancellationToken cancellationToken)
    {
        var objLocacionClie = _mapper.Map<LocacionCliente>(request.LocacionCliRequest);

        objLocacionClie.Id = Guid.NewGuid();
        objLocacionClie.Estado = "A";
        objLocacionClie.UsuarioCreacion = "Admin";


        var objResult = await _repoLocacionCliAsync.AddAsync(objLocacionClie, cancellationToken);
        if (objResult is null)
        {
            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Ocurrió un error al registrar el turno", StatusCode = "000", Succeeded = true };

        }

        return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Locación registrada exitosamente", StatusCode = "000", Succeeded = true };
    }
}
