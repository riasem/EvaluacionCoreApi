using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Turnos.Dto;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace EvaluacionCore.Application.Features.Turnos.Queries.ConsultaColaboradoresXJefeAsync;

public record ConsultaColaboradoresXJefeAsyncQuery(string IdentificacionSesion) : IRequest<ResponseType<List<ColaboradorXJefeResponse>>>;

public class ConsultaColaboradoresXJefeAsyncQueryHandler : IRequestHandler<ConsultaColaboradoresXJefeAsyncQuery, ResponseType<List<ColaboradorXJefeResponse>>>
{
    private readonly IConfiguration _config;
    private readonly ILogger<ConsultaColaboradoresXJefeAsyncQueryHandler> _log;
    private string ConnectionString_Marc { get; }

    public ConsultaColaboradoresXJefeAsyncQueryHandler(IConfiguration config, ILogger<ConsultaColaboradoresXJefeAsyncQueryHandler> log)
    {
        _config = config;
        _log = log;
        ConnectionString_Marc = _config.GetConnectionString("DefaultConnection");
    }

    public async Task<ResponseType<List<ColaboradorXJefeResponse>>> Handle(ConsultaColaboradoresXJefeAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var connectionString = this.ConnectionString_Marc; //cadena de conexión a tu base de datos.

            var colaboradorXJefe = new List<ColaboradorXJefeResponse>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand comando = new SqlCommand("EAPP_SP_CONSULTA_COLABORADOR_COORDINADOR", con))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    var dt = new DataTable();
                    // string identificacionSesion
                    var parametro02 = comando.Parameters.AddWithValue("identificacionSesion", request.IdentificacionSesion);
                    parametro02.SqlDbType = SqlDbType.NVarChar;

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        String IdentificacionColaborador = reader["identificacionColaborador"].ToString()!; 
                        String ColaboradorId = reader["idColaborador"].ToString()!;
                        String NombreColaborador = reader["nombreColaborador"].ToString()!;
                        String LocalidadPrincipalColaboradorId = reader["idLocalidadPrincipalColaborador"].ToString()!;
                        String CodigoLocalidadPrincipalColaborador = reader["codigoLocalidadPrincipalColaborador"].ToString()!;
                        String NombreLocalidadPrincipalColaborador = reader["nombreLocalidadPrincipalColaborador"].ToString()!;
                        String JefeColaboradorId = reader["idJefeColaborador"].ToString()!;
                        String IdentificacionJefeColaborador = reader["identificacionJefeColaborador"].ToString()!;
                        String NombreJefeColaborador = reader["nombreJefeColaborador"].ToString()!;

                        Guid IdColaborador = new Guid();
                        if (ColaboradorId != String.Empty || ColaboradorId != null)
                        {
                            IdColaborador = Guid.Parse(ColaboradorId);
                        }

                        Guid IdLocalidadPrincipalColaborador = new Guid();
                        if (LocalidadPrincipalColaboradorId != String.Empty || LocalidadPrincipalColaboradorId != null)
                        {
                            IdLocalidadPrincipalColaborador = Guid.Parse(ColaboradorId);
                        }

                        Guid IdJefeColaborador = new Guid();
                        if (JefeColaboradorId != String.Empty || JefeColaboradorId != null)
                        {
                            IdJefeColaborador = Guid.Parse(ColaboradorId);
                        }
                        //
                        colaboradorXJefe.Add(new ColaboradorXJefeResponse()
                        {
                            IdentificacionColaborador = IdentificacionColaborador,
                            IdColaborador = IdColaborador,
                            NombreColaborador = NombreColaborador,
                            IdLocalidadPrincipalColaborador = IdLocalidadPrincipalColaborador,
                            CodigoLocalidadPrincipalColaborador = CodigoLocalidadPrincipalColaborador,
                            NombreLocalidadPrincipalColaborador = NombreLocalidadPrincipalColaborador,
                            IdJefeColaborador = IdJefeColaborador,
                            IdentificacionJefeColaborador = IdentificacionJefeColaborador,
                            NombreJefeColaborador = NombreJefeColaborador
                        });
                    }

                    reader.Close();
                }

                if (con.State == ConnectionState.Open) con.Close();
            }

            return new ResponseType<List<ColaboradorXJefeResponse>>() { Data = colaboradorXJefe, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
        }
        catch (SqlException sqlError)
        {
            // manejar sqlException
            throw sqlError;

        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<List<ColaboradorXJefeResponse>>() { Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002", Succeeded = false };

        }

    }
}