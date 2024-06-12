using Microsoft.AspNetCore.Mvc;
using ExamTest.Data.Models;
using ExamTest.Api.Handler;
using ExamTest.Data.Queries;

namespace ExamTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamQueries _examQueries;

        public ExamController(IExamQueries examQueries)
        {
            _examQueries = examQueries;
        }

        // GET: api/Exam
        [HttpGet]
        public async Task<IResult> GetExamss()
        {
            return await ApiExamHandler.GetExams(_examQueries);
        }

        // GET: api/Exam/5
        [HttpGet("{id}")]
        public async Task<IResult> GetExamModel(int id)
        {
            return ApiExamHandler.GetExam(id , _examQueries);
        }

        // PUT: api/Exam/5
        [HttpPut("{id}")]
        public async Task<IResult> PutExamModel(int id, ExamModel examModel)
        {
            return await ApiExamHandler.PutExam(id, _examQueries, examModel);
        }

        // POST: api/Exam
        [HttpPost]
        public async Task<IResult> PostExamModel(ExamModel examModel)
        {
            return await ApiExamHandler.PostExam(_examQueries, examModel);
        }

        // DELETE: api/Exam/5
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteExamModel(int id)
        {
            return await ApiExamHandler.DeleteExamModel(id, _examQueries);
        }
    }
}
