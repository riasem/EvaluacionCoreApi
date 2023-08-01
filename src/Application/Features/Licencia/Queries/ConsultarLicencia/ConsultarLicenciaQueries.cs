using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Licencia.Dto;
using EvaluacionCore.Application.Features.Licencia.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using EvaluacionCore.Application.Features.Marcacion.Queries.GetDispositivoMarcacion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Licencia.Queries.ConsultarLicencia;

public record ConsultarLicenciaQueries(Guid idLicencia, string IdentificacionSesion) : IRequest<ResponseType<LicenciaResponseType>>;

public class ConsultarLicenciaQueriesHandle : IRequestHandler<ConsultarLicenciaQueries, ResponseType<LicenciaResponseType>>
{
    public readonly ILicencia _repository;
    public ConsultarLicenciaQueriesHandle(ILicencia repository)
    {
        _repository = repository;
    }

    public async Task<ResponseType<LicenciaResponseType>> Handle(ConsultarLicenciaQueries request, CancellationToken cancellationToken)
    {
        var result = await _repository.ConsultaLicencia(request.idLicencia,request.IdentificacionSesion );

        return result;
    }

}
