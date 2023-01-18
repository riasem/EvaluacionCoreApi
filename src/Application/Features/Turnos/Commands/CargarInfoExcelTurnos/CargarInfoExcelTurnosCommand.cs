using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Interfaces;
using MediatR;

namespace EvaluacionCore.Application.Features.Turnos.Commands.CargarInfoExcelTurnos
{
    public record CargarInfoExcelTurnosCommand(CargarInfoExcelTurnosRequest TurnoRequest) : IRequest<ResponseType<string>>;

    public class CreateTurnoColaboradorCommandHandler : IRequestHandler<CargarInfoExcelTurnosCommand, ResponseType<string>>
    {
        private readonly ITurnosAsignadosExcel _repoTurnosExcelAsync;

        public CreateTurnoColaboradorCommandHandler(ITurnosAsignadosExcel repoTurnosExcelAsync)
        {
            _repoTurnosExcelAsync = repoTurnosExcelAsync;
        }

        public async Task<ResponseType<string>> Handle(CargarInfoExcelTurnosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objResult = await _repoTurnosExcelAsync.CargarInfoExcelTurnosAsync(request.TurnoRequest);

                if (objResult == 0)
                    return new ResponseType<string>() { Data = null, Message = "No se pudo cargar los turnos", StatusCode = "101", Succeeded = false };

                return new ResponseType<string>() { Data = null, Message = "Turnos cargados correctamente", StatusCode = "100", Succeeded = true };
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }

}
