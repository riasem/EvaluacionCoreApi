namespace EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetComboBitacoraMarcacion
{
    public class GetComboBitacoraMarcacionRequest
    {
        public string Tipo { get; set; }
        public string Udn { get; set; }
        public string Area { get; set; }
        public string Identificacion { get; set; }
    }
}