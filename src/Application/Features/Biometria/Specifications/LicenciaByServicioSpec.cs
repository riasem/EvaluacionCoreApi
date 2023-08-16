using Ardalis.Specification;
using EvaluacionCore.Domain.Entities.Common;
using EvaluacionCore.Domain.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Biometria.Specifications;

public class LicenciaByServicioSpec : Specification<LicenciaTerceroSG>
{

    public LicenciaByServicioSpec(Guid tipoLicencia)
    {
        Query.Where(x => x.IdLicenciaTercero == tipoLicencia && x.Estado == "ACTIVO");
    }

}
