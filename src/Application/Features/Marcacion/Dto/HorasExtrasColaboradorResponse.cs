using EvaluacionCore.Application.Features.Turnos.Commands.CreateTurnoColaborador;
using EvaluacionCore.Application.Features.Turnos.Commands.UpdateTurnoColaborador;
using EvaluacionCore.Domain.Entities.ControlAsistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Dto;

public class HorasExtrasColaboradorResponse
{
    [JsonPropertyName("codigoUDN")]
    public string CodigoUDN { get; set; }

    [JsonPropertyName("nombreUDN")]
    public string NombreUDN { get; set; }

    [JsonPropertyName("diaSemanaIngreso")]
    public string DiaSemanaIngreso { get; set; }

    [JsonPropertyName("fechaIngreso")]
    public DateTime? FechaIngreso { get; set; }

    [JsonPropertyName("horaIngreso")]
    public string HoraIngreso { get; set; }

    [JsonPropertyName("fechaSalida")]
    public DateTime? FechaSalida { get; set; }

    [JsonPropertyName("horaSalida")]
    public string HoraSalida { get; set; }

    [JsonPropertyName("idTurno")]
    public string IdTurno { get; set; }

    [JsonPropertyName("descTurno")]
    public string DescTurno { get; set; }

    [JsonPropertyName("horasTurnoAsignadas")]
    public string HorasTurnoAsignadas { get; set; }

    [JsonPropertyName("horasExtraAprobadas")]
    public string HorasExtraAprobadas { get; set; }

    [JsonPropertyName("horasTurnoTrabajadas")]
    public string HorasTurnoTrabajadas { get; set; }

    [JsonPropertyName("horasExtraTrabajadas")]
    public string HorasExtraTrabajadas { get; set; }

    [JsonPropertyName("identificacionColaborador")]
    public string IdentificacionColaborador { get; set; }

    [JsonPropertyName("nombreColaborador")]
    public string NombreColaborador { get; set; }

    [JsonPropertyName("idLocalidadPrincipalColaborador")]
    public string IdLocalidadPrincipalColaborador { get; set; }

    [JsonPropertyName("codigoLocalidadPrincipalColaborador")]
    public string CodigoLocalidadPrincipalColaborador { get; set; }

    [JsonPropertyName("nombreLocalidadPrincipalColaborador")]
    public string NombreLocalidadPrincipalColaborador { get; set; }

    [JsonPropertyName("identificacionJefeColaborador")]
    public string IdentificacionJefeColaborador { get; set; }

    [JsonPropertyName("nombreJefeColaborador")]
    public string NombreJefeColaborador { get; set; }

    [JsonPropertyName("clienteTurno")]
    public ClienteTurno ClienteTurno { get; set; }

}

public class ClienteTurno
{
    [JsonPropertyName("idTurno")]
    public string IdTurno { get; set; }

    [JsonPropertyName("idTurnoReceso")]
    public string IdTurnoReceso { get; set; }

    [JsonPropertyName("horasSobretiempoAprobadas")]
    public string HorasSobretiempoAprobadas { get; set; }

    [JsonPropertyName("identificacionAprobador")]
    public string IdentificacionAprobador { get; set; }

    [JsonPropertyName("comentariosAprobacion")]
    public string ComentariosAprobacion { get; set; }

}
