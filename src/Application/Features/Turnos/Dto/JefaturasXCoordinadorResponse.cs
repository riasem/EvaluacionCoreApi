using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Turnos.Dto;
public class JefaturasXCoordinadorResponse
{
    [JsonPropertyName("idColaboradorCoordinador")]
    public string IdColaboradorCoordinador { get; set; }

    [JsonPropertyName("nombreColaboradorCoordinador")]
    public string NombreColaboradorCoordinador { get; set; }

    [JsonPropertyName("identificacionColaboradorJefe")]
    public string IdentificacionColaboradorJefe { get; set; }

    [JsonPropertyName("idColaboradorJefe")]
    public string IdColaboradorJefe { get; set; }

    [JsonPropertyName("nombreColaboradorJefe")]
    public string NombreColaboradorJefe { get; set; }

}