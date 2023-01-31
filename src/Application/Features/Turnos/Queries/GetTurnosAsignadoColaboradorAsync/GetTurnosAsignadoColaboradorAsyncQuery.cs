using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.Turnos.Queries.GetTurnosAsignadoColaboradorAsync;

public record GetTurnosAsignadoColaboradorAsyncQuery(string Identificacion, DateTime FechaDesde, DateTime FechaHasta) : IRequest<ResponseType<List<TurnoColaboradorType>>>;

public class GetTurnosAsignadoColaboradorAsyncQueryHandler : IRequestHandler<GetTurnosAsignadoColaboradorAsyncQuery, ResponseType<List<TurnoColaboradorType>>>
{

    private readonly IConfiguration _config;
    private readonly IRepositoryAsync<TurnoColaborador> _repositoryTurnoColAsync;
    private readonly IRepositoryAsync<Cliente> _repositoryClienteAsync;
    private readonly IRepositoryAsync<Turno> _repositoryTurnoAsync;
    private readonly IMapper _mapper;

    public GetTurnosAsignadoColaboradorAsyncQueryHandler(IConfiguration config, IRepositoryAsync<TurnoColaborador> repositoryTurnoColAsync, IRepositoryAsync<Cliente> repositoryClienteAsync, IRepositoryAsync<Turno> repositoryTurnoAsync, IMapper mapper)
    {
        _config = config;
        _repositoryTurnoColAsync = repositoryTurnoColAsync;
        _repositoryClienteAsync = repositoryClienteAsync;
        _repositoryTurnoAsync = repositoryTurnoAsync;
        _mapper = mapper;
    }

    public async Task<ResponseType<List<TurnoColaboradorType>>> Handle(GetTurnosAsignadoColaboradorAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objColaborador = await _repositoryClienteAsync.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(request.Identificacion),cancellationToken);


            var objTurnoColaborador = await _repositoryTurnoColAsync.ListAsync(new TurnoByFechaColaboradorSpec(request.FechaDesde, request.FechaHasta, objColaborador.Id), cancellationToken);

            if (!objTurnoColaborador.Any())
            {
                return new ResponseType<List<TurnoColaboradorType>>() { Succeeded = true, StatusCode = "001", Message = "No tiene turno asignado para la fecha solicitada, solicita te asignen un turno." };
            }

            List<TurnoColaboradorType> turnoColaboradorType = new();

            foreach (var item in objTurnoColaborador)
            {

                turnoColaboradorType.Add(new TurnoColaboradorType
                {
                    Id = item.Id,
                    IdTurno = item.IdTurno,
                    HoraEntrada = item.Turno.Entrada,
                    HoraSalida = item.Turno.Salida,
                    CodigoTurno = item.Turno.CodigoTurno,
                    CodigoIntegracion = item.Turno.CodigoIntegracion,
                    IdColaborador = item.IdColaborador,
                    FechaAsignacion = item.FechaAsignacion,
                    IdTurnoPadre = item.Turno.IdTurnoPadre,
                    ApellidosColaborador = objColaborador.Apellidos,
                    NombresColaborador = objColaborador.Nombres,
                    Identificacion = objColaborador.Identificacion,
                    Descripcion = item.Turno.Descripcion,
                    Mensaje = item.Turno.Descripcion + " " + item.Turno.Entrada.ToString("HH:mm:ss") + " - " + item.Turno.Salida.ToString("HH:mm:ss"),
                    MargenEntradaGracia = item.Turno.MargenEntradaGracia,
                    MargenEntradaPrevio = item.Turno.MargenEntradaPrevio,
                    MargenSalidaGracia = item.Turno.MargenSalidaGracia,
                    MargenSalidaPosterior = item.Turno.MargenSalidaPosterior,
                    TiempoMinLaboExtra = item.Turno.TiempoMinLaboExtra,
                    TiempoMaxLaboExtra = item.Turno.TiempoMaxLaboExtra
                });

            }


            return new ResponseType<List<TurnoColaboradorType>>() { Data = turnoColaboradorType, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        }
        catch (Exception )
        {
            return new ResponseType<List<TurnoColaboradorType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }
    }
}
