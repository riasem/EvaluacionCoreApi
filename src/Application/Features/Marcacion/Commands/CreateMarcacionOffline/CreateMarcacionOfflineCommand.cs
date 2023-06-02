using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionOffline;

public record CreateMarcacionOfflineCommand(List<CreateMarcacionOfflineRequest> RequestOfflineMarca,string IdentificacionSesion) : IRequest<ResponseType<string>>;
public class CreateMarcacionOfflineCommandHandler : IRequestHandler<CreateMarcacionOfflineCommand, ResponseType<string>>
{
    private readonly IMarcacion _repository;

    public CreateMarcacionOfflineCommandHandler(IMarcacion repository)
    {
        _repository = repository;
    }

    public async Task<ResponseType<string>> Handle(CreateMarcacionOfflineCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var objResult = await _repository.CreateMarcacionOffline(request.RequestOfflineMarca,request.IdentificacionSesion, cancellationToken);

            return objResult;
        }
        catch (Exception)
        {
            return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
        }
    }
}
