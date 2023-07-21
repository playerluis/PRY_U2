using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace P_U3.Models;

public class Cita
{
    public int Id { get; set; }

    [Required]
    [DisplayName("Fecha de la cita")]
    [DataType(DataType.Date)]
    public DateOnly Fecha { get; set; }
    
    [Required]
    [DisplayName("Hora de Inicio")]
    [DataType(DataType.Time)]
    public TimeOnly HoraDeInicio { get; set; }

    [Required]
    [DisplayName("Duración en horas")]
    public int Horas { get; set; }

    [DisplayName("Doctor")] public int DoctorId { get; set; }

    [DisplayName("Doctor")] public Doctor? Doctor { get; set; }

    [DisplayName("Paciente")] public int PacienteId { get; set; }

    [DisplayName("Paciente")] public Paciente? Paciente { get; set; }
}