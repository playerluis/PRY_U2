using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using P_U3.Utils;

namespace P_U3.Models;

public class Doctor
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
    [StringLength(10)] 
    [DisplayName("Cédula")]
    public string Cedula { get; set; }

    [DisplayName("Especialidad")]
    public int EspecialidadId { get; set; }
    
    [DisplayName("Especialidad")]
    public Especialidad? Especialidad { get; set; }
        
}