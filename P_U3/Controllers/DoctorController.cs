using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P_U3.Context;
using P_U3.Models;

namespace P_U3.Controllers;

[Authorize]
public class DoctorController : Controller
{
    private readonly BddCitasMedicasContext _context;

    public DoctorController(BddCitasMedicasContext context)
    {
        _context = context;
    }

    // GET: Doctor
    public async Task<IActionResult> Index()
    {
        var asdContext = _context.Doctor.Include(d => d.Especialidad);
        return View(await asdContext.ToListAsync());
    }

    // GET: Doctor/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctor
            .Include(d => d.Especialidad)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    // GET: Doctor/Create
    public IActionResult Create()
    {
        ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Nombre");
        return View();
    }

    // POST: Doctor/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Cedula,EspecialidadId")] Doctor doctor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
        {
            Console.WriteLine(error.ErrorMessage);
        }

        ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Nombre", doctor.EspecialidadId);
        return View(doctor);
    }

    // GET: Doctor/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctor.FindAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Nombre", doctor.EspecialidadId);
        return View(doctor);
    }

    // POST: Doctor/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Cedula,EspecialidadId")] Doctor doctor)
    {
        if (id != doctor.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(doctor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(doctor.Id))
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
        ViewData["EspecialidadId"] = new SelectList(_context.Especialidad, "Id", "Nombre", doctor.EspecialidadId);
        return View(doctor);
    }

    // GET: Doctor/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctor
            .Include(d => d.Especialidad)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    // POST: Doctor/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var doctor = await _context.Doctor.FindAsync(id);
        if (doctor != null)
        {
            _context.Doctor.Remove(doctor);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DoctorExists(int id)
    {
        return _context.Doctor.Any(e => e.Id == id);
    }
}