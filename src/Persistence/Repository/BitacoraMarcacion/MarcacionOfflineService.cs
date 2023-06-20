using AutoMapper;
using EvaluacionCore.Application.Common.Exceptions;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Marcacion.Commands.CreateCabeceraLog;
using EvaluacionCore.Application.Features.Marcacion.Interfaces;
using EvaluacionCore.Domain.Entities.Marcaciones;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Persistence.Repository.BitacoraMarcacion;


public class MarcacionOfflineService : IMarcacionOffline
{
    private readonly ILogger<MarcacionOfflineService> _log;

    private readonly IRepositoryGRiasemAsync<AccLogMarcacionOffline> _repoLogMarcOffline;
    private readonly IMapper _mapper;


    public MarcacionOfflineService(ILogger<MarcacionOfflineService> log, IRepositoryGRiasemAsync<AccLogMarcacionOffline> repoLogMarcOffline,
        IMapper mapper)
    {
        _log = log;
        _repoLogMarcOffline = repoLogMarcOffline;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> CreateCabeceraLogOffline(List<CreateCabeceraLogRequest> Request, string IdentificacionSesion, CancellationToken cancellationToken)
    {
        var lstCabeceraOffline = _mapper.Map<List<AccLogMarcacionOffline>>(Request);

        lstCabeceraOffline.ForEach(item => { item.UsuarioCreacion = IdentificacionSesion; item.FechaCreacion = DateTime.Now; });

        var result = await _repoLogMarcOffline.AddRangeAsync(lstCabeceraOffline,cancellationToken);

        if (!result.Any())
        {
            return new ResponseType<string> { Message = CodeMessageResponse.GetMessageByCode("100", "Cabecera"), StatusCode = "101", Succeeded = true };
        }

        return new ResponseType<string> { Message = CodeMessageResponse.GetMessageByCode("100", "Cabecera"), StatusCode = "100", Succeeded = true };
        
    }
}
