using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Licencia.Commands.ActualizarLicencia;
using EvaluacionCore.Application.Features.Licencia.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Licencia.Interfaces;

public interface ILicencia
{
    Task<ResponseType<string>> ActualizarLicencia(ActualizarLicenciaRequest request, string IdentificacionSesion);

    Task<ResponseType<LicenciaResponseType>> ConsultaLicencia(Guid IdLicencia, string IdentificacionSesion);

}
