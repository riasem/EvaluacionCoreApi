using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Application.Features.Turnos.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.Turnos.Queries.GetTurnosColaborador;

public record GetTurnosColaboradorAsyncQuery(string Identificacion, DateTime FechaDesde, DateTime FechaHasta) : IRequest<ResponseType<List<TurnoColaboradorType>>>;

public class GetTurnosColaboradorAsyncHandler : IRequestHandler<GetTurnosColaboradorAsyncQuery, ResponseType<List<TurnoColaboradorType>>>
{
    private readonly IConfiguration _config;
    private readonly IRepositoryAsync<TurnoColaborador> _repositoryTurnoColAsync;
    private readonly IRepositoryAsync<Cliente> _repositoryClienteAsync;
    private readonly IRepositoryAsync<Turno> _repositoryTurnoAsync;
    private readonly IMapper _mapper;

    //private readonly ITurnoRepository _repository;


    public GetTurnosColaboradorAsyncHandler(IRepositoryAsync<TurnoColaborador> repository, IConfiguration config,
        IRepositoryAsync<Cliente> repositoryCliente, IRepositoryAsync<Turno> repositoryTurno, IMapper mapper)
    {
        _config = config;
        _repositoryClienteAsync = repositoryCliente;
        _repositoryTurnoAsync = repositoryTurno;
        _repositoryTurnoColAsync = repository;
        _mapper = mapper;
    }


    public async Task<ResponseType<List<TurnoColaboradorType>>> Handle(GetTurnosColaboradorAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objColaborador = await _repositoryClienteAsync.ListAsync(cancellationToken);
            var objTurno = await _repositoryTurnoAsync.ListAsync(cancellationToken);

            var idColaborador = objColaborador.Where(e => e.Identificacion == request.Identificacion).FirstOrDefault();

            var objTurnoColaborador = await _repositoryTurnoColAsync.ListAsync(new TurnoColaboradorByFilterSpec(idColaborador.Id, request.FechaDesde, request.FechaHasta), cancellationToken);
            //var objTurnoColaborador = objTurnoColaboradorBase.Where(e => /*e.Colaborador.Identificacion == request.Identificacion 
            //                                                        &&*/ e.FechaAsignacion > request.FechaDesde 
            //                                                        && e.FechaAsignacion < request.FechaHasta).ToList();


            List<TurnoColaboradorType> turnoColaboradorType = new();

            foreach (var item in objTurnoColaborador)
            {
                var turno = objTurno.Where(x => x.Id == item.IdTurno).FirstOrDefault();
                var colaborador = objColaborador.Where(x => x.Id == item.IdColaborador).FirstOrDefault();


                turnoColaboradorType.Add(new TurnoColaboradorType
                {
                    Id = item.Id,
                    IdTurno = item.IdTurno,
                    HoraEntrada = turno.Entrada,
                    HoraSalida = turno.Salida,
                    CodigoTurno = turno.CodigoTurno,
                    CodigoIntegracion = turno.CodigoIntegracion,
                    IdColaborador = item.IdColaborador,
                    FechaAsignacion = item.FechaAsignacion,
                    IdTurnoPadre = turno.IdTurnoPadre,
                    ApellidosColaborador = colaborador.Apellidos,
                    NombresColaborador = colaborador.Nombres,
                    Identificacion = colaborador.Identificacion
                });

            }


            return new ResponseType<List<TurnoColaboradorType>>() { Data = turnoColaboradorType, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        }
        catch (Exception e)
        {
            return new ResponseType<List<TurnoColaboradorType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }
        
    }

}