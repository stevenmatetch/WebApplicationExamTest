using ExamTest.Data.Context;
using ExamTest.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamTest.Data.Queries
{
    public class ExamQueries(ExamTestDbContext context) : IExamQueries
    {
        private readonly ExamTestDbContext _context = context;

        public async Task<IEnumerable<ExamModel>> GetExams()
        {
            return await _context.Exam.ToListAsync();
        }

        public ExamModel GetExam(int id)
        {
            return _context.Exam.Single(e => e.Id == id);
        }

        public bool ExamModelExists(int id)
        {
            return _context.Exam.Any(e => e.Id == id);
        }

        public void EntityStateModified(ExamModel examModel)
        {
            _context.Entry(examModel).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Remove(ExamModel examModel)
        {
            _context.Exam.Remove(examModel);
        }

        public void Add(ExamModel examModel)
        {
            _context.Exam.Add(examModel);
        }
    }
}
