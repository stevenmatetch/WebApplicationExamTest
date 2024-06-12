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
    public class ClassQueries(ExamTestDbContext context) : IClassQueries
    {
        private readonly ExamTestDbContext _context = context;

        public async Task<IEnumerable<ClassModel>> GetClasses()
        {
            return await _context.Class.ToListAsync();
        }

        public ClassModel GetClass(int id)
        {
            return _context.Class.Single(e => e.Id == id);
        }

        public bool ExamModelExists(int id)
        {
            return _context.Class.Any(e => e.Id == id);
        }

        public void EntityStateModified(ClassModel subjectModel)
        {
            _context.Entry(subjectModel).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Remove(ClassModel classModel)
        {
            _context.Class.Remove(classModel);
        }

        public void Add(ClassModel subjectModel)
        {
            _context.Class.Add(subjectModel);
        }
    }
}
