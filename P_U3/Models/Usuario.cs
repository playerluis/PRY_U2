using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using P_U3.Utils;

namespace P_U3.Models;

public class Usuario : IModeable
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
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; }

    [Required] 
    [StringLength(255)]
    [DisplayName("Contraseña")]
    public string Contrasenia { get; set; }

    
}