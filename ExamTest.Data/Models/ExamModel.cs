
using System.ComponentModel.DataAnnotations;

namespace ExamTest.Data.Models
{
    public class ExamModel
    {
        //[Key]
        public int Id { get; set; }
        //public int ExamID { get; set; } 
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Test { get; set; }
        [DataType(DataType.MultilineText)]
        public string StudentAnswer { get; set; }
        //public float Mark { get; set; }

        public int SubjectId { get; set; }
        //public string StudentId { get; set; }
        //public bool myHidden { get; set; }

        public ExamModel(int id, string title, string test, string studentAnswer, int subjectId)
        {
            Id = id;
            Title = title;
            Test = test;
            StudentAnswer = studentAnswer;
            SubjectId = subjectId;
        }

        //public static ExamModel Create(ExamAddDto exam)

    }
}