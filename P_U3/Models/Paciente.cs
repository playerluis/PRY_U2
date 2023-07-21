using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using P_U3.Utils;

namespace P_U3.Models;

public class Paciente : IModeable
{
    public int Id { get; set; }

    [Required] 
    [StringLength(255)] 
    [DisplayName("Nombre(s)")]
    public string Nombre { get; set; }

    [Required]
    [StringLength(255)] 
    [DisplayName("Apellido(s)")]
    public string Apellido { get; set; }

    [Required]
    [DisplayName("Fecha de Nacimiento")]
    [DataType(DataType.Date)]
    public DateOnly FechaDeNacimiento { get; set; }

    [Required]
    [StringLength(255)]
    [DisplayName("Dirección")]
    public string Direccion { get; set; }

}