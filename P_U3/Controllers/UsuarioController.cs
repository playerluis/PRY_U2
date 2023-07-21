using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P_U3.Context;
using P_U3.Models;

namespace P_U3.Controllers;

[Authorize]
public class UsuarioController : Controller
{
    private readonly BddCitasMedicasContext _context;

    public UsuarioController(BddCitasMedicasContext context)
    {
        _context = context;
    }

    // GET: Usuario
    public async Task<IActionResult> Index()
    {
        return View(await _context.Usuario.ToListAsync());
    }

    // GET: Usuario/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();
        

        var usuario = await _context.Usuario
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
            return NotFound();
        

        return View(usuario);
    }

    // GET: Usuario/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Usuario/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Contrasenia")] Usuario usuario)
    {
        if (!ModelState.IsValid) return View(usuario);
        _context.Add(usuario);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Usuario/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();


        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null)
            return NotFound();

        return View(usuario);
    }

    // POST: Usuario/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,Contrasenia")] Usuario usuario)
    {
        if (id != usuario.Id)
            return NotFound();
        

        if (!ModelState.IsValid) return View(usuario);
        try
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(usuario.Id))
                return NotFound();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Usuario/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();
        
        var usuario = await _context.Usuario
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
            return NotFound();
        
        return View(usuario);
    }

    // POST: Usuario/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario != null)
            _context.Usuario.Remove(usuario);
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(int id)
    {
        return _context.Usuario.Any(e => e.Id == id);
    }
}