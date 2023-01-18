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

        objResult.Add(new ComboNovedadType() { CodigoNovedad = "NT" ,DescripcionNovedad = "NO TIENE TURNO ASIGNADO" }  );
        objResult.Add(new ComboNovedadType() { CodigoNovedad = "NS", DescripcionNovedad = "SIN REGISTRO DE SALIDA Y SIN TURNO ASIGNADO" });
        objResult.Add(new ComboNovedadType() { CodigoNovedad = "AI" ,DescripcionNovedad = "ATRASO INJUSTIFICADO"});
        objResult.Add(new ComboNovedadType() { CodigoNovedad = "AJ" ,DescripcionNovedad = "ATRASO JUSTIFICADO"});
        objResult.Add(new ComboNovedadType() { CodigoNovedad = "SI" ,DescripcionNovedad = "SALIDA ANTICIPADA INJUSTIFICADA"});
        objResult.Add(new ComboNovedadType() { CodigoNovedad = "SJ" ,DescripcionNovedad = "SALIDA ANTICIPADA JUSTIFICADA"});
        objResult.Add(new ComboNovedadType() { CodigoNovedad = "ER" ,DescripcionNovedad = "EXCESO DE RECESO"});
        objResult.Add(new ComboNovedadType() { CodigoNovedad = "NR" ,DescripcionNovedad = "SIN REGISTRO DE RETORNO DE RECESO"});
        objResult.Add(new ComboNovedadType() { CodigoNovedad = "FJ" ,DescripcionNovedad = "FALTA JUSTIFICADA"});
        objResult.Add(new ComboNovedadType() { CodigoNovedad = "FI" ,DescripcionNovedad = "FALTA INJUSTIFICADA"});
        


        return new ResponseType<List<ComboNovedadType>>() { Data = objResult, Succeeded = true, StatusCode = "000", Message = "Consulta generada exitosamente" };
        
    }

}