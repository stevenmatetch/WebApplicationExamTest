using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationExamTest.Data;
using WebApplicationExamTest.Models;

namespace WebApplicationExamTest.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public SubjectController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subject.ToListAsync());
        }

        public async Task<IActionResult> Index1(string SetId)
        {
            //Session["cSessionStrP"] = SetId;
            TempData["StudenId"] = SetId;
            return View(await _context.Subject.ToListAsync());
            //heu
        }

        public async Task<IActionResult> ViewAnswer(int? SetSubjectId)
        {
            TempData["SetSubjectId"] = SetSubjectId;
            //string cSessionStrP = Session["cSessionStrP"] as string;
            try
            {
                var studentId = TempData["StudenId"].ToString();
                var x = await _context.Answer.Where(answer => answer.SubjectId == SetSubjectId && answer.StudentId == studentId).ToListAsync();
                return View(x);
            }
            catch(Exception ex)
            {
                List<Answer> list = new List<Answer>();     
                return View(list);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewAnswer([Bind("StudentAnswer,Comment,Mark,StudentId,SubjectId")] Answer answer, string StudentId, string StudentAnswer)
        {
          
            if (ModelState.IsValid)
            {             
                CorrectedExam correctedExam = new CorrectedExam();
                correctedExam.StudentId = StudentId;
                correctedExam.StudentAnswer = answer.StudentAnswer;
                correctedExam.SubjectId = answer.SubjectId;
                correctedExam.StudentAnswer = StudentAnswer;
                correctedExam.Mark = answer.Mark;
                correctedExam.Comment = answer.Comment;
                _context.CorrectedExam.Add(correctedExam);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return Redirect("/Class");

        }


        public async Task<IActionResult> ViewCorrectedExam(int? SetSubjectId)
        {
            TempData["SetSubjectId"] = SetSubjectId;
            //string cSessionStrP = Session["cSessionStrP"] as string;
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var x = await _context.CorrectedExam.Where(correctedExam => correctedExam.SubjectId == SetSubjectId && correctedExam.StudentId == user.Id).ToListAsync();
                return View(x);
            }
            catch (Exception ex)
            {
                List<Answer> list = new List<Answer>();
                return View(list);
            }

        }

        public async Task<IActionResult> ViewAllCorrectedExam()
        {
            var user = await _userManager.GetUserAsync(User);

            var x = await _context.CorrectedExam.Where(correctedExam =>correctedExam.StudentId == user.Id).ToListAsync();

            return View(x);
      
        }


        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,SubejectId")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                //Exam exam = new Exam();
                //exam.SubjectId = subject.Id;
                //List<Exam> exams = new List<Exam>();
                
                //exams.Where(x => x.Id == subject.Id).ToList();
                

                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Mark")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
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
            return View(subject);
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subject.FindAsync(id);
            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _context.Subject.Any(e => e.Id == id);
        }
    }
}
