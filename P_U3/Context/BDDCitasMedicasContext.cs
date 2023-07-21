using Microsoft.EntityFrameworkCore;
using P_U3.Models;

namespace P_U3.Context;

public class BddCitasMedicasContext : DbContext
{
    public DbSet<Especialidad> Especialidad { get; set; } = null!;
    public DbSet<Doctor> Doctor { get; set; } = null!;
    public DbSet<Paciente> Paciente { get; set; } = null!;
    public DbSet<Cita> Cita { get; set; } = null!;
    public DbSet<Usuario> Usuario { get; set; } = null!;


    public BddCitasMedicasContext(DbContextOptions<BddCitasMedicasContext> options) : base(options)
    {
    }
}