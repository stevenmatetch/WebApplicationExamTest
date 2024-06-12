using ExamTest.Data.Models;

namespace ExamTest.Data.Queries
{
    public interface IExamQueries
    {
        void Add(ExamModel examModel);
        void EntityStateModified(ExamModel examModel);
        bool ExamModelExists(int id);
        ExamModel GetExam(int id);
        Task<IEnumerable<ExamModel>> GetExams();
        void Remove(ExamModel examModel);
        Task SaveChangesAsync();
    }
}