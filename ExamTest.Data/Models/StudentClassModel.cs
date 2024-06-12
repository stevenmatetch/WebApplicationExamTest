using System.ComponentModel.DataAnnotations;

namespace ExamTest.Data.Models
{
    public class StudentClassModel
    {
        public int Id { get; set; }
        public string StudentId { get; set; }

        public int ClassId { get; set; }        


        //context.Student.where(m.classId == SetClassId)


        //     context.StudentClass.where(m.classId == ClassID)

    }
}
