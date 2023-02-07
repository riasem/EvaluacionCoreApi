using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Dto;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionWeb
{
    public record CreateMarcacionWebCommand(CreateMarcacionWebRequest CreateMarcacion) : IRequest<ResponseType<MarcacionWebResponseType>>;

    public class CreateMarcacionWebCommandHandler : IRequestHandler<CreateMarcacionWebCommand, ResponseType<MarcacionWebResponseType>>
    {
        private readonly IMarcacion _repository;

        public CreateMarcacionWebCommandHandler(IMarcacion repository)
        {
            _repository = repository;
        }

        public async Task<ResponseType<MarcacionWebResponseType>> Handle(CreateMarcacionWebCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repository.CreateMarcacionWeb(request.CreateMarcacion, cancellationToken);

                return objResult;
            }
            catch (Exception)
            {
                return new ResponseType<MarcacionWebResponseType>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }

}
