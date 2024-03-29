﻿using EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesExcel;
using EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesTxt;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionApp;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionOffline;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionWeb;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Queries.GetHorasExtrasColaborador;

namespace EvaluacionCore.Application.Features.Marcacion.Interfaces;

public interface IMarcacion
{
    Task<ResponseType<MarcacionResponseType>> CreateMarcacion(CreateMarcacionRequest Request, CancellationToken cancellationToken);

    Task<ResponseType<CreateMarcacionResponseType>> CreateMarcacionApp(CreateMarcacionAppRequest Request,string IdentificacionSesion, CancellationToken cancellationToken);

    Task<ResponseType<CreateMarcacionResponseType>> CreateMarcacionAppLast(CreateMarcacionAppLastRequest Request, string IdentificacionSesion, CancellationToken cancellationToken);

    Task<ResponseType<List<ConsultaRecursoType>>> ConsultarRecursos(Guid IdCliente, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken);

    Task<ResponseType<MarcacionWebResponseType>> CreateMarcacionWeb(CreateMarcacionWebRequest Request, CancellationToken cancellationToken);
    
    Task<ResponseType<List<NovedadMarcacionType>>> ConsultaNovedadMarcacion(string Identificacion, string FiltroNovedades, DateTime FDesde, DateTime FHasta, CancellationToken cancellationToken);

    Task<ResponseType<List<NovedadMarcacionWebType>>> ConsultaAsistencia(string Identificacion, string FiltroNovedades, DateTime FDesde, DateTime FHasta, CancellationToken cancellationToken);

    Task<ResponseType<List<NovedadMarcacionWebType>>> ConsultaNovedadMarcacionWeb(string Identificacion, string FiltroNovedades, DateTime FDesde, DateTime FHasta, CancellationToken cancellationToken);

    Task<ResponseType<List<ColaboradorByLocalidadResponseType>>> ListadoColaboradorByLocalidad(string IdentificacionSesion, CancellationToken cancellationToken);

    Task<ResponseType<string>> CreateMarcacionOffline (CreateMarcacionOfflineRequest Request,string IdentificacionSesion,string TipoCarga, CancellationToken cancellationToken);

    Task<ResponseType<List<NovedadesMarcacionOfflineResponse>>> NovedadesMaracionOffline(string CodUdn, string Identificacion, DateTime? FechaDesde, DateTime? FechasHasta,int? DeviceId ,string IdentificacionSesion, CancellationToken cancellationToken);

    Task<ResponseType<List<DispositivosMarcacionResponse>>> GetDispositivoMarcacion();

    Task<ResponseType<string>> CargarMarcacionesExcel(List<CargaMarcacionesExcelRequest> request, string IdentificacionSesion,CancellationToken cancellationToken);

    Task<ResponseType<string>> CargarMarcacionesTxt(List<CargaMarcacionesTxtRequest> request, string IdentificacionSesion, CancellationToken cancellationToken);

    Task<ResponseType<List<HorasExtrasColaboradorResponse>>> GetConsultaHorasExtrasColaborador(string Opcion, string IdentificacionSesion, DateTime fechaDesde, DateTime fechaHasta, List<Identificacion> Identificaciones, CancellationToken cancellationToken);

 }
