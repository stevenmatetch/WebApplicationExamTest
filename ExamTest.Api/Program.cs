using ExamTest.Data.Context;
using ExamTest.Data.Queries;
using ExamTest.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplicationExamTest;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExamTestDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IExamQueries, ExamQueries>();

builder.Services.AddControllers();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<Applicationuser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ExamTestDbContext>()
    .AddApiEndpoints();

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapIdentityApi<Applicationuser>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyHeader();
    c.AllowAnyOrigin();
});

app.UseAuthentication();

app.MapControllers();

// Create a scope to access UserManager and RoleManager
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Applicationuser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
   
    SeedDb.Seed(userManager,roleManager);
}

app.Run();

//app.UseAuthorization();
//app.UseSession();

/*app.MapGet("/exams", (IExamQueries examQueries) =>
    ApiExamHandler.GetExams(examQueries))
    .Produces<IList<ExamModel>>()
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status500InternalServerError)
    .RequireAuthorization();


app.MapGet("/students", (ClaimsPrincipal claims, IExamQueries examQueries) =>
    ApiExamHandler.GetStudent(claims, examQueries))
    .Produces<IList<ExamModel>>()
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status500InternalServerError);

app.MapGet("/answers", (ClaimsPrincipal claims, IExamQueries examQueries) =>
    ApiExamHandler.GetStudent(claims, examQueries))
    .Produces<IList<ExamModel>>()
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status500InternalServerError);

app.MapGet("/classes", (ClaimsPrincipal claims, IExamQueries examQueries) =>
    ApiExamHandler.GetStudent(claims, examQueries))
    .Produces<IList<ExamModel>>()
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status500InternalServerError);

app.MapGet("/subjects", (ClaimsPrincipal claims, IExamQueries examQueries) =>
    ApiExamHandler.GetStudent(claims, examQueries))
    .Produces<IList<ExamModel>>()
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status500InternalServerError);


app.MapGet("/user-profile", async (HttpContext httpContext) =>
{
    // Retrieve the user ID from the claims
    string userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

    if (string.IsNullOrEmpty(userId))
    {
        return Results.BadRequest("User ID could not be retrieved.");
    }

    // Assuming _applicationDbContext is injected into the scope
    var userProfile = await httpContext.RequestServices.GetRequiredService<ExamTestDbContext>()
       .Users
       .Where(u => u.Id == userId)
       .Select(u => new UserProfileDto
       {
           UserId = u.Id,
           Email = u.Email,
           Name = u.UserName,
       })
       .FirstOrDefaultAsync();

    if (userProfile == null)
    {
        return Results.NotFound("User profile not found.");
    }

    return Results.Ok(userProfile);
});
*/

// Create a scope to access UserManager
/*var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var b = scope.ServiceProvider.GetRequiredService<UserManager<ExamTest.Shared.Models.ApplicationUser>>();
    var c = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    // Perform operations with userManager here, such as seeding data
    var a = new SeedData();
    a.Seed(b, c);
}
*/
//app.MapFallbackToPage("/_Host");

