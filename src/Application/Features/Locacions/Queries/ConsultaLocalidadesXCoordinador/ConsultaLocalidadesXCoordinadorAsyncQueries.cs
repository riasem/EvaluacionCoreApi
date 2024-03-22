using AutoMapper;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Locacions.Dto;
using EvaluacionCore.Application.Features.Locacions.Specifications;
using EvaluacionCore.Application.Features.Localidads.Dto;
using EvaluacionCore.Application.Features.Turnos.Dto;
using EvaluacionCore.Application.Features.Turnos.Queries.ConsultaJefaturasXCoordinador;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Queries.ConsultaLocalidadesXCoordinador;

public record ConsultaLocalidadesXCoordinadorAsyncQueries(string IdentificacionSesion) : IRequest<ResponseType<List<LocalidadXColaboradorType>>>;

public class ConsultaLocalidadesXCoordinadorAsyncQueriesHandler : IRequestHandler<ConsultaLocalidadesXCoordinadorAsyncQueries, ResponseType<List<LocalidadXColaboradorType>>>
{
    private readonly IConfiguration _config;
    private readonly ILogger<ConsultaLocalidadesXCoordinadorAsyncQueriesHandler> _log;
    private string ConnectionString_Marc { get; }

    public ConsultaLocalidadesXCoordinadorAsyncQueriesHandler(IConfiguration config, ILogger<ConsultaLocalidadesXCoordinadorAsyncQueriesHandler> log)
    {
        _config = config;
        _log = log;
        ConnectionString_Marc = _config.GetConnectionString("DefaultConnection");
    }

    public async Task<ResponseType<List<LocalidadXColaboradorType>>> Handle(ConsultaLocalidadesXCoordinadorAsyncQueries request, CancellationToken cancellationToken)
    {
        try
        {
            var connectionString = this.ConnectionString_Marc; //cadena de conexión a tu base de datos.

            var localidadesXColaborador = new List<LocalidadXColaboradorType>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand comando = new SqlCommand("EAPP_SP_CONSULTA_LOCALIDAD_COORDINADOR", con))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    var dt = new DataTable();
                    // string IdentificacionSesion
                    var parametro02 = comando.Parameters.AddWithValue("identificacionSesion", request.IdentificacionSesion);
                    parametro02.SqlDbType = SqlDbType.NVarChar;

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        String IdentificacionLocalidad = reader["identificacionLocalidad"].ToString()!;
                        String CodigoLocalidad = reader["codigoLocalidad"].ToString()!;
                        String NombreLocalidad = reader["nombreLocalidad"].ToString()!;
                        Guid IdLocalidad = new Guid();

                        if (IdentificacionLocalidad != String.Empty || IdentificacionLocalidad != null)
                        {
                            IdLocalidad = Guid.Parse(IdentificacionLocalidad);
                        }
                        localidadesXColaborador.Add(new LocalidadXColaboradorType()
                        {
                            IdentificacionLocalidad = IdLocalidad,
                            CodigoLocalidad = CodigoLocalidad,
                            NombreLocalidad = NombreLocalidad
                        });
                    }

                    reader.Close();
                }

                if (con.State == ConnectionState.Open) con.Close();
            }

            return new ResponseType<List<LocalidadXColaboradorType>>() { Data = localidadesXColaborador, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };
        }
        catch (SqlException sqlError)
        {
            // manejar sqlException
            throw sqlError;

        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<List<LocalidadXColaboradorType>>() { Message = CodeMessageResponse.GetMessageByCode("002"), StatusCode = "002", Succeeded = false };

        }
    }

}