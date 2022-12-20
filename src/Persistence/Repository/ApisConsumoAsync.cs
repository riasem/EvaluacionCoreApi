using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Permisos.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace EvaluacionCore.Persistence.Repository.General;

public class ApisConsumoAsync : IApisConsumoAsync
{
    private readonly IConfiguration _config;
    private readonly ILogger<ApisConsumoAsync> _log;
   
    public ApisConsumoAsync(ILogger<ApisConsumoAsync> log, IConfiguration config)
    {
        _log = log;
        _config = config;
    }

   
    public async Task<(bool Success, object Data)> GetEndPoint(object request,string uriEndPoint,string nombreEndPoint)
    {
        try
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZGVudGlmaWNhY2lvbiI6IjA5NTA5NDcyODMiLCJPcmdhbml6YWNpb25JZCI6IiIsIlRva2VuRWNvbW1lcmNlIjoiL0JnV3M5bkQ0My8wRCtUVmtWaWZlajlqdFdhVk1qaWRFSGUzMy8wWmpmMzVkZkU3aUk3UkdtOG4xbWt5WURLbVRkc1Y4SUQzUi8yQm9GUkNMWVloZjhsZFdmaVo0VENDWHVuNTdvOXRyWnM9IiwiSXBEaXNwb3NpdGl2byI6IjE5Mi4xNjguMS4xIiwiSWREaXNwb3NpdGl2byI6ImE4MjVkMWUwMTgxNTM4MzMiLCJuYmYiOjE2Njk4MjYwMDcsImV4cCI6MTY4Mjk2NjAwNywiaWF0IjoxNjY5ODI2MDA3fQ.k2atHTECwx7l5GcgzRIjIocpZuffYTwtycAQPxSvsSU");
            var response = await client.GetAsync(uriEndPoint);
            var local = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var resulTask = response.Content.ReadFromJsonAsync<ResponseType<List<SolicitudGeneralType>>>().Result.Data; //response.Content.ReadFromJsonAsync<ResponseType<string>>().Result;
                return (true, resulTask);
            }

        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error in {Method}", nameof(PostEndPoint));
            return (false, null);
        }
        return (false, null);
    }

   
    public async Task<(bool Success, object Data)> PostEndPoint(object request,string uriEndPoint,string nombreEndPoint)
    {
        try
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");
            var response = await client.PostAsJsonAsync(uriEndPoint, request);
            var local = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var resulTask = response.Content.ReadFromJsonAsync<ResponseType<string>>().Result;
                return (resulTask.Succeeded, resulTask);
            }

        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error in {Method}", nameof(PostEndPoint));
            return (false, null);
        }
        return (false, null);
    }

    public async Task<(bool Success, object Data)> PutEndPoint(object request, string uriEndPoint, string nombreEndPoint)
    {
        try
        {

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");
            var response = await client.PutAsJsonAsync(uriEndPoint, request);
            var local = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var resulTask = response.Content.ReadFromJsonAsync<ResponseType<string>>().Result;
                return (resulTask.Succeeded, resulTask);
            }

        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error in {Method}", nameof(PostEndPoint));
            return (false, null);
        }
        return (false, null);
    }

}
