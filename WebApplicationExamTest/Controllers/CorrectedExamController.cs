using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationExamTest.Data;
using WebApplicationExamTest.Models;

namespace WebApplicationExamTest.Controllers
{
    public class CorrectedExamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CorrectedExamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CorrectedExam
        public async Task<IActionResult> Index()
        {
            return View(await _context.CorrectedExam.ToListAsync());
        }

        // GET: CorrectedExam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correctedExam = await _context.CorrectedExam
                .FirstOrDefaultAsync(m => m.Id == id);
            if (correctedExam == null)
            {
                return NotFound();
            }

            return View(correctedExam);
        }

        // GET: CorrectedExam/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CorrectedExam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,StudentAnswer,Mark,SubjectId,StudentId")] CorrectedExam correctedExam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correctedExam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(correctedExam);
        }

        // GET: CorrectedExam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correctedExam = await _context.CorrectedExam.FindAsync(id);
            if (correctedExam == null)
            {
                return NotFound();
            }
            return View(correctedExam);
        }

        // POST: CorrectedExam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,StudentAnswer,Mark,SubjectId,StudentId")] CorrectedExam correctedExam)
        {
            if (id != correctedExam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correctedExam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorrectedExamExists(correctedExam.Id))
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
            return View(correctedExam);
        }

        // GET: CorrectedExam/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correctedExam = await _context.CorrectedExam
                .FirstOrDefaultAsync(m => m.Id == id);
            if (correctedExam == null)
            {
                return NotFound();
            }

            return View(correctedExam);
        }

        // POST: CorrectedExam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var correctedExam = await _context.CorrectedExam.FindAsync(id);
            _context.CorrectedExam.Remove(correctedExam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorrectedExamExists(int id)
        {
            return _context.CorrectedExam.Any(e => e.Id == id);
        }
    }
}
