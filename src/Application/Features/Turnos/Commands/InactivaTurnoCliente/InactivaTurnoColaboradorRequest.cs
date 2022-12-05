using Newtonsoft.Json;

namespace EvaluacionCore.Application.Features.Turnos.Commands.InactivaTurnoColaborador
{
    public partial class InactivaTurnoColaboradorRequest
    {

        [JsonProperty("idTurno")]
        public Guid IdTurno { get; set; }

        [JsonProperty("colaborador")]
        public string Colaborador { get; set; }

        [JsonProperty("idColaborador")]
        public Guid IdColaborador { get; set; }

        [JsonProperty("fechaAsignacion")]
        public DateTime FechaAsignacion { get; set; }

        [JsonProperty("idAsignacion")]
        public Guid IdAsignacion { get; set; }

        [JsonProperty("subturnos")]
        public List<Subturno3> Subturnos { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }

    public partial class Subturno3
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("idTurnoAsignado")]
        public Guid? IdTurnoAsignado { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
