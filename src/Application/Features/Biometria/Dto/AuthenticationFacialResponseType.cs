using Newtonsoft.Json;

namespace EvaluacionCore.Application.Features.Biometria.Dto
{
    public class AuthenticationFacialResponseType
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("probability")]
        public double Probability { get; set; }
    }
}
