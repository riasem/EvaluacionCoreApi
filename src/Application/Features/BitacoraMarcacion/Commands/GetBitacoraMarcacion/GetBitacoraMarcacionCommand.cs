using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.GetBitacoraMarcacion
{
    public record GetBitacoraMarcacionCommand(GetBitacoraMarcacionRequest BitaMarcacionRequest) : IRequest<ResponseType<List<BitacoraMarcacionType>>>;

    public class GetMarcacionCommandHandler : IRequestHandler<GetBitacoraMarcacionCommand, ResponseType<List<BitacoraMarcacionType>>>
    {
        private readonly IBitacoraMarcacion _repoBitMarcacionAsync;

        public GetMarcacionCommandHandler(IBitacoraMarcacion repoBitMarcacionAsync)
        { 
            _repoBitMarcacionAsync = repoBitMarcacionAsync;
        }

        public async Task<ResponseType<List<BitacoraMarcacionType>>> Handle(GetBitacoraMarcacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repoBitMarcacionAsync.GetBitacoraMarcacionAsync(request.BitaMarcacionRequest);

                return new ResponseType<List<BitacoraMarcacionType>>() { Data = objResult, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<List<BitacoraMarcacionType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

    }
}