namespace WebApplicationExamTest.Models
{
    public class Answer
    {
        public int Id { get; set; }
        //public int ExamID { get; set; } 
        //public string Title { get; set; }

        //public string Test { get; set; }

        public string StudentAnswer { get; set; }
        public float Mark { get; set; }

        public string Comment { get; set; }

        public int SubjectId { get; set; }
        public string StudentId { get; set; }
        /// <summary>
        /// variable
        /// </summary>
        public bool Done { get; set; }

    }
}
