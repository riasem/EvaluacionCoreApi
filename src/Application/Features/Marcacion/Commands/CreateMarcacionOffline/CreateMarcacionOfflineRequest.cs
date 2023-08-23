using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CreateMarcacionOffline;

public class CreateMarcacionOfflineRequest
{
    [JsonPropertyName("identificacion")]
    public string Identificacion { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("codigoBiometrico")]
    public string CodigoBiometrico { get; set; }

    [JsonPropertyName("imagen")]
    public string Imagen { get; set; }

    [JsonPropertyName("extension")]
    public string Extension { get; set; }

    [JsonPropertyName("idCabecera")]
    public Guid IdCabecera { get; set; }

    [JsonPropertyName("cantidadSincronizada")]
    public int CantidadSincronizada { get; set; }

    public string TipoComunicacion { get; set; }

    //[JsonPropertyName("imagen")]
    //public IFormFile Imagen { get; set; }
}
