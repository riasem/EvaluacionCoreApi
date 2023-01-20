using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Permisos.Dto;

public class ComboNovedadType
{
    [JsonPropertyName("codigo")]
    public string Codigo { get; set; }

    [JsonPropertyName("descripcion")]
    public string Descripcion { get; set; }


}
