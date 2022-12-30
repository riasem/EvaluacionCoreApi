using EvaluacionCore.Application.Features.Turnos.Commands.GetTurnosAsignadosExcel;

namespace EvaluacionCore.Application.Features.Turnos.Interfaces
{
    public interface ITurnosAsignadosExcel
    {
        Task<string> GetTurnosAsignadosExcelAsync(GetTurnosAsignadosExcelRequest request);
    }
}
