using Newtonsoft.Json;

namespace EvaluacionCore.Application.Features.Biometria.Dto
{
    public class GetPersonListResponseType
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("face")]
        public FaceLuxandResponseType[] Face { get; set; }
    }

    public partial class FaceLuxandResponseType
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
    }
}
