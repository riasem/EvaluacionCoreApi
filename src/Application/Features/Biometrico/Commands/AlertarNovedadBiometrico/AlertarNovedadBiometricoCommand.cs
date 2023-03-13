using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometrico.Interfaces;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using MediatR;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.AlertarNovedadBiometrico
{
    public record AlertarNovedadBiometricoCommand(string parametros) : IRequest<ResponseType<List<BitacoraMarcacionType>>>;

    public class AlertarNovedadBiometricoCommandHandler : IRequestHandler<AlertarNovedadBiometricoCommand, ResponseType<List<BitacoraMarcacionType>>>
    {
        private readonly IBiometrico _repoBitMarcacionAsync;

        public AlertarNovedadBiometricoCommandHandler(IBiometrico repoBitMarcacionAsync)
        { 
            _repoBitMarcacionAsync = repoBitMarcacionAsync;
        }

        public async Task<ResponseType<List<BitacoraMarcacionType>>> Handle(AlertarNovedadBiometricoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repoBitMarcacionAsync.AlertarNovedadBiometricoAsync("");

                return new ResponseType<List<BitacoraMarcacionType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<List<BitacoraMarcacionType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

    }
}