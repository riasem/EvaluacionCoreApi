namespace WorkFlow.Domain.Entities.Vacaciones
{
    public class PeriodoVacacion
    {
        public int Periodo { get; set; } 

        public DateTime? InicioVacacion { get; set; } 

        public DateTime? FinVacacion { get; set; }

        public int? DiasVacaciones { get; set; }

        public int? DiasTomados { get; set; }

        public string NombreEmpleado { get; set; }
    }
}
