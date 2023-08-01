using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Licencia.Dto;

public class LicenciaResponseType
{
    public Guid IdLicenciaTercero { get; set; }

    public string TipoLicencia { get; set; }

    public DateTime FechaInicioSuscripcion  { get; set; }

    public DateTime FechaUltimaRenovacion { get; set; }

    public DateTime FechaProximaRenovacion { get; set; }

    public float CostoLicencia { get;set; }

    public string CodigoLicencia { get; set; }

    public string ReferenciaTecnica { get; set; }


}
