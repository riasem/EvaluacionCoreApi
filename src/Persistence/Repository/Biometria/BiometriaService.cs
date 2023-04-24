using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Biometria.Commands.AuthenticationFacial;
using EvaluacionCore.Application.Features.Biometria.Commands.CreateFacePerson;
using EvaluacionCore.Application.Features.Biometria.Commands.GetFaceVerification;
using EvaluacionCore.Application.Features.Biometria.Dto;
using EvaluacionCore.Application.Features.Biometria.Interfaces;
using EvaluacionCore.Application.Features.Common.Specifications;
using EvaluacionCore.Domain.Entities.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Luxand;

namespace Workflow.Persistence.Repository.Biometria
{
    public class BiometriaService : IBiometria
    {
        private readonly IConfiguration _config;
        private readonly ILogger<BiometriaService> _log;
        private readonly IRepositoryAsync<Cliente> _repoCliente;
        private readonly string apiKeyLuxand = string.Empty;
        private readonly string apiBaseLuxand = string.Empty;
        private string nombreEnpoint = string.Empty;
        private string uriEndPoint = string.Empty;

        public BiometriaService(IConfiguration config, ILogger<BiometriaService> log, IRepositoryAsync<Cliente> repoCliente)
        {
            _log = log;
            _config = config;
            _repoCliente = repoCliente;
            apiBaseLuxand = _config.GetSection("Luxand:ApiBase").Get<string>();
            apiKeyLuxand = _config.GetSection("Luxand:ApiKey").Get<string>();
        }

