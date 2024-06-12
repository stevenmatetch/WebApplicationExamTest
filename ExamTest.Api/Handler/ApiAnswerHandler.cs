using ExamTest.Core.Services;
using ExamTest.Data.Models;
using ExamTest.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace ExamTest.Api.Handler
{
    public class ApiAnswerHandler
    {
        public static async Task<IResult> GetAnswers(IAnswerQueries answerQueries)
        {
            try
            {
                var exam = await AnswerService.GetAnswers(answerQueries);

                return Results.Ok(exam);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async static Task<IResult> GetAnswerModel(int id, IAnswerQueries answerQueries)
        {
            try
            {
                var examModel = AnswerService.GetAnswer(id, answerQueries);

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

        public async static Task<IResult> PutAnswerModel(int id, IAnswerQueries examQueries, AnswerModel answerModel)
        {
            try
            {
                if (id != answerModel.Id)
                {
                    return Results.BadRequest();
                }

                examQueries.EntityStateModified(answerModel);

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


        public async static Task<IResult> PostAnswerModel(IAnswerQueries examQueries, AnswerModel answerModel)
        {
            try
            {
                examQueries.Add(answerModel);

                await examQueries.SaveChangesAsync();

                return Results.Ok(answerModel);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async static Task<IResult> DeleteAnswerModel(int id, IAnswerQueries answerQueries)
        {
            try
            {
                var examModel = AnswerService.GetAnswer(id, answerQueries);

                if (examModel == null)
                {
                    return Results.NotFound();
                }

                AnswerService.Remove(examModel, answerQueries);

                await answerQueries.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
