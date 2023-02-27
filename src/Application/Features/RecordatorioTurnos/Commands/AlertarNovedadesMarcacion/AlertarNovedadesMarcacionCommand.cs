using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.EvalCore.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.AlertaTurnos.Commands.AlertaTurnosNoAsignados;

public record AlertarNovedadesMarcacionCommand() : IRequest<ResponseType<string>>;


public class AlertarNovedadesMarcacionCommandHandler : IRequestHandler<AlertarNovedadesMarcacionCommand, ResponseType<string>>
{
    private readonly IRecordatorio _repository;
    
    public AlertarNovedadesMarcacionCommandHandler(IRecordatorio repository)
    {
        _repository = repository; 
    }

    public async Task<ResponseType<string>> Handle(AlertarNovedadesMarcacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var (objResult, sucess) = await _repository.ProcesarRecordatorios(cancellationToken);

            return new ResponseType<string>() { Data = null, Message = objResult, StatusCode = sucess == 1 ? "000" : "001", Succeeded = sucess == 1 };
            
        }
        catch (Exception)
        {
            return new ResponseType<string>() { Data = null, Message = "Ocurrió un error.", StatusCode = "002", Succeeded = false };
        }
        
    }

}
