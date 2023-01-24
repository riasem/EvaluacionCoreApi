using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacionCapacidadesEspeciales
{
    public record GetBitacoraMarcacionCapacidadesEspecialesCommand(GetBitacoraMarcacionCapacidadesEspecialesRequest Request) : IRequest<ResponseType<List<BitacoraMarcacionType>>>;

    public class GetMarcacionCommandHandler : IRequestHandler<GetBitacoraMarcacionCapacidadesEspecialesCommand, ResponseType<List<BitacoraMarcacionType>>>
    {
        private readonly IBitacoraMarcacion _repoBitMarcacionAsync;

        public GetMarcacionCommandHandler(IBitacoraMarcacion repoBitMarcacionAsync)
        {
            _repoBitMarcacionAsync = repoBitMarcacionAsync;
        }

        public async Task<ResponseType<List<BitacoraMarcacionType>>> Handle(GetBitacoraMarcacionCapacidadesEspecialesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repoBitMarcacionAsync.GetBitacoraMarcacionCapacidadesEspecialesAsync(request.Request);

                return objResult;
            }
            catch (Exception)
            {
                return new ResponseType<List<BitacoraMarcacionType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

    }
}
