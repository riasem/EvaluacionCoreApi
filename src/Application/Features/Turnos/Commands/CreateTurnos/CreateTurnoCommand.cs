using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurno;

public record CreateTurnoCommand(CreateTurnoRequest TurnoRequest) : IRequest<ResponseType<string>>;


public class CreateTurnoCommandHandler : IRequestHandler<CreateTurnoCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<Turno> _repoTurnoAsync;
    private readonly IRepositoryAsync<SubTurno> _repoSubturnoAsync;
    private readonly IMapper _mapper;

    public CreateTurnoCommandHandler(IRepositoryAsync<Turno> repository, IRepositoryAsync<SubTurno> repoSubAsync,
        IConfiguration config, IMapper mapper)
    {
        _repoTurnoAsync = repository;
        _repoSubturnoAsync = repoSubAsync;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(CreateTurnoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            SubTurno subTurno = new();
            var objClient = _mapper.Map<Turno>(request.TurnoRequest);
            var objConsult = await _repoTurnoAsync.FirstOrDefaultAsync(new TurnoByCodigoSpec(objClient.CodigoTurno), cancellationToken);
            objClient.Id = Guid.NewGuid();
            objClient.Estado = "A";

            //Caso donde el codigo del turno ya existe
            if (objConsult is not null)
            {
                return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar, turno ya existente", StatusCode = "101", Succeeded = true };
            }
            var objResult = await _repoTurnoAsync.AddAsync(objClient, cancellationToken);

            //Caso donde no se guarda correctamente el turno
            if (objResult is null)
            {
                return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar el turno", StatusCode = "102", Succeeded = false };

            }

            //insertar metodo de crear subturno 
            subTurno.CodigoSubturno = objResult.CodigoTurno;
            subTurno.EsSubturnoPrincipal = true;
            subTurno.Entrada = objResult.Entrada;
            subTurno.Salida = objResult.Salida;
            subTurno.MargenEntrada = objResult.MargenEntrada;
            subTurno.MargenSalida = objResult.MargenSalida;
            subTurno.Descripcion = objResult.Descripcion;
            subTurno.IdTurno = objResult.Id;
            subTurno.IdTipoSubturno = Guid.Parse("AAA1E77A-45FF-4E74-A7CC-0AC4B3AE3354");
            subTurno.UsuarioCreacion = "Admin";

            var objInsertSub = await _repoSubturnoAsync.AddAsync(subTurno, cancellationToken);


            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Turno registrado exitosamente", StatusCode = "100", Succeeded = true };
        }
        catch (Exception e)
        {
            return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar el turno ", StatusCode = "102", Succeeded = false };
        }
        
    }
}
