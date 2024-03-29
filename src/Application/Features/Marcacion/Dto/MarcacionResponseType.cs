﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Dto;

public class MarcacionResponseType
{
    [JsonPropertyName("marcacionId")]
    public int MarcacionId { get; set; }

    //[JsonPropertyName("codigoMarcacionId")]
    //public int CodigoMarcacionId { get; set; }

    [JsonPropertyName("tipoMarcacion")]
    public string TipoMarcacion { get; set; }

    [JsonPropertyName("estadoMarcacion")]
    public string EstadoMarcacion { get; set; }
}
