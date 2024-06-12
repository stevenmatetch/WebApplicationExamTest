using ExamTest.Data.Models;
using ExamTest.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTest.Core.Services
{
    public static class AnswerService
    {
        public static async Task<IEnumerable<AnswerModel>> GetAnswers(IAnswerQueries answerQueries) => await answerQueries.GetAnswers();

        public static AnswerModel GetAnswer(int id, IAnswerQueries answerQueries) => answerQueries.GetAnswer(id);

        public static void Remove(AnswerModel answerModel, IAnswerQueries answerQueries) => answerQueries.Remove(answerModel);
    }
}
