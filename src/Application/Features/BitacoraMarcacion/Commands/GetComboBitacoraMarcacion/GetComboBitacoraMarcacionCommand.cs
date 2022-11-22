using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetComboBitacoraMarcacion
{
    public record GetComboBitacoraMarcacionCommand(GetComboBitacoraMarcacionRequest BitaMarcacionRequest) : IRequest<ResponseType<List<ComboBitacoraMarcacionType>>>;

    public class GetComboBitacoraMarcacionCommandHandler : IRequestHandler<GetComboBitacoraMarcacionCommand, ResponseType<List<ComboBitacoraMarcacionType>>>
    {
        private readonly IBitacoraMarcacion _repoBitMarcacionAsync;

        public GetComboBitacoraMarcacionCommandHandler(IBitacoraMarcacion repoBitMarcacionAsync)
        {
            _repoBitMarcacionAsync = repoBitMarcacionAsync;
        }

        public async Task<ResponseType<List<ComboBitacoraMarcacionType>>> Handle(GetComboBitacoraMarcacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repoBitMarcacionAsync.GetComboBitacoraMarcacionAsync(request.BitaMarcacionRequest);

                return new ResponseType<List<ComboBitacoraMarcacionType>>() { Data = objResult, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<List<ComboBitacoraMarcacionType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

    }

}
