﻿using System.ComponentModel.DataAnnotations;

namespace ExamTest.Data.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        //public int ExamID { get; set; } 
        //public string Title { get; set; }

        //public string Test { get; set; }

        [DataType(DataType.MultilineText)]
        public string StudentAnswer { get; set; }
        public float Mark { get; set; }

        public string Comment { get; set; }

        public int SubjectId { get; set; }
        public string StudentId { get; set; }
        /// <summary>
        /// variable
        /// </summary>
        public bool Done { get; set; }

        public ICollection<AnswerModel> Answers { get; set; }

    }
}
