namespace ExamTest.Shared.Models
{
    public record UserModel
    {
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
