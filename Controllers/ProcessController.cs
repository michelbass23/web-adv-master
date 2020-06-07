using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EscAdv.Data;
using EscAdv.Models;

namespace EscAdv.Controllers
{
  public class ProcessController : Controller
  {
    private readonly EscAdvContext _context;

    public ProcessController(EscAdvContext context)
    {
      _context = context;
    }

    // GET: Process
    // public async Task<IActionResult> Index()
    // {
    //   return View(await _context.Process.ToListAsync());
    // }
    public async Task<IActionResult> Index(string search)
    {
      var process = from m in _context.Process
                   select m;

      if (!String.IsNullOrEmpty(search))
      {
        process = process.Where(s => s.type.ToUpper().Contains(search.ToUpper()));
      }

      return View(await process.ToListAsync());
    }

    // GET: Process/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var process = await _context.Process
          .FirstOrDefaultAsync(m => m.id == id);
      if (process == null)
      {
        return NotFound();
      }

      return View(process);
    }

    // GET: Process/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Process/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,title,type,petition,created")] Process process)
    {
      if (ModelState.IsValid)
      {
        _context.Add(process);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(process);
    }

    // GET: Process/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var process = await _context.Process.FindAsync(id);
      if (process == null)
      {
        return NotFound();
      }
      return View(process);
    }

    // POST: Process/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("id,title,type,petition,created")] Process process)
    {
      if (id != process.id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(process);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ProcessExists(process.id))
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
      return View(process);
    }

    // GET: Process/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var process = await _context.Process
          .FirstOrDefaultAsync(m => m.id == id);
      if (process == null)
      {
        return NotFound();
      }

      return View(process);
    }

    // POST: Process/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var process = await _context.Process.FindAsync(id);
      _context.Process.Remove(process);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ProcessExists(int id)
    {
      return _context.Process.Any(e => e.id == id);
    }
  }
}
