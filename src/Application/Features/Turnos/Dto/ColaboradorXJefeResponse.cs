using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Turnos.Dto;
public class ColaboradorXJefeResponse
{
    [JsonPropertyName("identificacionColaborador")]
    public string IdentificacionColaborador { get; set; }

    [JsonPropertyName("idColaborador")]
    public Guid? IdColaborador { get; set; }

    [JsonPropertyName("nombreColaborador")]
    public string NombreColaborador { get; set; }

    [JsonPropertyName("idLocalidadPrincipalColaborador")]
    public Guid? IdLocalidadPrincipalColaborador { get; set; }

    [JsonPropertyName("codigoLocalidadPrincipalColaborador")]
    public string CodigoLocalidadPrincipalColaborador { get; set; }

    [JsonPropertyName("nombreLocalidadPrincipalColaborador")]
    public string NombreLocalidadPrincipalColaborador { get; set; }

    [JsonPropertyName("idJefeColaborador")]
    public Guid? IdJefeColaborador { get; set; }

    [JsonPropertyName("identificacionJefeColaborador")]
    public string IdentificacionJefeColaborador { get; set; }

    [JsonPropertyName("nombreJefeColaborador")]
    public string NombreJefeColaborador { get; set; }
}