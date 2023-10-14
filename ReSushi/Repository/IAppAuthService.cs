using ReSushi.Models;

namespace ReSushi.Repository
{
    public interface IAppAuthService
    {
        Task<Token> Authenticate(User user);
        //Task<RegistrationResult> Register(User user);
    }
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
