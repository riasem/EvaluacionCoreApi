using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.BitacoraMarcacion.Dto
{
    public class ComboBitacoraMarcacionType
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }
        
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }
    }
}
