using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.EvalCore.Specifications;

public class GetRolesAccesoByCargoConvivenciaSpec : Specification<RolCargoSG>
{
    public GetRolesAccesoByCargoConvivenciaSpec(string codCargoConvivencia, string codScc, Guid uidCanal, string tipoCliente)
    {
        if (tipoCliente == "EJE")
        {
            Query.Where(rca => rca.CargoSG.Id == Guid.Parse(codCargoConvivencia) &&
                               rca.CargoSG.Departamento.Id == Guid.Parse(codScc) &&
                               rca.RolSG.CanalSGId == uidCanal &&
                               rca.Estado == "A")
                .Include(rca => rca.RolSG);
        }
        else
        {
            Query.Where(rca => rca.CargoSG.CodigoHomologacion == codCargoConvivencia &&
                               rca.CargoSG.Departamento.CodigoHomologacion == codScc &&
                               rca.RolSG.CanalSGId == uidCanal &&
                               rca.Estado == "A")
                .Include(rca => rca.RolSG);
        }



    }
}
