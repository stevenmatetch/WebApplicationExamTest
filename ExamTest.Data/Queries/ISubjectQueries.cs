using ExamTest.Data.Models;

namespace ExamTest.Data.Queries
{
    public interface ISubjectQueries
    {
        void Add(SubjectModel subjectModel);
        void EntityStateModified(SubjectModel subjectModel);
        bool SubjectModelExists(int id);
        SubjectModel GetSubject(int id);
        Task<IEnumerable<SubjectModel>> GetSubjects();
        void Remove(SubjectModel subjectModel);
        Task SaveChangesAsync();
    }
}