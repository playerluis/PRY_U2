using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P_U3.Context;
using P_U3.Models;

namespace P_U3.Controllers;

[Authorize]
public class PacienteController : Controller
{
    private readonly BddCitasMedicasContext _context;

    public PacienteController(BddCitasMedicasContext context)
    {
        _context = context;
    }

    // GET: Pacientes
    public async Task<IActionResult> Index()
    {
        return View(await _context.Paciente.ToListAsync());
    }

    // GET: Pacientes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var paciente = await _context.Paciente
            .FirstOrDefaultAsync(m => m.Id == id);
        if (paciente == null)
        {
            return NotFound();
        }

        return View(paciente);
    }

    // GET: Pacientes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Pacientes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,FechaDeNacimiento,Direccion,FechaDeRegistro")] Paciente paciente)
    {
        if (ModelState.IsValid)
        {
            _context.Add(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(paciente);
    }

    // GET: Pacientes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var paciente = await _context.Paciente.FindAsync(id);
        if (paciente == null)
        {
            return NotFound();
        }
        return View(paciente);
    }

    // POST: Pacientes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,FechaDeNacimiento,Direccion,FechaDeRegistro")] Paciente paciente)
    {
        if (id != paciente.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(paciente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(paciente.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(paciente);
    }

    // GET: Pacientes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var paciente = await _context.Paciente
            .FirstOrDefaultAsync(m => m.Id == id);
        if (paciente == null)
        {
            return NotFound();
        }

        return View(paciente);
    }

    // POST: Pacientes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var paciente = await _context.Paciente.FindAsync(id);
        if (paciente != null)
        {
            _context.Paciente.Remove(paciente);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PacienteExists(int id)
    {
        return _context.Paciente.Any(e => e.Id == id);
    }
}