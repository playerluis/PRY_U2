using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P_U3.Context;
using P_U3.Models;

namespace P_U3.Controllers;

[Authorize]
public class EspecialidadController : Controller
{
    private readonly BddCitasMedicasContext _context;

    public EspecialidadController(BddCitasMedicasContext context)
    {
        _context = context;
    }

    // GET: Especialidads
    public async Task<IActionResult> Index()
    {
        return View(await _context.Especialidad.ToListAsync());
    }

    // GET: Especialidads/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();
        
        var especialidad = await _context.Especialidad
            .FirstOrDefaultAsync(m => m.Id == id);
        if (especialidad == null)
            return NotFound();
        

        return View(especialidad);
    }

    // GET: Especialidads/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Especialidads/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre")] Especialidad especialidad)
    {
        if (!ModelState.IsValid) return View(especialidad);
        _context.Add(especialidad);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Especialidads/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();
        
        var especialidad = await _context.Especialidad.FindAsync(id);
        if (especialidad == null)
            return NotFound();
        
        return View(especialidad);
    }

    // POST: Especialidads/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Especialidad especialidad)
    {
        if (id != especialidad.Id)
            return NotFound();
        
        if (!ModelState.IsValid) return View(especialidad);
        try
        {
            _context.Update(especialidad);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EspecialidadExists(especialidad.Id))
                return NotFound();
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Especialidads/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var especialidad = await _context.Especialidad
            .FirstOrDefaultAsync(m => m.Id == id);
        if (especialidad == null)
            return NotFound();
        
        return View(especialidad);
    }

    // POST: Especialidads/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var especialidad = await _context.Especialidad.FindAsync(id);
        if (especialidad != null)
        {
            _context.Especialidad.Remove(especialidad);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EspecialidadExists(int id)
    {
        return _context.Especialidad.Any(e => e.Id == id);
    }
}