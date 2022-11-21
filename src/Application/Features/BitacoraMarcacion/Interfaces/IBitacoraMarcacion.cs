using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;

namespace EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces
{
    public interface IBitacoraMarcacion
    {
        Task<List<BitacoraMarcacionType>> GetBitacoraMarcacionAsync(GetBitacoraMarcacionRequest request);
    }
}