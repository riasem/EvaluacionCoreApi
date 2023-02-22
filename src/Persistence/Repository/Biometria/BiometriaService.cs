using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Commands.GetFaceVerification;
using EvaluacionCore.Application.Features.Biometria.Dto;
using EvaluacionCore.Application.Features.Biometria.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Workflow.Persistence.Repository.Biometria
{
    public class BiometriaService : IBiometria
    {
        private readonly IConfiguration _config;
        private readonly ILogger<BiometriaService> _log;
        private readonly string apiKeyLuxand = string.Empty;
        private readonly string apiBaseLuxand = string.Empty;
        private string nombreEnpoint = string.Empty;
        private string uriEndPoint = string.Empty;

        public BiometriaService(IConfiguration config, ILogger<BiometriaService> log)
        {
            _log = log;
            _config = config;
            apiBaseLuxand = _config.GetSection("Luxand:ApiBase").Get<string>();
            apiKeyLuxand = _config.GetSection("Luxand:ApiKey").Get<string>();
        }

        public async Task<ResponseType<string>> GetFaceVerificationAsync(GetFaceVerificationRequest request)
        {
            try
            {
                nombreEnpoint = _config.GetSection("Luxand:FaceLandMarks").Get<string>();
                uriEndPoint = string.Concat(apiBaseLuxand, nombreEnpoint);

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("token", apiKeyLuxand ?? string.Empty);

                #region se adjunta imagen a petición
                byte[] bytes = Convert.FromBase64String(request.Base64);

                Stream stream = new MemoryStream(bytes);

                using var multipartFormContent = new MultipartFormDataContent();

                var fileStreamContent = new StreamContent(stream);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(string.Concat("image/", request.Extension));

                multipartFormContent.Add(fileStreamContent, name: "photo", fileName: string.Concat(request.Nombre, ".", request.Extension));
                #endregion

                var resLuxand = await client.PostAsync(uriEndPoint, multipartFormContent);

                if (!resLuxand.IsSuccessStatusCode)
                    return new ResponseType<string>() { Data = null, Message = "No se pudo realizar la verificación", StatusCode = "101", Succeeded = false };

                var responseType = resLuxand.Content.ReadFromJsonAsync<LandMarkResponse>().Result;

                if (responseType.Status != "success")
                    return new ResponseType<string>() { Data = null, Message = "Ocurrió un inconveniente al realizar la verificación", StatusCode = "101", Succeeded = false };

                if (responseType.Landmarks.Count() == 0)
                    return new ResponseType<string>() { Data = null, Message = "Imagen no válida", StatusCode = "101", Succeeded = false };

                return new ResponseType<string>() { Data = null, Message = "Verificación existosa", StatusCode = "100", Succeeded = true };
            }
            catch (Exception ex)
            {
                _log.LogError(ex, string.Empty);
                return new ResponseType<string>() { Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}
