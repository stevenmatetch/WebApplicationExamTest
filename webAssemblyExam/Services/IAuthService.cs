
using ExamTest.Shared.Models;
using System.Threading.Tasks;

namespace webAssemblyExam.Services
{
    public interface IAuthService
    {
        Task <HttpResponseMessage> Login(LoginModel loginModel);
        Task Logout();
        Task<HttpResponseMessage> Register(RegisterModel registerModel);
    }
}
