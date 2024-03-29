﻿using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetBitacoraMarcacionCapacidadesEspeciales;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Commands.GetComboBitacoraMarcacion;
using EvaluacionCore.Application.Features.BitacoraMarcacion.Dto;

namespace EvaluacionCore.Application.Features.BitacoraMarcacion.Interfaces
{
    public interface IBitacoraMarcacion
    {
        Task<List<BitacoraMarcacionType>> GetBitacoraMarcacionAsync(GetBitacoraMarcacionRequest request);
        Task<List<ComboBitacoraMarcacionType>> GetComboBitacoraMarcacionAsync(GetComboBitacoraMarcacionRequest request);
        Task<ResponseType<List<BitacoraMarcacionType>>> GetBitacoraMarcacionCapacidadesEspecialesAsync(GetBitacoraMarcacionCapacidadesEspecialesRequest request);
    }
}