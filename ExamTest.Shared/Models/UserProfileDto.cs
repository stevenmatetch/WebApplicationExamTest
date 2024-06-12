using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTest.Shared.Models
{
    public record UserProfileDto
    {
        public string UserId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}
