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
    public class StudentModelConfiguration: IEntityTypeConfiguration<StudentModel>
    {
        public void Configure(EntityTypeBuilder<StudentModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
