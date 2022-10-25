using AutoMapper;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Locacions.Commands.DeleteLocation;

public record DeleteLocacionCommand(string IdLocacion) : IRequest<ResponseType<string>>;

public class DeleteLocacionCommandHandler : IRequestHandler<DeleteLocacionCommand,ResponseType<string>>
{
    private readonly IRepositoryAsync<Locacion> _repoLocacionAsync;
    private readonly IMapper _mapper;
    public DeleteLocacionCommandHandler(IRepositoryAsync<Locacion> repoLocacionAsync, IMapper mapper)
    {
        _repoLocacionAsync = repoLocacionAsync;
        _mapper = mapper;
    }

    public async Task<ResponseType<string>> Handle(DeleteLocacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var objLocacion = await _repoLocacionAsync.GetByIdAsync(Guid.Parse(request.IdLocacion), cancellationToken);
            if (objLocacion is null)
            {
                return new ResponseType<string>() { Data = null, Message = "No existe la locación que desea eliminar", StatusCode = "999", Succeeded = false };
            }
            objLocacion.Estado = "I";
            await _repoLocacionAsync.UpdateAsync(objLocacion, cancellationToken);

            return new ResponseType<string>() { Data = null, Message = "Locación eliminada exitosamente", StatusCode = "000", Succeeded = true };

        }
        catch (Exception ex)
        {

            return new ResponseType<string>() { Data = null, Message = ex.Message, StatusCode = "999", Succeeded = false };
        }


        
    }
}
