namespace EvaluacionCore.Application.Features.Marcacion.Dto;

public class ConsultaRecursoType
{
    public string Identificacion { get; set; }

    public string Colaborador { get; set; }

    public string HTotalAsignadas { get; set; }
    public string HTotalTrabajadas { get; set; }
    public string HTotalPendiente { get; set; }

    public List<Dias> Dias { get; set; }
}

public class Dias
{
    public DateTime Fecha { get; set; }

    public string HorasTrabajada { get; set; }

    public string HorasAsignadas { get; set; }

    public string HorasPendiente { get; set; }

    public string LocalidadDescripcion { get; set; }
}
