using Newtonsoft.Json;

namespace EvaluacionCore.Application.Features.Biometria.Dto
{
    public class LandMarkResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("landmarks")]
        public List<object> Landmarks { get; set; }
    }
}
