using Newtonsoft.Json;

namespace EvaluacionCore.Application.Features.Biometria.Dto
{
    public class CreatePersonResponseType
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
