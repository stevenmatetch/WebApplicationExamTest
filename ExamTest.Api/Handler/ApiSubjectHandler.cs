using ExamTest.Core.Services;
using ExamTest.Data.Models;
using ExamTest.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace ExamTest.Api.Handler
{
    public static class ApiSubjectHandler
    {
        public static async Task<IResult> GetSubjects(ISubjectQueries examQueries)
        {
            try
            {
                var exam = await SubjectService.GetSubjects(examQueries);

                return Results.Ok(exam);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public static IResult GetSubject(int id, ISubjectQueries examQueries)
        {
            try
            {
                var subjectModel = examQueries.GetSubject(id);

                if (subjectModel == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(subjectModel);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async static Task<IResult> PutSubject(int id, ISubjectQueries subjectQueries, SubjectModel subjectModel)
        {
            try
            {
                if (id != subjectModel.Id)
                {
                    return Results.BadRequest();
                }

                subjectQueries.EntityStateModified(subjectModel);

                try
                {
                    await subjectQueries.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!subjectQueries.SubjectModelExists(id))
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

        public async static Task<IResult> PostSubject(ISubjectQueries subjectQueries, SubjectModel subjectModel)
        {
            try
            {
                subjectQueries.Add(subjectModel);

                await subjectQueries.SaveChangesAsync();

                return Results.Ok(subjectModel);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async static Task<IResult> DeleteSubject(int id, ISubjectQueries subjectQueries)
        {
            try
            {
                var examModel = subjectQueries.GetSubject(id);

                if (examModel == null)
                {
                    return Results.NotFound();
                }

                subjectQueries.Remove(examModel);

                await subjectQueries.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
