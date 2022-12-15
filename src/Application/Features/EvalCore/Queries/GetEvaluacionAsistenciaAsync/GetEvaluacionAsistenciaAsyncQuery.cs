using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.EvalCore.Dto;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EvaluacionCore.Application.Features.EvalCore.Queries.GetEvaluacionAsistenciaAsync;

public record GetEvaluacionAsistenciaAsyncQuery(string Identificacion, DateTime FechaDesde, DateTime FechaHasta) : IRequest<ResponseType<List<EvaluacionAsistenciaResponseType>>>;

public class GetEvaluacionAsistenciaAsyncHandler : IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>
{
    private readonly IRepositoryAsync<Cliente> _repositoryClienteAsync;
    private readonly IRepositoryAsync<ClaseTurno> _repositoryClassAsync;
    private readonly IRepositoryAsync<SubclaseTurno> _repositorySubcAsync;
    private readonly IRepositoryAsync<TipoTurno> _repositoryTurnoAsync;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private string uriEnpoint = "";

    //private readonly ITurnoRepository _repository;


    public GetEvaluacionAsistenciaAsyncHandler(IRepositoryAsync<ClaseTurno> repository, IRepositoryAsync<SubclaseTurno> repositorySubt, IRepositoryAsync<Cliente> repositoryCli,
        IConfiguration config, IRepositoryAsync<TipoTurno> repositoryTurno, IMapper mapper)
    {
        _repositoryClienteAsync = repositoryCli;
        _repositorySubcAsync = repositorySubt;
        _repositoryTurnoAsync = repositoryTurno;
        _repositoryClassAsync = repository;
        _mapper = mapper;
        _config = config;
    }


