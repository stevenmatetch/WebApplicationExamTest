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
    public class StudentClassConfigurationModel : IEntityTypeConfiguration<StudentClassModel>
    {
        public void Configure(EntityTypeBuilder<StudentClassModel> builder)
        {
            builder.HasKey(x => x.Id);          
            builder.Property(x => x.StudentId);
            builder.Property(x => x.ClassId);

        }
    }
}
