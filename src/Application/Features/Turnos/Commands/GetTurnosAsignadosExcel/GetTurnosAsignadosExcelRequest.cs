namespace EvaluacionCore.Application.Features.Turnos.Commands.GetTurnosAsignadosExcel
{
    public class GetTurnosAsignadosExcelRequest
    {
        public string CodUdn { get; set; }
        public string CodArea { get; set; } 
        public string CodScc { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
    }
}
