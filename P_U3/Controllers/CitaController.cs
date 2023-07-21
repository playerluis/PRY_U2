using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P_U3.Context;
using P_U3.Models;

namespace P_U3.Controllers;

[Authorize]
public class CitaController : Controller
{
    private readonly BddCitasMedicasContext _context;

    public CitaController(BddCitasMedicasContext context)
    {
        _context = context;
    }

    // GET: Citas
    public async Task<IActionResult> Index()
    {
        var asdContext = _context.Cita
            .Include(c => c.Doctor)
            .Include(c => c.Paciente);
        return View(await asdContext.ToListAsync());
    }

    // GET: Citas/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();


        var cita = await _context.Cita
            .Include(c => c.Doctor)
            .Include(c => c.Paciente)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cita == null)
            return NotFound();


        return View(cita);
    }

    // GET: Citas/Create
    public IActionResult Create()
    {
        ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "Apellido");
        ViewData["PacienteId"] = new SelectList(_context.Set<Paciente>(), "Id", "Apellido");
        return View();
    }

    // POST: Citas/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Fecha,HoraDeInicio,Horas,DoctorId,PacienteId")] Cita cita)
    {
        var fechaActual = DateTime.Now;
        var fechaCita = cita.Fecha.ToDateTime(cita.HoraDeInicio);

        if (fechaCita < fechaActual)
            ModelState.AddModelError("Fecha", "La fecha de la cita no puede ser menor a la fecha actual");

        var fechasUsadas = await _context.Cita
            .Where(c => c.DoctorId == cita.DoctorId)
            .Where(c => c.Fecha == cita.Fecha)
            .Where(c => c.HoraDeInicio <= cita.HoraDeInicio.AddHours(cita.Horas) && cita.HoraDeInicio >= c.HoraDeInicio)
            .ToListAsync();

        if (fechasUsadas.Count > 0)
            ModelState.AddModelError("Fecha", "El doctor ya tiene una cita en ese horario");

        if (ModelState.IsValid)
        {
            _context.Add(cita);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "Apellido", cita.DoctorId);
        ViewData["PacienteId"] = new SelectList(_context.Set<Paciente>(), "Id", "Apellido", cita.PacienteId);
        return View(cita);
    }

    // GET: Citas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();


        var cita = await _context.Cita.FindAsync(id);
        if (cita == null)
            return NotFound();

        ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "Apellido", cita.DoctorId);
        ViewData["PacienteId"] = new SelectList(_context.Set<Paciente>(), "Id", "Apellido", cita.PacienteId);
        return View(cita);
    }

    // POST: Citas/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Fecha,HoraDeInicio,Horas,DoctorId,PacienteId")]
        Cita cita)
    {
        if (id != cita.Id)
            return NotFound();
        
        var fechaActual = DateTime.Now;
        var fechaCita = cita.Fecha.ToDateTime(cita.HoraDeInicio);

        if (fechaCita < fechaActual)
            ModelState.AddModelError("Fecha", "La fecha de la cita no puede ser menor a la fecha actual");

        var fechasUsadas = await _context.Cita
            .Where(c => c.Id != cita.Id)
            .Where(c => c.DoctorId == cita.DoctorId)
            .Where(c => c.Fecha == cita.Fecha)
            .Where(c => c.HoraDeInicio <= cita.HoraDeInicio.AddHours(cita.Horas) && cita.HoraDeInicio >= c.HoraDeInicio)
            .ToListAsync();

        if (fechasUsadas.Count > 0)
            ModelState.AddModelError("Fecha", "El doctor ya tiene una cita en ese horario");
        
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cita);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(cita.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["DoctorId"] = new SelectList(_context.Set<Doctor>(), "Id", "Apellido", cita.DoctorId);
        ViewData["PacienteId"] = new SelectList(_context.Set<Paciente>(), "Id", "Apellido", cita.PacienteId);
        return View(cita);
    }

    // GET: Citas/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();


        var cita = await _context.Cita
            .Include(c => c.Doctor)
            .Include(c => c.Paciente)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cita == null)
            return NotFound();


        return View(cita);
    }

    // POST: Citas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cita = await _context.Cita.FindAsync(id);
        if (cita != null)
            _context.Cita.Remove(cita);


        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CitaExists(int id)
    {
        return _context.Cita.Any(e => e.Id == id);
    }
}