using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador;

public record CreateTurnoColaboradorCommand(CreateTurnoColaboradorRequest TurnoRequest) : IRequest<ResponseType<string>>;


public class CreateTurnoColaboradorCommandHandler : IRequestHandler<CreateTurnoColaboradorCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoAsync;
    private readonly IMapper _mapper;

    public CreateTurnoColaboradorCommandHandler(IRepositoryAsync<TurnoColaborador> repository, IMapper mapper)
    {
        _repoTurnoAsync = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(CreateTurnoColaboradorCommand request, CancellationToken cancellationToken)
    {
        DateTime fechaAsigna;

        try
        {
            DateTime fechaDesde = request.TurnoRequest.FechaAsignacionDesde;
            DateTime fechaHasta = request.TurnoRequest.FechaAsignacionHasta;
            TimeSpan difFechas = fechaHasta - fechaDesde;

            var consulta = await _repoTurnoAsync.ListAsync(cancellationToken);
            var filtro = consulta.Where(e => e.FechaAsignacion > fechaDesde && e.FechaAsignacion < fechaHasta).ToList();

            if (filtro.Count > 0)
            {
                return new ResponseType<string>() { Data = null, Message = "Ya existen turnos asignados en el rango de las fechas indicadas", StatusCode = "101", Succeeded = false };
            }

            for (int i = 0; i <= difFechas.Days; i++ )
            {
                foreach (var item in request.TurnoRequest.ClienteSubturnos)
                {
                    Guid guid = Guid.NewGuid();
                    TurnoColaborador objClient = new()
                    {
                        Id = guid,
                        Estado = "A",
                        UsuarioCreacion = "SYSTEM",
                        IdTurno = request.TurnoRequest.IdTurno,
                        IdColaborador = item.IdCliente,
                        FechaAsignacion = fechaDesde.AddDays(i)
                    };

                    var objResult = await _repoTurnoAsync.AddAsync(objClient, cancellationToken);

                    if (objResult is null)
                    {
                        return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación del turno Laboral", StatusCode = "101", Succeeded = false };
                    }

                    if (item.Subturnos.Count > 0)
                    {

                        foreach (var item2 in item.Subturnos)
                        {
                            Guid guid2 = Guid.NewGuid();
                            TurnoColaborador objClient2 = new()
                            {
                                Id = guid2,
                                Estado = "A",
                                UsuarioCreacion = "SYSTEM",
                                IdTurno = item2.IdSubturno,
                                IdColaborador = item.IdCliente,
                                FechaAsignacion = fechaDesde.AddDays(i)
                            };

                            var objResult2 = await _repoTurnoAsync.AddAsync(objClient2, cancellationToken);

                            if (objResult2 is null)
                            {
                                return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación del (los) turno(s) de receso", StatusCode = "101", Succeeded = false };
                            }
                        }
                    }
                }
            }

            
            return new ResponseType<string>() { Data = /*objResult.Id.ToString()*/ null,Message = "Turnos asignados correctamente", StatusCode ="100",Succeeded = true };
        }
        catch (Exception ex)
        {
            return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "102", Succeeded = false };
        }
        
       
    }
}
