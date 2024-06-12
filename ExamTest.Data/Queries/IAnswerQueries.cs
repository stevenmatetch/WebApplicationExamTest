using ExamTest.Data.Models;

namespace ExamTest.Data.Queries
{
    public interface IAnswerQueries
    {
        void Add(AnswerModel answerModel);
        void EntityStateModified(AnswerModel answerModel);
        bool ExamModelExists(int id);
        AnswerModel GetAnswer(int id);
        Task<IEnumerable<AnswerModel>> GetAnswers();
        void Remove(AnswerModel answerModel);
        Task SaveChangesAsync();
    }
}