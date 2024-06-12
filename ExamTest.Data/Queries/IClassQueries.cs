using ExamTest.Data.Models;

namespace ExamTest.Data.Queries
{
    public interface IClassQueries
    {
        void Add(ClassModel subjectModel);
        void EntityStateModified(ClassModel subjectModel);
        bool ExamModelExists(int id);
        ClassModel GetClass(int id);
        Task<IEnumerable<ClassModel>> GetClasses();
        void Remove(ClassModel classModel);
        Task SaveChangesAsync();
    }
}