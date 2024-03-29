﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Marcacion.Commands.CreateMarcacion;

public class CreateMarcacionRequest
{
    [JsonPropertyName("codigoEmpleado")]
    public string CodigoEmpleado { get; set; }

    [JsonPropertyName("dispositivoId")]
    public string DispositivoId { get; set; }

    [JsonPropertyName("localidadId")]
    public Guid LocalidadId { get; set; }

    [JsonPropertyName("identificacionSesion")]
    public string IdentificacionSesion { get; set; }

    [JsonPropertyName("tipoComunicacion")]
    public string TipoComunicacion { get; set; }

    [JsonPropertyName("consultaMonitoLogRiasem")]
    public bool ConsultaMonitoLogRiasem { get; set; } = true;

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("descripcion")]
    public string Descripcion { get; set; } = String.Empty;
}