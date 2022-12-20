namespace EvaluacionCore.Application.Features.Permisos.Dto;

public class SolicitudGeneralType
{
    public Guid Id { get; set; }
    public string IdFeature { get; set; }
    public string CodigoFeature { get; set; }
    public string IdTipoPermiso { get; set; }
    public string TipoPermisoNombre { get; set; }
    public Guid IdEstadoSolicitud { get; set; }
    public int IdSolicitante { get; set; }
    public int IdBeneficiario { get; set; }
    public string NombreEmpleado { get; set; }
    public string IdentificacionEmpleado { get; set; }
    public string Empresa { get; set; }
    public string Area { get; set; }
    public string Departamento { get; set; }
    public bool AplicaDescuento { get; set; }
    public DateTime FechaCreacion { get; set; }
}
