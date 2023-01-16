using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Calendario.Dto;
using EvaluacionCore.Application.Features.Calendario.Interfaces;
using EvaluacionCore.Application.Features.Calendario.Specifications;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Application.Features.Locacions.Specifications;
using EvaluacionCore.Domain.Entities.Asistencia;
using EvaluacionCore.Domain.Entities.Calendario;
using EvaluacionCore.Domain.Entities.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Persistence.Repository.Calendario;

public class CalendarioServices : ICalendario
{
    private readonly ILogger<DiasFeriadoIdentificacionResponseType> _log;
    private readonly IRepositoryAsync<LocalidadColaborador> _repoLocalidadColaborador;
    private readonly IRepositoryAsync<CalendarioNacional> _repoCalendarioNa;
    private readonly IRepositoryAsync<CalendarioLocal> _repoCalendarioLocal;
    private readonly IRepositoryAsync<Canton> _repoCanton;

    public CalendarioServices(ILogger<DiasFeriadoIdentificacionResponseType> log, IRepositoryAsync<LocalidadColaborador> repoLocalidadColaborador, 
                               IRepositoryAsync<CalendarioNacional> repoCalendarioNa,IRepositoryAsync<CalendarioLocal> repoCalendarioLocal,
                               IRepositoryAsync<Canton> repoCanton)
    { 
        _log = log;
        _repoLocalidadColaborador = repoLocalidadColaborador;
        _repoCalendarioNa = repoCalendarioNa;
        _repoCalendarioLocal = repoCalendarioLocal;
        _repoCanton = repoCanton;
    }

