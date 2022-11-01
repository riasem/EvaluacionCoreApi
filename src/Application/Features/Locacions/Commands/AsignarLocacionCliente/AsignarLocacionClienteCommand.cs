﻿using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;

namespace EvaluacionCore.Application.Features.Localidads.Commands.AsignarLocalidadCliente;

public record AsignarLocalidadClienteCommand(AsignarLocalidadClienteRequest LocalidadCliRequest) : IRequest<ResponseType<string>>;
public class AsignarLocalidadClienteCommandHandler : IRequestHandler<AsignarLocalidadClienteCommand, ResponseType<string>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryAsync<LocalidadCliente> _repoLocalidadCliAsync;

    public AsignarLocalidadClienteCommandHandler(IMapper mapper, IRepositoryAsync<LocalidadCliente> repoLocalidadCliAsync)
    {
        _mapper = mapper;
        _repoLocalidadCliAsync = repoLocalidadCliAsync;

    }

    public async Task<ResponseType<string>> Handle(AsignarLocalidadClienteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var objLocalidadClie = _mapper.Map<LocalidadCliente>(request.LocalidadCliRequest);

            objLocalidadClie.Id = Guid.NewGuid();
            objLocalidadClie.Estado = "A";
            objLocalidadClie.UsuarioCreacion = "Admin";


            var objResult = await _repoLocalidadCliAsync.AddAsync(objLocalidadClie, cancellationToken);
            if (objResult is null)
            {
                return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Ocurrió un error al registrar el turno", StatusCode = "001", Succeeded = true };

            }

            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Locación guardada exitosamente", StatusCode = "000", Succeeded = true };

        }
        catch (Exception)
        {

            throw;
        }
        
    }
}
