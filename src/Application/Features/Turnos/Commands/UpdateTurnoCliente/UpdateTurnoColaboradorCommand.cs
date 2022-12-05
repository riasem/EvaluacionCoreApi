using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Commands.UpdateTurnoColaborador;

public record UpdateTurnoColaboradorCommand(UpdateTurnoColaboradorRequest TurnoRequest) : IRequest<ResponseType<string>>;


public class UpdateTurnoColaboradorCommandHandler : IRequestHandler<UpdateTurnoColaboradorCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<TurnoColaborador> _repoTurnoAsync;
    private readonly IMapper _mapper;

    public UpdateTurnoColaboradorCommandHandler(IRepositoryAsync<TurnoColaborador> repository, IMapper mapper)
    {
        _repoTurnoAsync = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(UpdateTurnoColaboradorCommand request, CancellationToken cancellationToken)
    {

        try
        {
            DateTime fechaDesde = request.TurnoRequest.FechaAsignacionDesde;
            DateTime fechaHasta = request.TurnoRequest.FechaAsignacionHasta;
            TimeSpan difFechas = fechaHasta - fechaDesde;


            for (int i = 0; i <= difFechas.Days; i++ )
            {
                foreach (var item in request.TurnoRequest.ClienteSubturnos)
                {

                    //var filtro = consulta.Where(e => e.FechaAsignacion == fechaDesde.AddDays(i) && e.IdColaborador == item.IdCliente).ToList();

                    //if (filtro.Count > 0)
                    //{
                    //    return new ResponseType<string>() { Data = null, Message = "Ya existen turnos asignados en el rango de las fechas indicadas", StatusCode = "101", Succeeded = false };
                    //}
                    Guid guid = Guid.NewGuid();
                    TurnoColaborador objClient = new()
                    {
                        Id = request.TurnoRequest.IdTurnoColaborador,
                        Estado = "A",
                        UsuarioCreacion = "SYSTEM",
                        FechaModificacion = DateTime.UtcNow,
                        IdTurno = request.TurnoRequest.IdTurno,
                        IdColaborador = item.IdCliente,
                        FechaAsignacion = fechaDesde.AddDays(i)
                    };

                    await _repoTurnoAsync.UpdateAsync(objClient, cancellationToken);


                    if (item.Subturnos.Count > 0)
                    {

                        foreach (var item2 in item.Subturnos)
                        {
                            //Guid guid2 = Guid.NewGuid();
                            TurnoColaborador objClient2 = new()
                            {

                                Id = item2.IdSubTurnoColaborador,
                                Estado = "A",
                                UsuarioCreacion = "SYSTEM",
                                FechaModificacion = DateTime.UtcNow,
                                IdTurno = item2.IdSubturno,
                                IdColaborador = item.IdCliente,
                                FechaAsignacion = fechaDesde.AddDays(i)
                            };

                            await _repoTurnoAsync.UpdateAsync(objClient2, cancellationToken);

                        }
                    }
                }
            }

            
            return new ResponseType<string>() { Data = /*objResult.Id.ToString()*/ null,Message = "Turnos actualizados correctamente", StatusCode ="100",Succeeded = true };
        }
        catch (Exception ex)
        {
            return new ResponseType<string>() { Data = null, Message = "No se pudo registrar la asignación", StatusCode = "102", Succeeded = false };
        }
        
       
    }
}