        public async Task<ResponseType<string>> AuthenticationFacialAsync(AuthenticationFacialRequest request)
        {

            try
            {
                nombreEnpoint = _config.GetSection("Luxand:FaceVerification").Get<string>();
                uriEndPoint = string.Concat(apiBaseLuxand, nombreEnpoint);

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("token", apiKeyLuxand ?? string.Empty);

                #region Parametros archivo y nombre persona
                byte[] bytes = Convert.FromBase64String(request.Base64);

                Stream stream = new MemoryStream(bytes);

                using var multipartFormContent = new MultipartFormDataContent();

                var fileStreamContent = new StreamContent(stream);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(string.Concat("image/", request.Extension));

                multipartFormContent.Add(fileStreamContent, name: "photo", fileName: string.Concat(request.Nombre, ".", request.Extension));
                #endregion
                //Consultamos los datos del colaborador
                //var objColaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(request.Identificacion));

                var resLuxand = await client.PostAsync(string.Concat(uriEndPoint, "/", request.FacialPersonUid), multipartFormContent);

                var responseType = resLuxand.Content.ReadFromJsonAsync<AuthenticationFacialResponseType>().Result;

                if (responseType.Status == "success")
                {
                    if (responseType.Probability > 0.96)
                        return new ResponseType<string>() { Data = null, Message = "Autenticación existosa", StatusCode = "100", Succeeded = true };
                    else
                        return new ResponseType<string>() { Data = null, Message = "Autenticación fallida", StatusCode = "101", Succeeded = false };
                }
                else
                    return new ResponseType<string>() { Data = null, Message = "Autenticación fallida", StatusCode = "101", Succeeded = false };
            }
            catch (Exception ex)
            {
                _log.LogError(ex, string.Empty);
                return new ResponseType<string>() { Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }



            #region Autenticación con SDK Comentado


            //try
            //{
            //    nombreEnpoint = _config.GetSection("Luxand:FaceVerification").Get<string>();
            //    uriEndPoint = string.Concat(apiBaseLuxand, nombreEnpoint);

            //    using var client = new HttpClient();
            //    client.DefaultRequestHeaders.Add("token", apiKeyLuxand ?? string.Empty);

            //    #region Parametros archivo y nombre persona
            //    byte[] bytes = Convert.FromBase64String(request.Base64);
            //    var rutaBase = _config.GetSection("Adjuntos:RutaBase").Get<string>();
            //    var directorio = rutaBase + "temp/";
            //    if (!Directory.Exists(directorio))
            //    {
            //        Directory.CreateDirectory(directorio);
            //    }
            //    var rutafinal = directorio + "imagen."+request.Extension;
            //    await File.WriteAllBytesAsync(rutafinal, bytes);
            //    #endregion

            //    #region Luxand
            //    FSDK.ActivateLibrary("aK4rAKMCHRmlWrZoR36CfwHj+nGoV3O75HmPUDj0QLlutgwJ7IkJfOXVpSDlyaBhu+4nowm120626ilmjXkoPjDmU6sOtWSvdT2zKnjjVduRhmaFv487DMH/YwIJaAbVMQxwE4ukpLBpd8alT+HbkRrbCAVNJYNrs7qi3srdsCw=");
            //    FSDK.InitializeLibrary();
            //    var objColaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(request.Identificacion));
            //    if (objColaborador is null) return new ResponseType<string>() { Data = null, Message = "Colaborador no tiene Imagen de Perfil", StatusCode = "101", Succeeded = true };

            //    FSDK.CImage imageCola = new FSDK.CImage(objColaborador.ImagenPerfil.RutaAcceso.ToString());
            //    FSDK.CImage image = new FSDK.CImage(rutafinal); // Imagen enviada

            //    File.Delete(rutafinal);

            //    byte[] template = new byte[FSDK.TemplateSize];
            //    byte[] templateImgCola = new byte[FSDK.TemplateSize];
            //    FSDK.TFacePosition facePosition = new FSDK.TFacePosition();
            //    FSDK.TFacePosition facePositionCola = new FSDK.TFacePosition();
            //    facePosition = image.DetectFace();
            //    facePositionCola = imageCola.DetectFace();
            //    template = image.GetFaceTemplateInRegion(ref facePosition);
            //    templateImgCola = imageCola.GetFaceTemplateInRegion(ref facePositionCola);

            //    float Similarity = 0.0f;

            //    FSDK.MatchFaces(ref template, ref templateImgCola, ref Similarity);
            //    #endregion

            //    if (Similarity >= 0.96)
            //    {
            //        return new ResponseType<string>() { Data = null, Message = "Autenticación existosa", StatusCode = "100", Succeeded = true };
            //    }
            //    else
            //    {
            //        return new ResponseType<string>() { Data = null, Message = "Autenticación fallida", StatusCode = "101", Succeeded = false };
            //    }

            //}
            //catch (Exception ex)
            //{
            //    _log.LogError(ex, string.Empty);
            //    return new ResponseType<string>() { Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            //}
            #endregion
        }

        public async Task<ResponseType<string>> AuthenticationFacialLastAsync(AuthenticationFacialLastRequest request)
        {
            try
            {
                //Consultamos los datos del colaborador
                var objColaborador = await _repoCliente.FirstOrDefaultAsync(new GetColaboradorByIdentificacionSpec(request.Identificacion));
                if (objColaborador is null) return new ResponseType<string>() { Data = null, Message = "Colaborador no tiene Imagen de Perfil", StatusCode = "101", Succeeded = true };

                #region Guardar temporalmente Archivo
                var rutaBase = _config.GetSection("Adjuntos:RutaBase").Get<string>();
                var directorio = rutaBase + "temp/";
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                var rutaFinal = directorio + request.AdjuntoFiles.FileName;

                using (var stream = System.IO.File.Create(rutaFinal))
                {
                    await request.AdjuntoFiles.CopyToAsync(stream);
                }

                #endregion


                #region Luxand
                FSDK.ActivateLibrary("aK4rAKMCHRmlWrZoR36CfwHj+nGoV3O75HmPUDj0QLlutgwJ7IkJfOXVpSDlyaBhu+4nowm120626ilmjXkoPjDmU6sOtWSvdT2zKnjjVduRhmaFv487DMH/YwIJaAbVMQxwE4ukpLBpd8alT+HbkRrbCAVNJYNrs7qi3srdsCw=");
                FSDK.InitializeLibrary();

                FSDK.CImage imageCola = new FSDK.CImage(objColaborador.ImagenPerfil.RutaAcceso.ToString());
                FSDK.CImage image = new FSDK.CImage(rutaFinal); // Imagen enviada


                byte[] template = new byte[FSDK.TemplateSize];
                byte[] templateImgCola = new byte[FSDK.TemplateSize];
                FSDK.TFacePosition facePosition = new FSDK.TFacePosition();
                FSDK.TFacePosition facePositionCola = new FSDK.TFacePosition();
                facePosition = image.DetectFace();
                facePositionCola = imageCola.DetectFace();
                template = image.GetFaceTemplateInRegion(ref facePosition);
                templateImgCola = imageCola.GetFaceTemplateInRegion(ref facePositionCola);

                #endregion

                float Similarity = 0.0f;
                File.Delete(rutaFinal);
                //FSDK.GetMatchingThresholdAtFAR(96 / 100, ref Threshold);
                FSDK.MatchFaces(ref template, ref templateImgCola, ref Similarity);

                if (Similarity >= 0.96)
                {
                    return new ResponseType<string>() { Data = null, Message = "Autenticación existosa", StatusCode = "100", Succeeded = true };
                }
                else
                {
                    return new ResponseType<string>() { Data = null, Message = "Autenticación fallida", StatusCode = "101", Succeeded = false };
                }

                //nombreEnpoint = _config.GetSection("Luxand:FaceVerification").Get<string>();
                //uriEndPoint = string.Concat(apiBaseLuxand, nombreEnpoint);

                //using var client = new HttpClient();
                //client.DefaultRequestHeaders.Add("token", apiKeyLuxand ?? string.Empty);

                //#region Parametros archivo y nombre persona

                //using var multipartFormContent = new MultipartFormDataContent();
                //var fileStreamContent = new StreamContent(request.AdjuntoFiles.OpenReadStream());
                //multipartFormContent.Add(fileStreamContent, name: "photo", fileName: request.AdjuntoFiles.FileName);

                //#endregion

                //var resLuxand = await client.PostAsync(string.Concat(uriEndPoint, "/", request.FacialPersonUid), multipartFormContent);

                //var responseType = resLuxand.Content.ReadFromJsonAsync<AuthenticationFacialResponseType>().Result;

                //if (responseType.Status == "success")
                //{
                //    if (responseType.Probability > 0.96)
                //        return new ResponseType<string>() { Data = null, Message = "Autenticación existosa", StatusCode = "100", Succeeded = true };
                //    else
                //        return new ResponseType<string>() { Data = null, Message = "Autenticación fallida", StatusCode = "101", Succeeded = false };
                //}
                //else
                //    return new ResponseType<string>() { Data = null, Message = "Autenticación fallida", StatusCode = "101", Succeeded = false };
            }
            catch (Exception ex)
            {
                _log.LogError(ex, string.Empty);
                return new ResponseType<string>() { Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }


        public async Task<ResponseType<string>> CreateFacePersonAsync(CreateFacePersonRequest request)
        {
            try
            {
                nombreEnpoint = _config.GetSection("Luxand:GetCreatePerson").Get<string>();
                uriEndPoint = string.Concat(apiBaseLuxand, nombreEnpoint);

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("token", apiKeyLuxand ?? string.Empty);

                #region Parametros archivo y nombre persona
                byte[] bytes = Convert.FromBase64String(request.Base64);

                Stream stream = new MemoryStream(bytes);

                using var multipartFormContent = new MultipartFormDataContent();

                var fileStreamContent = new StreamContent(stream);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(string.Concat("image/", request.Extension));

                multipartFormContent.Add(fileStreamContent, name: "photo", fileName: string.Concat(request.Nombre, ".", request.Extension));

                multipartFormContent.Add(new StringContent(request.Colaborador), name: "name");
                #endregion

                var resLuxand = await client.PostAsync(uriEndPoint, multipartFormContent);

                if (!resLuxand.IsSuccessStatusCode)
                    return new ResponseType<string>() { Data = null, Message = "No se pudo crear la persona", StatusCode = "101", Succeeded = false };

                var responseType = resLuxand.Content.ReadFromJsonAsync<CreatePersonResponseType>().Result;

                if (responseType.Status != "success")
                    return new ResponseType<string>() { Data = null, Message = "Ocurrió un inconveniente al realizar la creación", StatusCode = "101", Succeeded = false };

                await VerifyPersonExistsLuxand(uriEndPoint, request.FacialPersonUid);

                return new ResponseType<string>() { Data = responseType.Uuid.ToString(), Message = "Creación existosa", StatusCode = "100", Succeeded = true };
            }
            catch (Exception ex)
            {
                _log.LogError(ex, string.Empty);
                return new ResponseType<string>() { Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

        public async Task<ResponseType<string>> GetFaceVerificationAsync(GetFaceVerificationRequest request)
        {
            try
            {
                nombreEnpoint = _config.GetSection("Luxand:FaceLandMarks").Get<string>();
                uriEndPoint = string.Concat(apiBaseLuxand, nombreEnpoint);

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("token", apiKeyLuxand ?? string.Empty);

                byte[] bytes = Convert.FromBase64String(request.Base64);

                Stream stream = new MemoryStream(bytes);

                using var multipartFormContent = new MultipartFormDataContent();

                var fileStreamContent = new StreamContent(stream);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(string.Concat("image/", request.Extension));

                multipartFormContent.Add(fileStreamContent, name: "photo", fileName: string.Concat(request.Nombre, ".", request.Extension));

                var resLuxand = await client.PostAsync(uriEndPoint, multipartFormContent);

                if (!resLuxand.IsSuccessStatusCode)
                    return new ResponseType<string>() { Data = null, Message = "No se pudo realizar la verificación", StatusCode = "101", Succeeded = false };

                var responseType = resLuxand.Content.ReadFromJsonAsync<LandMarkResponseType>().Result;

                if (responseType.Status != "success")
                    return new ResponseType<string>() { Data = null, Message = "Ocurrió un inconveniente al realizar la verificación", StatusCode = "101", Succeeded = false };

                if (responseType.Landmarks.Count == 0)
                    return new ResponseType<string>() { Data = null, Message = "Imagen no válida", StatusCode = "101", Succeeded = false };

                return new ResponseType<string>() { Data = null, Message = "Verificación existosa", StatusCode = "100", Succeeded = true };
            }
            catch (Exception ex)
            {
                _log.LogError(ex, string.Empty);
                return new ResponseType<string>() { Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }

        public async Task<bool> VerifyPersonExistsLuxand(string uri, string uidPerson)
        {
            try
            {
                if (!string.IsNullOrEmpty(uidPerson))
                {
                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("token", apiKeyLuxand ?? string.Empty);

                    var resDeleteLuxand = await client.DeleteAsync(string.Concat(uriEndPoint, "/", uidPerson));
                }

                return true;
            }
            catch { return false; }
        }

    }
}