    public async Task<ResponseType<List<DiasFeriadoIdentificacionResponseType>>> GetDiasFeriadosByIdentificacion(string identificacion, DateTime fecha)
    {
        try
        {
            var objClienteLocalidad = await _repoLocalidadColaborador.ListAsync(new GetLocationByColaboradorSpec(identificacion));
            if (!objClienteLocalidad.Any()) return new ResponseType<List<DiasFeriadoIdentificacionResponseType>>() { StatusCode = "001", Succeeded = true, Message = "No tiene asignado una localidad" };
            var canton = objClienteLocalidad.Where(x => x.EsPrincipal == true).FirstOrDefault()/*DistinctBy(x => x.Localidad.Canton.Codigo).ToList()*/;
            var pais = objClienteLocalidad.Where(x => x.EsPrincipal == true).FirstOrDefault()/*.DistinctBy(x => x.Localidad.Canton.Provincia.Pais.Codigo).ToList()*/;
            List<DiasFeriadoIdentificacionResponseType> result = new();

            //foreach (var pais in listPaises)
            //{
                var CalendarioNacional = await _repoCalendarioNa.FirstOrDefaultAsync(new GetFeriadosNacionalByIdentificacionSpec(pais.Localidad.Canton.Provincia.Pais.Id, fecha));
                if (CalendarioNacional != null)
                {
                    result.Add(new DiasFeriadoIdentificacionResponseType
                    {
                        //Identificacion = identificacion,
                        //Nombres = pais.Colaborador.Nombres,
                        //Apellidos = pais.Colaborador.Apellidos,
                        Fecha = fecha,
                        Localidad = pais.Localidad.Descripcion,
                        Canton = pais.Localidad.Canton.Descripcion,
                        Provincia = pais.Localidad.Canton.Provincia.Descripcion,
                        Pais = pais.Localidad.Canton.Provincia.Pais.Descripcion,
                        FechaConmemorativa = CalendarioNacional.FechaConmemorativa,
                        EsRecuperable = CalendarioNacional.EsRecuperable,
                        FechaFestiva = CalendarioNacional.FechaFestiva,
                        TipoFeriado = "Nacional",
                        Descripcion = CalendarioNacional.Descripcion
                    });;;
                }
            //}
            //foreach (var canton in listCantones)
            //{
                var CalendarioLocal = await _repoCalendarioLocal.FirstOrDefaultAsync(new GetFeriadosLocalesByIdentificacionSpec(canton.Localidad.Canton.Id, fecha));
                if (CalendarioLocal != null)
                {
                    result.Add(new DiasFeriadoIdentificacionResponseType
                    {
                        //Identificacion = identificacion,
                        //Nombres = canton.Colaborador.Nombres,
                        //Apellidos = canton.Colaborador.Apellidos,
                        Fecha = fecha,
                        Localidad = canton.Localidad.Descripcion,
                        Canton = canton.Localidad.Canton.Descripcion,
                        Provincia = canton.Localidad.Canton.Provincia.Descripcion,
                        Pais = canton.Localidad.Canton.Provincia.Pais.Descripcion,
                        FechaConmemorativa = CalendarioLocal.FechaConmemorativa,
                        EsRecuperable = CalendarioLocal.EsRecuperable,
                        FechaFestiva = CalendarioLocal.FechaFestiva,
                        TipoFeriado = "Local",
                        Descripcion = CalendarioLocal.Descripcion
                    });
                }
            //}

            if (!result.Any())
            {
                return new ResponseType<List<DiasFeriadoIdentificacionResponseType>>() { Message = "La consulta no retorna datos", StatusCode = "001", Succeeded = true };
            }

            return new ResponseType<List<DiasFeriadoIdentificacionResponseType>>() { Data = result, Succeeded = true, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000" };
        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<List<DiasFeriadoIdentificacionResponseType>>() { Succeeded = false, Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002" };
        }

    }

    public async Task<ResponseType<List<DiasFeriadosResponseType>>> GetDiasFeriadosByCanton(Guid idCanton, DateTime fecha)
    {
        try
        {
            var objCanton = await _repoCanton.FirstOrDefaultAsync(new GetCantonById(idCanton));
            List<DiasFeriadosResponseType> diasFeriados = new();

            var CalendarioLocal = await _repoCalendarioLocal.FirstOrDefaultAsync(new GetFeriadosLocalesByIdentificacionSpec(idCanton, fecha));
            if (CalendarioLocal != null)
            {
                diasFeriados.Add(new DiasFeriadosResponseType
                {
                    Fecha = fecha,
                    TipoFeriado = "Local",
                    Canton = objCanton.Descripcion,
                    Descripcion = CalendarioLocal.Descripcion,
                    EsRecuperable = CalendarioLocal.EsRecuperable,
                    FechaFestiva = CalendarioLocal.FechaFestiva,
                    FechaConmemorativa = CalendarioLocal.FechaConmemorativa,
                    Pais = objCanton.Provincia.Pais.Descripcion,
                    Provincia = objCanton.Provincia.Descripcion
                });
            }
            var CalendarioNacional = await _repoCalendarioNa.FirstOrDefaultAsync(new GetFeriadosNacionalByIdentificacionSpec(objCanton.Provincia.Pais.Id, fecha));
            if (CalendarioNacional != null)
            {
                diasFeriados.Add(new DiasFeriadosResponseType
                {
                    Fecha = fecha,
                    TipoFeriado = "Nacional",
                    Canton = objCanton.Descripcion,
                    Descripcion = CalendarioNacional.Descripcion,
                    EsRecuperable = CalendarioNacional.EsRecuperable,
                    FechaFestiva = CalendarioNacional.FechaFestiva,
                    FechaConmemorativa = CalendarioNacional.FechaConmemorativa,
                    Pais = objCanton.Provincia.Pais.Descripcion,
                    Provincia = objCanton.Provincia.Descripcion
                });
            }
            if (!diasFeriados.Any())
            {
                return new ResponseType<List<DiasFeriadosResponseType>>() { Message = "La consulta no retorna datos", StatusCode = "001", Succeeded = true };
            }

            return new ResponseType<List<DiasFeriadosResponseType>>() { Data = diasFeriados, Succeeded = true, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000" };
        }
        catch (Exception ex)
        {

            _log.LogError(ex, string.Empty);
            return new ResponseType<List<DiasFeriadosResponseType>>() { Succeeded = false, Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002" };
        }

    }
}
