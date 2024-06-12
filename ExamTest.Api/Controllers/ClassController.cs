using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamTest.Data.Context;
using ExamTest.Data.Models;
using ExamTest.Api.Handler;
using ExamTest.Data.Queries;

namespace ExamTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IExamQueries _examQueries;

        public ClassController(IExamQueries examQueries)
        {
            _examQueries = examQueries;
        }

        // GET: api/Class
        [HttpGet]
        public async Task<IResult> GetExamss()
        {
            return await ApiExamHandler.GetExams(_examQueries);
        }

        // GET: api/Class/5
        [HttpGet("{id}")]
        public async Task<IResult> GetExamModel(int id)
        {
            return ApiExamHandler.GetExam(id, _examQueries);
        }

        // PUT: api/Class/5
        [HttpPut("{id}")]
        public async Task<IResult> PutExamModel(int id, ExamModel examModel)
        {
            return await ApiExamHandler.PutExam(id, _examQueries, examModel);
        }

        // POST: api/Class
        [HttpPost]
        public async Task<IResult> PostExamModel(ExamModel examModel)
        {
            return await ApiExamHandler.PostExam(_examQueries, examModel);
        }

        // DELETE: api/Class/5
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteExamModel(int id)
        {
            return await ApiExamHandler.DeleteExamModel(id, _examQueries);
        }
    }
}