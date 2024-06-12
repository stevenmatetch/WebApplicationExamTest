using ExamTest.Data.Context.Configurations;
using ExamTest.Data.Models;
using ExamTest.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



namespace ExamTest.Data.Context
{

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ExamTestDbContext>
    {
        public ExamTestDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExamTestDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DATABASE_NAME;Integrated Security=True;TrustServerCertificate=True");

            return new ExamTestDbContext(optionsBuilder.Options);
        }
    }

    public class ExamTestDbContext : IdentityDbContext<Applicationuser>
    {
        public ExamTestDbContext(DbContextOptions<ExamTestDbContext> options) 
            : base(options) 
        {
           if(base.Database.ProviderName != "Microsoft.WntityFrameworkCore.InMemory")
           {
                base.Database.Migrate();
           }
        } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
          
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");
   
            builder.ApplyConfiguration(new ExamModelConfiguration());
            builder.ApplyConfiguration(new StudentClassConfigurationModel());
            builder.ApplyConfiguration(new SubjectModelConfiguration());
            builder.ApplyConfiguration(new AnswerModelConfiguration());
            builder.ApplyConfiguration(new ExamModelConfiguration());
            builder.ApplyConfiguration(new ClassModelConfiguration());
        }

        public DbSet<ExamModel> Exam { get; set; }
        public DbSet<SubjectModel> Subject { get; set; }
        public DbSet<ClassModel> Class { get; set; }
        public DbSet<AnswerModel> Answer { get; set; }
        public DbSet<StudentClassModel> StudentClass { get; set; }

       
    }
}
