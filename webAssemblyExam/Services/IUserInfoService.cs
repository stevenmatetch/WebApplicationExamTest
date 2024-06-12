
namespace webAssemblyExam.Services
{
    public interface IUserInfoService
    {
        Task<string?> GetUserIdAsync();
    }
}