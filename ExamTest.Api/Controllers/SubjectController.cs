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
    public class SubjectController : ControllerBase
    {

        private readonly ISubjectQueries _subjectQueries;

        public SubjectController(ISubjectQueries examQueries)
        {
            _subjectQueries = examQueries;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<IResult> GetExamss()
        {
            return await ApiSubjectHandler.GetSubjects(_subjectQueries);
        }

        // GET: api/Subject/5
        [HttpGet("{id}")]
        public async Task<IResult> GetSubjectModel(int id)
        {
            return ApiSubjectHandler.GetSubject(id, _subjectQueries);
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<IResult> PutExamModel(int id, SubjectModel examModel)
        {
            return await ApiSubjectHandler.PutSubject(id, _subjectQueries, examModel);
        }

        // POST: api/Subject
        [HttpPost]
        public async Task<IResult> PostExamModel(SubjectModel examModel)
        {
            return await ApiSubjectHandler.PostSubject(_subjectQueries, examModel);
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteExamModel(int id)
        {
            return await ApiSubjectHandler.DeleteSubject(id, _subjectQueries);
        }
    }
}
