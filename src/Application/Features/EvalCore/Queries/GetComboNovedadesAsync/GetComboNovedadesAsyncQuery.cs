using EvaluacionCore.Application.Common.Wrappers;
using EvaluacionCore.Application.Features.Permisos.Dto;
using MediatR;

namespace EvaluacionCore.Application.Features.EvalCore.Queries.GetEvaluacionAsistenciaAsync;

public record GetComboNovedadesAsyncQuery() : IRequest<ResponseType<List<ComboNovedadType>>>;

public class GetComboNovedadesAsyncHandler : IRequestHandler<GetComboNovedadesAsyncQuery, ResponseType<List<ComboNovedadType>>>
{
    

    async Task<ResponseType<List<ComboNovedadType>>> IRequestHandler<GetComboNovedadesAsyncQuery, ResponseType<List<ComboNovedadType>>>.Handle(GetComboNovedadesAsyncQuery request, CancellationToken cancellationToken)
    {
        List<ComboNovedadType> objResult = new() { };

        objResult.Add(new ComboNovedadType() { Codigo = "NT" ,Descripcion = "NO TIENE TURNO ASIGNADO" }  );
        objResult.Add(new ComboNovedadType() { Codigo = "NS", Descripcion = "SIN REGISTRO DE SALIDA Y SIN TURNO ASIGNADO" });
        objResult.Add(new ComboNovedadType() { Codigo = "AI" ,Descripcion = "ATRASO INJUSTIFICADO"});
        objResult.Add(new ComboNovedadType() { Codigo = "AJ" ,Descripcion = "ATRASO JUSTIFICADO"});
        objResult.Add(new ComboNovedadType() { Codigo = "SI" ,Descripcion = "SALIDA ANTICIPADA INJUSTIFICADA"});
        objResult.Add(new ComboNovedadType() { Codigo = "SJ" ,Descripcion = "SALIDA ANTICIPADA JUSTIFICADA"});
        objResult.Add(new ComboNovedadType() { Codigo = "ER" ,Descripcion = "EXCESO DE RECESO"});
        objResult.Add(new ComboNovedadType() { Codigo = "NR" ,Descripcion = "SIN REGISTRO DE RETORNO DE RECESO"});
        objResult.Add(new ComboNovedadType() { Codigo = "FJ" ,Descripcion = "FALTA JUSTIFICADA"});
        objResult.Add(new ComboNovedadType() { Codigo = "FI" ,Descripcion = "FALTA INJUSTIFICADA"});
        


        return new ResponseType<List<ComboNovedadType>>() { Data = objResult, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        
    }

}