﻿using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Locacions.Dto;
using EvaluacionCore.Application.Features.Locacions.Specifications;
using EvaluacionCore.Application.Features.Localidads.Dto;
using EvaluacionCore.Domain.Entities.Asistencia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Queries.ConsultaLocalidadesXCoordinador;

public record ConsultaLocalidadesXCoordinadorAsyncQueries(string Identificacion) : IRequest<ResponseType<List<LocalidadXColaboradorType>>>;

public class ConsultaLocalidadesXCoordinadorAsyncQueriesHandler : IRequestHandler<ConsultaLocalidadesXCoordinadorAsyncQueries, ResponseType<List<LocalidadXColaboradorType>>>
{
    private readonly IRepositoryAsync<Localidad> _repositoryAsync;
    private readonly IRepositoryAsync<LocalidadColaborador> _repositoryLcAsync;
    private readonly IMapper _mapper;

    public ConsultaLocalidadesXCoordinadorAsyncQueriesHandler(IRepositoryAsync<Localidad> repository, IRepositoryAsync<LocalidadColaborador> repositoryLc, IMapper mapper)
    {
        _repositoryAsync = repository;
        _repositoryLcAsync = repositoryLc;
        _mapper = mapper;
    }

    public async Task<ResponseType<List<LocalidadXColaboradorType>>> Handle(ConsultaLocalidadesXCoordinadorAsyncQueries request, CancellationToken cancellationToken)
    {
        return new ResponseType<List<LocalidadXColaboradorType>>() { Data = null, Message = "Ocurrió un error durante la consulta", StatusCode = "002", Succeeded = false };
        /*try
        {
            var objLocalidad = await _repositoryAsync.ListAsync(cancellationToken);

            if (request.IdLocalidad is not null)
            {
                objLocalidad = objLocalidad.Where(x => x.Id == Guid.Parse(request.IdLocalidad) && x.Estado == "A").ToList();
            }
            else if (request.Identificacion is not null)
            {
                var localidadcolaborador = await _repositoryLcAsync.ListAsync(new GetLocationByColaboradorSpec(request.Identificacion), cancellationToken);
                objLocalidad = new List<Localidad>();

                foreach (var lc in localidadcolaborador)
                {
                    objLocalidad.Add(lc.Localidad);
                }
            }
            else
            {
                objLocalidad = objLocalidad.Where(x => x.Estado == "A").ToList();
            }

            return new ResponseType<List<LocalidadType>>() { Data = _mapper.Map<List<LocalidadType>>(objLocalidad), Message = "Consulta Generada exitosamente", StatusCode = "000", Succeeded = true };
        }
        catch (Exception e)
        {
            return new ResponseType<List<LocalidadType>>() { Data = null, Message = "Ocurrió un error durante la consulta", StatusCode = "002", Succeeded = false };
        }
    } */
    }

}