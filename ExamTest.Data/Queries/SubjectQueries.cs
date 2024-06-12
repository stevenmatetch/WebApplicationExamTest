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
    public class SubjectQueries(ExamTestDbContext context) : ISubjectQueries
    {
        private readonly ExamTestDbContext _context = context;

        public async Task<IEnumerable<SubjectModel>> GetSubjects()
        {
            return await _context.Subject.ToListAsync();
        }

        public SubjectModel GetSubject(int id)
        {
            return _context.Subject.Single(e => e.Id == id);
        }

        public bool SubjectModelExists(int id)
        {
            return _context.Subject.Any(e => e.Id == id);
        }

        public void EntityStateModified(SubjectModel subjectModel)
        {
            _context.Entry(subjectModel).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Remove(SubjectModel subjectModel)
        {
            _context.Subject.Remove(subjectModel);
        }

        public void Add(SubjectModel subjectModel)
        {
            _context.Subject.Add(subjectModel);
        }
    }
}

