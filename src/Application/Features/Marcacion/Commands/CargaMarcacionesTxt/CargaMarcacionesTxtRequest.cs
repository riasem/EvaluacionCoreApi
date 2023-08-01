using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Features.Marcacion.Commands.CargaMarcacionesTxt;

public class CargaMarcacionesTxtRequest
{
    public string Id { get; set; }

    public DateTime FechaMarcacion { get; set; }

    public string Identificacion { get; set; }

    public string CodigoBiometrico { get; set; }

    public bool Sincronizado { get; set; }

    public string ImgBase { get; set; }

    public string TipoMarcacion { get; set; }

    public string IdentificacionDispositivo { get; set; }

}
