using ExamTest.Data.Models;
using ExamTest.Data.Queries;

namespace ExamTest.Core.Services
{
    public static class ExamService
    {
        public static async Task<IEnumerable<ExamModel>> GetExams(IExamQueries examQueries) => await examQueries.GetExams();

        public static ExamModel GetExam(int id, IExamQueries examQueries) => examQueries.GetExam(id);

        public static void Remove(ExamModel examModel, IExamQueries examQueries) => examQueries.Remove(examModel);
    }
}
