using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace P_U3.Models;

public class LoginModel
{
    [Required (ErrorMessage = "El email es requerido")]
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; }

    [Required (ErrorMessage = "La contraseña es requerida")] 
    [StringLength(255)]
    [DisplayName("Contraseña")]
    public string Contrasenia { get; set; }
}