    async Task<ResponseType<List<EvaluacionAsistenciaResponseType>>> IRequestHandler<GetEvaluacionAsistenciaAsyncQuery, ResponseType<List<EvaluacionAsistenciaResponseType>>>.Handle(GetEvaluacionAsistenciaAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //var objClaseTurno = await _repositoryClassAsync.ListAsync(cancellationToken);
            //var objSubclaseTurno = await _repositorySubcAsync.ListAsync(cancellationToken);
            //var objTipoTurno = await _repositoryTurnoAsync.ListAsync(cancellationToken);

            List<TipoJornadaType> listaTipoJornada = new();
            List<ModalidadJornadaType> listaModalidadJornada = new();

            var modalidadJornadaTypes = _config.GetSection("modalidadJornada").Get<List<ModalidadJornadaType>>();
            var tipoJornadaTypes = _config.GetSection("tipoJornada").Get<List<TipoJornadaType>>();

            List<EvaluacionAsistenciaResponseType> listaEvaluacionAsistencia = new();

            TurnoLaboral turnoLaboral = new()
            {
                Id = Guid.Parse("DED32B36-454F-4E8A-B186-578145C9D9ED"),
                Entrada = DateTime.Parse("2022/12/12 08:00"),
                Salida = DateTime.Parse("2022/12/12 17:00"),
                TotalHoras = "8"
            };

            TurnoReceso turnoReceso = new TurnoReceso()
            {
                Id = Guid.Parse("F4153037-3876-4821-85C2-433BA4C3D413"),
                Entrada = DateTime.Parse("2022/12/12 12:00"),
                Salida = DateTime.Parse("2022/12/12 14:00"),
                TotalHoras = "1"
            };

            Solicitud solicitud = new()
            {
                IdSolicitud = Guid.Parse("e44ef4e9-2a45-4bdd-9ade-654d4e73f756"),
                IdTipoSolicitud = Guid.Parse("de4d17bd-9f03-4ccb-a3c0-3f37629cea6a"),
                TipoSolicitud = "JUS"

            };

            listaEvaluacionAsistencia.Add(new EvaluacionAsistenciaResponseType()
            {
                Colaborador = "Pincay Kleber",
                Identificacion = "0951733286",
                CodBiometrico = "16056",
                Udn = "LAFATTORIA S.A.",
                Area = "ADMINISTRACION",
                SubCentroCosto = "SISTEMAS",
                Fecha = DateTime.Parse("2022/12/12"),
                Novedad = "ATRASO DE 20 MINUTOS",
                TurnoLaboral = turnoLaboral,
                TurnoReceso = turnoReceso,
                Solicitudes = new List<Solicitud>()
                {
                    new Solicitud() {
                    IdSolicitud = Guid.Parse("25BFCA79-A1B9-4EBD-8246-E92C08309047"),
                    IdTipoSolicitud = Guid.Parse("26a08ec8-40fe-435c-8655-3f570278879e"),
                    TipoSolicitud = "VAC"
                    },
                    new Solicitud {
                    IdSolicitud = Guid.Parse("4FEE69CB-3BB1-4809-BF6F-963E7535EE5B"),
                    IdTipoSolicitud = Guid.Parse("de4d17bd-9f03-4ccb-a3c0-3f37629cea6a"),
                    TipoSolicitud = "PER"
                    },
                    new Solicitud {
                    IdSolicitud = Guid.Parse("659F416C-F753-4000-EB45-08DADE2BEA25"),
                    IdTipoSolicitud = Guid.Parse("16d8e575-51a2-442d-889c-1e93e9f786b2"),
                    TipoSolicitud = "JUS"
                    }
                }
            });

            listaEvaluacionAsistencia.Add(new EvaluacionAsistenciaResponseType()
            {
                Colaborador = "Salas Jorge",
                Identificacion = "0951810993",
                CodBiometrico = "15848",
                Udn = "LAFATTORIA S.A.",
                Area = "ADMINISTRACION",
                SubCentroCosto = "SISTEMAS",
                Fecha = DateTime.Parse("2022/12/12"),
                Novedad = "ATRASO DE 20 MINUTOS",
                TurnoLaboral = turnoLaboral,
                TurnoReceso = turnoReceso,
                Solicitudes = new List<Solicitud>()
                {
                    new Solicitud {
                    IdSolicitud = Guid.Parse("4FEE69CB-3BB1-4809-BF6F-963E7535EE5B"),
                    IdTipoSolicitud = Guid.Parse("de4d17bd-9f03-4ccb-a3c0-3f37629cea6a"),
                    TipoSolicitud = "PER"
                    },
                    new Solicitud {
                    IdSolicitud = Guid.Parse("659F416C-F753-4000-EB45-08DADE2BEA25"),
                    IdTipoSolicitud = Guid.Parse("16d8e575-51a2-442d-889c-1e93e9f786b2"),
                    TipoSolicitud = "JUS"
                    }
                }
            });

            listaEvaluacionAsistencia.Add(new EvaluacionAsistenciaResponseType()
            {
                Colaborador = "Valdiviezo Angel",
                Identificacion = "0922219480",
                CodBiometrico = "16015",
                Udn = "LAFATTORIA S.A.",
                Area = "ADMINISTRACION",
                SubCentroCosto = "SISTEMAS",
                Fecha = DateTime.Parse("2022/12/12"),
                Novedad = "ATRASO DE 20 MINUTOS",
                TurnoLaboral = turnoLaboral,
                TurnoReceso = turnoReceso,
                Solicitudes = new List<Solicitud>() { solicitud }
            });

            listaEvaluacionAsistencia.Add(new EvaluacionAsistenciaResponseType()
            {
                Colaborador = "Borbor Douglas",
                Identificacion = "0951635390",
                CodBiometrico = "16042",
                Udn = "LAFATTORIA S.A.",
                Area = "ADMINISTRACION",
                SubCentroCosto = "SISTEMAS",
                Fecha = DateTime.Parse("2022/12/12"),
                Novedad = "ATRASO DE 20 MINUTOS",
                TurnoLaboral = turnoLaboral,
                TurnoReceso = turnoReceso,
                Solicitudes = new List<Solicitud>() { solicitud }
            });

            return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = listaEvaluacionAsistencia, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        }
        catch (Exception e)
        {
            return new ResponseType<List<EvaluacionAsistenciaResponseType>>() { Data = null, Succeeded = false, StatusCode = "002", Message = "Ocurrió un error durante la consulta" };
            //insertar logs
        }

    }
}