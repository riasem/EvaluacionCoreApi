using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurno;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoSubTurno;

public record CreateTurnoSubTurnoCommand(CreateTurnoSubTurnoRequest TurnoRequest) : IRequest<ResponseType<string>>;


public class CreateTurnoSubTurnoCommandHandler : IRequestHandler<CreateTurnoSubTurnoCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<Turno> _repoTurnoAsync;
    private readonly IMapper _mapper;
    private readonly IRepositoryAsync<ClaseTurno> _repositoryClassAsync;
    private readonly IRepositoryAsync<SubclaseTurno> _repositorySubcAsync;
    private readonly IRepositoryAsync<TipoTurno> _repositoryTurnoAsync;

    public CreateTurnoSubTurnoCommandHandler(IRepositoryAsync<Turno> repository, IRepositoryAsync<ClaseTurno> repositoryClass, 
        IRepositoryAsync<SubclaseTurno> repositorySubt, IConfiguration config, IRepositoryAsync<TipoTurno> repositoryTurno,  IMapper mapper)
    {
        _repoTurnoAsync = repository; 
        _repositorySubcAsync = repositorySubt;
        _repositoryTurnoAsync = repositoryTurno;
        _repositoryClassAsync = repositoryClass;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(CreateTurnoSubTurnoCommand request, CancellationToken cancellationToken)
    {
        //VALIDAR CON CASOS REALES (WEB)
        try
        {
            var objClient = _mapper.Map<Turno>(request.TurnoRequest);
            var objDetClient = _mapper.Map<List<CreateTurnoRequest>>(request.TurnoRequest.Turnos);

            var objTurno = await _repoTurnoAsync.ListAsync(cancellationToken);

            #region Validaciones


            //Validacion de tipo turno
            var objTipoTurno = await _repositoryTurnoAsync.GetByIdAsync(objClient.IdTipoTurno, cancellationToken);
            if (objTipoTurno == null)
            {
                return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar, tipo de turno incorrecto.", StatusCode = "101", Succeeded = false };
            }

            //Validacion de clase turno
            var objClaseTurno = await _repositoryClassAsync.GetByIdAsync(objClient.IdClaseTurno, cancellationToken);
            if (objClaseTurno == null)
            {
                return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar, clase de turno incorrecta.", StatusCode = "101", Succeeded = false };
            }

            //Validacion de subclase turno
            var objSubclaseTurno = await _repositorySubcAsync.GetByIdAsync(objClient.IdSubclaseTurno, cancellationToken);
            if (objSubclaseTurno == null)
            {
                return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar, subclase de turno incorrecto.", StatusCode = "101", Succeeded = false };
            }

            var objConsult = await _repoTurnoAsync.FirstOrDefaultAsync(new TurnoByCodigoSpec(objClient.CodigoTurno), cancellationToken);

            //Caso donde el codigo del turno ya existe
            if (objConsult is not null)
            {
                return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar, turno ya existente", StatusCode = "101", Succeeded = false };
            }

            #endregion

            objClient.Id = Guid.NewGuid();
            objClient.Estado = "A";

            ////Caso donde no hay idTurnoPadre (se considera subturno)
            //if (!objClient.IdTurnoPadre.HasValue)
            //{
            //    objClient.IdTurnoPadre = null;
            //}

            //registro
            var objResult = await _repoTurnoAsync.AddAsync(objClient, cancellationToken);

            //Caso donde no se guarda correctamente el turno
            if (objResult is null)
            {
                return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar el turno", StatusCode = "101", Succeeded = false };
            }

            foreach (var item in objDetClient)
            {
                item.IdTurnoPadre = objResult.Id;
                _ = new CreateTurnoCommand(item);
            }

            return new ResponseType<string>() { Data = objResult.Id.ToString(), Message = "Turno registrado exitosamente", StatusCode = "100", Succeeded = true };
        }
        catch (Exception)
        {
            return new ResponseType<string>() { Data = null, Message = "Ocurrió un error al registrar el turno ", StatusCode = "102", Succeeded = false };
        }
        
    }
}
