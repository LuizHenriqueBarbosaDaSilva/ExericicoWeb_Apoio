using Apoio.Data;
using Apoio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Apoio.Controllers
{

    public class ApostaController : Controller
    {
        private readonly WebetContext _context;
        public ApostaController(WebetContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var apostaContext = _context.Apostas.Include(ap => ap.Apostador);
            return View(apostaContext.ToList());
        }

        public IActionResult Create()
        {
            ViewData["ApostadorId"] = new SelectList(_context.Apostadores,"Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Valor", "PrevisaoDeGanho", "Vencedora", "ApostadorId")] Aposta aposta)
        {
            if (!ModelState.IsValid) 
            {
                ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Nome", aposta.Id); 
                return View(aposta); 
            }
            if (aposta == null) { return NotFound(); }
            _context.Add(aposta);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aposta = await _context.Apostas.FindAsync(id);
            if (aposta == null)
            {
                return NotFound();
            }

            ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Name");
            return View(aposta);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valor,PrevisaoDeGanha,Vencedora,ApostadorId")] Aposta aposta)
        {
            if (id != aposta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aposta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApostaExists(aposta.Id))
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
            return View(aposta);
        }
        private bool ApostaExists(int id)
        {
            return _context.Apostas.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Apostas.Include(m => m.Apostador).FirstOrDefaultAsync(m => m.Id == id);
            if (details == null)
            {
                return NotFound();
            }

            ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Name");
            return View(details);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delete = await _context.Apostas.Include(m => m.Apostador).FirstOrDefaultAsync(m => m.Id == id);
            if (delete == null)
            {
                return NotFound();
            }

            return View(delete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delete = await _context.Apostas.FindAsync(id);
            if (delete != null)
            {
                _context.Apostas.Remove(delete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
