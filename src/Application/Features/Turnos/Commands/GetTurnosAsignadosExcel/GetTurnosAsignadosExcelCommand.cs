using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Commands.GetTurnosAsignadosExcel
{
    public record GetTurnosAsignadosExcelCommand(GetTurnosAsignadosExcelRequest TurnosExcelRequest) : IRequest<ResponseType<string>>;

    public class GetTurnosAsignadosExcelCommandHandler : IRequestHandler<GetTurnosAsignadosExcelCommand, ResponseType<string>>
    {
        private readonly ITurnosAsignadosExcel _repoTurnosAsignadosExcelAsync;

        public GetTurnosAsignadosExcelCommandHandler(ITurnosAsignadosExcel repoTurnosAsignadosExcelAsync)
        {
            _repoTurnosAsignadosExcelAsync = repoTurnosAsignadosExcelAsync;
        }

        public async Task<ResponseType<string>> Handle(GetTurnosAsignadosExcelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repoTurnosAsignadosExcelAsync.GetTurnosAsignadosExcelAsync(request.TurnosExcelRequest);

                return new ResponseType<string>() { Data = objResult, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

    }

}