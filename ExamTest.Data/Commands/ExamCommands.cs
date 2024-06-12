using ExamTest.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamTest.Data.Commands
{
    public class ExamCommands(ExamTestDbContext context)
    {
        private readonly ExamTestDbContext _context = context;


       /* public int AddExam(ExamAddDto exam, DateTime created)
        {
            var exam = ExamModel.
        }
       */
    }
}
