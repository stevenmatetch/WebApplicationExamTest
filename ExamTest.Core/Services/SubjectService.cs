using ExamTest.Data.Models;
using ExamTest.Data.Queries;

namespace ExamTest.Core.Services
{
    public static class SubjectService
    {
        public static async Task<IEnumerable<SubjectModel>> GetSubjects(ISubjectQueries subjectQueries) => await subjectQueries.GetSubjects();

        public static SubjectModel GetSubject(int id, ISubjectQueries subjectQueries) => subjectQueries.GetSubject(id);

        public static void Remove(SubjectModel subjectModel, ISubjectQueries subjectQueries) => subjectQueries.Remove(subjectModel);
    }
}
