using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.EvalCore.Commands.EvaluacionAsistencias;
using EvaluacionCore.Application.Features.Permisos.Dto;

namespace EvaluacionCore.Application.Features.EvalCore.Interfaces;

public interface IEvaluacion
{
    //InformacionGeneralEvaluacion GetInfoGeneralByIdentificacion(string Identificacion);

    Task<(string response, int success)> EvaluateAsistencias(string identificacion, DateTime? fechaDesde, DateTime? fechaHasta);
    Task<List<BitacoraMarcacionType>> ConsultaMarcaciones(string Identificacion, DateTime fechaDesde, DateTime fechaHasta, string codigoMarcacion);
    Task<List<ColaboradorConvivenciaType>> ConsultaColaboradores(string codUdn, string codArea, string codScosto, string suscriptor);

}
