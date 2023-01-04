using System.Text.Json.Serialization;

namespace EvaluacionCore.Application.Features.Permisos.Dto;

public class ColaboradorConvivenciaType
{
    [JsonPropertyName("identificacion")]
    public string Identificacion { get; set; }

    [JsonPropertyName("Empleado")]
    public string Empleado { get; set; }

    [JsonPropertyName("codigoBiometrico")]
    public string CodigoBiometrico { get; set; }

}
