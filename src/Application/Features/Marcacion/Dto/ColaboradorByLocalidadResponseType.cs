using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Dto;

public class ColaboradorByLocalidadResponseType
{
    [JsonPropertyName("identificacion")]
    public string Identificacion { get; set; }

    [JsonPropertyName("empleado")]
    public string Empleado { get; set; }

    [JsonPropertyName("rutaImagen")]
    public string RutaImagen { get; set; }

    [JsonPropertyName("nombreUdn")]
    public string NombreUdn { get; set; }

    [JsonPropertyName("codigoUdn")]
    public string CodigoUdn { get; set; }

    [JsonPropertyName("codigoBiometrico")]
    public string CodigoBiometrico { get; set; }

    [JsonPropertyName("administrador")]
    public string Administrador { get; set; }

    [JsonPropertyName("clave")]
    public string Clave { get; set; }
}