using ExamTest.Data.Context;
using ExamTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTest.Data.Queries
{
    public class AnswerQueries(ExamTestDbContext context) : IAnswerQueries
    {
        private readonly ExamTestDbContext _context = context;

        public async Task<IEnumerable<AnswerModel>> GetAnswers()
        {
            return await _context.Answer.ToListAsync();
        }

        public AnswerModel GetAnswer(int id)
        {
            return _context.Answer.Single(e => e.Id == id);
        }

        public bool ExamModelExists(int id)
        {
            return _context.Answer.Any(e => e.Id == id);
        }

        public void EntityStateModified(AnswerModel subjectModel)
        {
            _context.Entry(subjectModel).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Remove(AnswerModel subjectModel)
        {
            _context.Answer.Remove(subjectModel);
        }

        public void Add(AnswerModel subjectModel)
        {
            _context.Answer.Add(subjectModel);
        }
    }
}
