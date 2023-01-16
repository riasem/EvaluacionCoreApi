using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Calendario.Dto;

public class DiasFeriadosResponseType
{
    public DateTime Fecha { get; set; }

    [JsonPropertyName("canton")]
    public string Canton { get; set; }

    [JsonPropertyName("provincia")]

    public string Provincia { get; set; }

    [JsonPropertyName("pais")]
    public string Pais { get; set; }

    [JsonPropertyName("fechaConmemorativa")]
    public DateTime FechaConmemorativa { get; set; }

    [JsonPropertyName("esRecuperable")]
    public bool? EsRecuperable { get; set; }

    [JsonPropertyName("fechaFestiva")]
    public DateTime FechaFestiva { get; set; }

    [JsonPropertyName("tipoFeriado")]
    public string TipoFeriado { get; set; }


    [JsonPropertyName("descripion")]
    public string Descripcion { get; set; }







    


}
