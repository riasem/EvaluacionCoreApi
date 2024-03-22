using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Dto;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace EvaluacionCore.Application.Features.Turnos.Queries.ConsultaJefaturasXCoordinador;

public record ConsultaJefaturasXCoordinadorQueries(JefaturasXCoordinadorRequest request, string IdentificacionSesion) : IRequest<ResponseType<List<JefaturasXCoordinadorResponse>>>;
public class ConsultaJefaturasXCoordinadorQueriesHandler : IRequestHandler<ConsultaJefaturasXCoordinadorQueries, ResponseType<List<JefaturasXCoordinadorResponse>>>
{
    private readonly IConfiguration _config;
    private readonly ILogger<ConsultaJefaturasXCoordinadorQueriesHandler> _log;
    private string ConnectionString_Marc { get; }

    public ConsultaJefaturasXCoordinadorQueriesHandler(IConfiguration config, ILogger<ConsultaJefaturasXCoordinadorQueriesHandler> log)
    {
        _config = config;
        _log = log;
        ConnectionString_Marc = _config.GetConnectionString("DefaultConnection");
    }

    public async Task<ResponseType<List<JefaturasXCoordinadorResponse>>> Handle(ConsultaJefaturasXCoordinadorQueries request, CancellationToken cancellationToken)
    {
        try
        {
            var connectionString = this.ConnectionString_Marc; //cadena de conexión a tu base de datos.

            var jefaturasXCoordinador = new List<JefaturasXCoordinadorResponse>();
             
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand comando = new SqlCommand("EAPP_SP_CONSULTA_JEFATURA_COORDINADOR", con))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    var dt = new DataTable();
                    // string IdentificacionSesion, DateTime fecha
                    var parametro02 = comando.Parameters.AddWithValue("identificacionSesion", request.IdentificacionSesion);
                    parametro02.SqlDbType = SqlDbType.NVarChar;
                    var parametro03 = comando.Parameters.AddWithValue("fecha", request.request.Fecha?.ToString("yyyy/MM/dd")!);
                    parametro03.SqlDbType = SqlDbType.NVarChar;

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        String IdColaboradorCoordinador = reader["idColaboradorCoordinador"].ToString()!;
                        String NombreColaboradorCoordinador = reader["nombreColaboradorCoordinador"].ToString()!;
                        String IdentificacionColaboradorJefe = reader["identificacionColaboradorJefe"].ToString()!;
                        String IdColaboradorJefe = reader["idColaboradorJefe"].ToString()!;
                        String NombreColaboradorJefe = reader["nombreColaboradorJefe"].ToString()!;
                        //
                        jefaturasXCoordinador.Add(new JefaturasXCoordinadorResponse()
                        {
                            IdColaboradorCoordinador = IdColaboradorCoordinador,
                            NombreColaboradorCoordinador = NombreColaboradorCoordinador,
                            IdentificacionColaboradorJefe = IdentificacionColaboradorJefe,
                            IdColaboradorJefe = IdColaboradorJefe,
                            NombreColaboradorJefe = NombreColaboradorJefe
                        });
                    }

                    reader.Close();
                }

                if (con.State == ConnectionState.Open) con.Close();
            }

            return new ResponseType<List<JefaturasXCoordinadorResponse>>() { Data = jefaturasXCoordinador, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
        }
        catch (SqlException sqlError)
        {
            // manejar sqlException
            throw sqlError;

        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<List<JefaturasXCoordinadorResponse>>() { Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002", Succeeded = false };

        }

    }
}