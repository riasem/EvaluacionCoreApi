using AutoMapper;
using Castle.Core.Logging;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Licencia.Commands.ActualizarLicencia;
using EvaluacionCore.Application.Features.Licencia.Dto;
using EvaluacionCore.Application.Features.Licencia.Interfaces;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Seguridad;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Persistence.Repository.Licencia;

public class LicenciaService : ILicencia
{
    private IMapper _mapper;
    private ILogger<LicenciaService> _log;
    private readonly IRepositoryAsync<LicenciaTerceroSG> _repoLicenciaTercero;
    private readonly IRepositoryAsync<LogLicenciaTerceroSG> _repoLogLicenciaTerceroSG;
    public LicenciaService(IRepositoryAsync<LicenciaTerceroSG> repoLicenciaTercero, IRepositoryAsync<LogLicenciaTerceroSG> repoLogLicenciaTerceroSG, IMapper mapper, ILogger<LicenciaService> log)
    {
        _repoLicenciaTercero = repoLicenciaTercero;
        _repoLogLicenciaTerceroSG = repoLogLicenciaTerceroSG;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<string>> ActualizarLicencia(ActualizarLicenciaRequest request, string IdentificacionSesion)
    {
        try
        {
            var licencia = await _repoLicenciaTercero.GetByIdAsync(request.IdLicencia);

            if (licencia is null) return new ResponseType<string> { Message = "No existe la licencia", StatusCode = "201", Succeeded = true };

            licencia.TipoLicencia = request.TipoLicencia;
            licencia.CodigoLicencia = request.CodigoLicencia;
            licencia.FechaInicioSuscripcion = request.FechaInicio;
            licencia.FechaUltimaRenovacion = request.FechaUltima;
            licencia.FechaProximaRenovacion = request.FechaProxima;
            licencia.ReferenciaTecnica = request.ReferenciaTecnica;
            licencia.CostoLicencia = request.CostoLicencia;
            licencia.UsuarioModificacion = IdentificacionSesion;
            licencia.FechaModificacion = DateTime.Now;
            await _repoLicenciaTercero.UpdateAsync(licencia);

            LogLicenciaTerceroSG objLogLicencia = new()
            {
                IdLicenciaTercero = licencia.IdLicenciaTercero,
                CodigoLicencia = licencia.CodigoLicencia,
                CostoLicencia = licencia.CostoLicencia,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = IdentificacionSesion,
                FechaRenovacion = licencia.FechaUltimaRenovacion,
                FechaProximaRenovacion = licencia.FechaProximaRenovacion,
                ReferenciaTecnica = licencia.ReferenciaTecnica,
                
            };


            var logLicencia = await _repoLogLicenciaTerceroSG.AddAsync(objLogLicencia);

            if (logLicencia is null) return new ResponseType<string> { Message = "No se ha podido guardar el log", Succeeded = true, StatusCode = "201" };

            return new ResponseType<string> { Message = "Se ha actualizado correctamente", StatusCode = "200", Succeeded = true };
        }
        catch (Exception ex)
        {

            _log.LogError(ex, string.Empty);
            return new ResponseType<string> { Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
        }

    }


    public async Task<ResponseType<LicenciaResponseType>> ConsultaLicencia(Guid IdLicencia, string IdentificacionSesion)
    {
        try
        {
            var objLicencia = await _repoLicenciaTercero.GetByIdAsync(IdLicencia);

            if (objLicencia == null) return new ResponseType<LicenciaResponseType> { Message = "No Existe Licencia", StatusCode = "001", Succeeded = true };

            var result = _mapper.Map<LicenciaResponseType>(objLicencia);

            return new ResponseType<LicenciaResponseType> { Message = "Consulta generada correctamente", StatusCode = "000", Succeeded = true, Data = result };
        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<LicenciaResponseType> { Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002", Succeeded = true, Data = null };
        }



    }
}
