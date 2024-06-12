using ExamTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamTest.Data.Context.Configurations
{
    public class AnswerModelConfiguration : IEntityTypeConfiguration<AnswerModel>
    {
        public void Configure(EntityTypeBuilder<AnswerModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Mark);
            builder.Property(x => x.StudentAnswer).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Comment);

            builder.Property(x => x.SubjectId);

            builder.Property(x => x.StudentId);

            builder.Property(x => x.Done);
        }
    }    
}
