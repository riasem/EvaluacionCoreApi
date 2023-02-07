using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionWeb
{
    public record CreateMarcacionWebCommand(CreateMarcacionWebRequest CreateMarcacion) : IRequest<ResponseType<string>>;

    public class CreateMarcacionWebCommandHandler : IRequestHandler<CreateMarcacionWebCommand, ResponseType<string>>
    {
        private readonly IMarcacion _repository;

        public CreateMarcacionWebCommandHandler(IMarcacion repository)
        {
            _repository = repository;
        }

        public async Task<ResponseType<string>> Handle(CreateMarcacionWebCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repository.CreateMarcacionWeb(request.CreateMarcacion, cancellationToken);

                return objResult;
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }

}
