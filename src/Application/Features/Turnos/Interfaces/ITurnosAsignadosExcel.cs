using EvaluacionCore.Application.Features.Turnos.Commands.CargarInfoExcelTurnos;
using EvaluacionCore.Application.Features.Turnos.Commands.GetTurnosAsignadosExcel;

namespace EvaluacionCore.Application.Features.Turnos.Interfaces
{
    public interface ITurnosAsignadosExcel
    {
        Task<string> GetTurnosAsignadosExcelAsync(GetTurnosAsignadosExcelRequest request);
        Task<int> CargarInfoExcelTurnosAsync(CargarInfoExcelTurnosRequest request);
    }
}
