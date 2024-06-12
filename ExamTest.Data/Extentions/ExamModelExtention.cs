using ExamTest.Data.Models;
using ExamTest.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamTest.Data.Extentions
{
    public static class ExamModelExtention
    {
        public static List<Exam> ToExamList(this IEnumerable<ExamModel> exams) =>
            exams.Select(ToExam).ToList();


        public static Exam ToExam(this ExamModel exam)
        {
            return new Exam(exam.Title, exam.Test, exam.StudentAnswer, exam.SubjectId);
        }
    }
}
