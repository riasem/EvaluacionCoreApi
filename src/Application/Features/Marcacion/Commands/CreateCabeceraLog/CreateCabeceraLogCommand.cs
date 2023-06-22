using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateCabeceraLog;

public record CreateCabeceraLogCommand(CreateCabeceraLogRequest requestCabecera,string IdentificacionSesion) : IRequest<ResponseType<string>>;
public class CreateCabeceraLogCommandHandler : IRequestHandler<CreateCabeceraLogCommand, ResponseType<string>>
{
    public IMarcacionOffline _repository;

    public CreateCabeceraLogCommandHandler(IMarcacionOffline repository)
    {
        _repository = repository;
    }

    public async Task<ResponseType<string>> Handle(CreateCabeceraLogCommand request, CancellationToken cancellationToken)
    {
        var objResult = await _repository.CreateCabeceraLogOffline(request.requestCabecera,request.IdentificacionSesion, cancellationToken);

        return objResult;
    }
}
