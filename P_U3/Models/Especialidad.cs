using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using P_U3.Utils;

namespace P_U3.Models;

public class Especialidad : IModeable
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    [DisplayName("Nombre")]
    public string Nombre { get; set; }
}