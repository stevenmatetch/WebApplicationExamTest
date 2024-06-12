using ExamTest.Core.Services;
using ExamTest.Data.Models;
using ExamTest.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace ExamTest.Api.Handler
{
    public static class ApiExamHandler
    {
        public static async Task<IResult> GetExams(IExamQueries examQueries)
        {
            try
            {
                var exam = await ExamService.GetExams(examQueries);

                return Results.Ok(exam);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public static IResult GetExam(int id, IExamQueries examQueries)
        {
            try
            {
                var examModel = examQueries.GetExam(id);

                if (examModel == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(examModel);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async static Task<IResult> PutExam(int id, IExamQueries examQueries, ExamModel examModel)
        {
            try
            {
                if (id != examModel.Id)
                {
                    return Results.BadRequest();
                }

                examQueries.EntityStateModified(examModel);

                try
                {
                    await examQueries.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!examQueries.ExamModelExists(id))
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async static Task<IResult> PostExam(IExamQueries examQueries, ExamModel examModel)
        {
            try
            {
                examQueries.Add(examModel);
              
                await examQueries.SaveChangesAsync();

                return Results.Ok(examModel);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async static Task<IResult> DeleteExamModel(int id, IExamQueries examQueries)
        {
            try
            {
                var examModel = examQueries.GetExam(id);

                if (examModel == null)
                {
                    return Results.NotFound();
                }

                examQueries.Remove(examModel);

                await examQueries.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        /* public static IResult GetExams(IExamQueries examQueries)
         {
             try
             {
                 var exam = ExamService.GetExams(examQueries);

                 return Results.Ok(exam);
             }
             catch (Exception ex)
             {

                 return Results.Problem(ex.Message);
             }
         }

         public static IResult GetStudent(ClaimsPrincipal claims, IExamQueries examQueries)
         {
             try
             {
                 string userid = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

                 var exam = ExamService.GetExams(examQueries);

                 return Results.Ok(exam);
             }
             catch (Exception ex)
             {

                 return Results.Problem(ex.Message);
             }
         }
        */
    }
}
