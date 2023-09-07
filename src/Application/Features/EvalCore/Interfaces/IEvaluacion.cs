using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.EvalCore.Commands.EvaluacionAsistencias;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Permisos.Dto;
using EvaluacionCore.Domain.Entities.ControlAsistencia;

namespace EvaluacionCore.Application.Features.EvalCore.Interfaces;

public interface IEvaluacion
{
    //InformacionGeneralEvaluacion GetInfoGeneralByIdentificacion(string Identificacion);

    Task<(string response, int success)> EvaluateAsistencias(string identificacion, DateTime? fechaDesde, DateTime? fechaHasta);
    Task<List<BitacoraMarcacionType>> ConsultaMarcaciones(string Identificacion, DateTime fechaDesde, DateTime fechaHasta, string codigoMarcacion);

    Task<List<ControlAsistenciaType>> ConsultaAsistencia(string CodigoBiometrico, DateTime fechaDesde, DateTime fechaHasta);

    Task<List<ColaboradorConvivenciaType>> ConsultaColaboradores(string codUdn, string codArea, string codScosto, string suscriptor);
    Task<List<ColaboradorConvivenciaType>> ConsultaColaborador(string suscriptor);

    Task<List<ControlAsistenciaCab>> ConsultaControlAsistenciaCab(string codUdn, string codArea, string codScosto, string periodo, string suscriptor);
    Task<List<ControlAsistenciaDet>> ConsultaControlAsistenciaDet(int idControlAsistenciaCab);
    Task<List<ControlAsistenciaNovedad>> ConsultaControlAsistenciaNovedad(int idControlAsistenciaDet);

}
