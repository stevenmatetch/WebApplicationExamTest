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
    public class SubjectModelConfiguration : IEntityTypeConfiguration<SubjectModel>
    {
        public void Configure(EntityTypeBuilder<SubjectModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(50);
            //builder.Property(x => x.StudentAnswer).HasMaxLength(50);

        }
    }
}
