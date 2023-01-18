using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Permisos.Dto;

public class ComboNovedadType
{
    [JsonPropertyName("codigoNovedad")]
    public string CodigoNovedad { get; set; }

    [JsonPropertyName("descripcionNovedad")]
    public string DescripcionNovedad { get; set; }


}
