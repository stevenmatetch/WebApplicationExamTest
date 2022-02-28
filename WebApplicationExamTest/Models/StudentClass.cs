using System.ComponentModel.DataAnnotations;

namespace WebApplicationExamTest.Models
{
    public class StudentClass
    {
       
        public int Id { get; set; }
        public string StudentId { get; set; }

        public int ClassId { get; set; }        


        //context.Student.where(m.classId == SetClassId)


        //     context.StudentClass.where(m.classId == ClassID)

    }
}
