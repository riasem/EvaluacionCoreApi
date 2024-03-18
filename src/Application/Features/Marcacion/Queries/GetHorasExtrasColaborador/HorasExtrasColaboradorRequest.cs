using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Queries.GetHorasExtrasColaborador;

public class HorasExtrasColaboradorRequest
{
    [JsonPropertyName("opcion")]
    public string Opcion { get; set; } = string.Empty;

    [JsonPropertyName("fechaDesde")]
    public DateTime? FechaDesde { get; set; }

    [JsonPropertyName("fechaHasta")]
    public DateTime? FechaHasta { get; set; }

    [JsonPropertyName("identificaciones")]
    public List<Identificacion> Identificaciones { get; set; }

}

public class Identificacion
{
    [JsonPropertyName("identificacionJefatura")]
    public string IdentificacionJefatura { get; set; }

    [JsonPropertyName("identificacionColaborador")]
    public string IdentificacionColaborador { get; set; }

    [JsonPropertyName("idLocalidad")]
    public Guid? IdLocalidad { get; set; }
}

public class DatosFiltro
{
    public string TipoFiltro { get; set; }

    public string ValorFiltro { get; set; }

}